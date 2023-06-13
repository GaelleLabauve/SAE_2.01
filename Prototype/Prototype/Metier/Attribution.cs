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
        public string Commentaire { get; set; }
        public Enseignant UnEnseignant { get; set; }
        public Materiel UnMateriel { get; set; }

        public Attribution()
        {
        }
        public Attribution(int fK_IdMateriel, int fK_IdPersonnel, DateTime dateAttribution, string commentaire)
        {
            this.FK_IdMateriel = fK_IdMateriel;
            this.FK_IdPersonnel = fK_IdPersonnel;
            this.DateAttribution = dateAttribution;
            this.Commentaire = commentaire;
        }

        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            String requete = "insert into Attribution(idmateriel, idpersonnel, dateattribution, commentaire) values(" + "1,1," + ", CURRENTDATE()," + this.Commentaire +"');";
            DataTable datas = accesBD.GetData(requete);
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Attribution> FindAll()
        {
            ObservableCollection<Attribution> lesAttributions = new ObservableCollection<Attribution>();

            DataAccess accesBD = new DataAccess();
            String requete = "SELECT * FROM Attribution";
            DataTable datas = accesBD.GetData(requete);

            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Attribution a = new Attribution(int.Parse(row["idMateriel"].ToString()), int.Parse(row["idPersonnel"].ToString()), (DateTime)row["dateAttribution"], (String)row["commentaire"]);
                    lesAttributions.Add(a);
                }
            }
            return lesAttributions;
        }
    }
}