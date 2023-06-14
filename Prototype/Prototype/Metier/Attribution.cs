using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Prototype.Metier
{
    public class Attribution : Crud<Attribution>
    {
        public int FK_IdMateriel { get; set; }
        public int FK_IdPersonnel { get; set; }
        public DateTime DateAttribution { get; set; }
        public string CommentaireAttribution { get; set; }
        public Enseignant UnEnseignant { get; set; }
        public Materiel UnMateriel { get; set; }

        public Attribution()
        {
        }
        public Attribution(int fK_IdMateriel, int fK_IdPersonnel, DateTime dateAttribution, string commentaireAttribution)
        {
            this.FK_IdMateriel = fK_IdMateriel;
            this.FK_IdPersonnel = fK_IdPersonnel;
            this.DateAttribution = dateAttribution;
            this.CommentaireAttribution = commentaireAttribution;
        }

        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData($"INSERT INTO EST_ATTRIBUE(idMateriel, idPersonnel, dateAttribution, commentaireAttribution) VALUES('{this.FK_IdMateriel}','{this.FK_IdPersonnel}','{this.DateAttribution}','{this.CommentaireAttribution}');");
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData($"UPDATE EST_ATTRIBUE SET commentaireAttribution='{this.CommentaireAttribution}' WHERE idMateriel='{this.FK_IdMateriel}' AND idPersonnel='{this.FK_IdPersonnel}';");
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData($"DELETE FROM EST_ATTRIBUE WHERE idMateriel={this.FK_IdMateriel} AND idPersonnel='{this.FK_IdPersonnel}';");
        }

        public ObservableCollection<Attribution> FindAll()
        {
            ObservableCollection<Attribution> lesAttributions = new ObservableCollection<Attribution>();

            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData("SELECT * FROM EST_ATTRIBUE ORDER BY idMateriel,idPersonnel,DateAttribution;");

            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Attribution a = new Attribution(int.Parse(row["idMateriel"].ToString()), int.Parse(row["idPersonnel"].ToString()), (DateTime)row["dateAttribution"], (String)row["commentaireAttribution"]);
                    lesAttributions.Add(a);
                }
            }
            return lesAttributions;
        }
    }
}