using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Prototype.Metier
{
    public class Attribution : Crud<Attribution>
    {
        /// <summary>
        /// Obtient ou definit l'idMateriel de cette attribution. Permet de trouver le materiel.
        /// </summary>
        public int FK_IdMateriel { get; set; }

        /// <summary>
        /// Obtient ou definit l'idPersonnel de cette attribution. Permet de trouver l'enseignant.
        /// </summary>
        public int FK_IdPersonnel { get; set; }

        /// <summary>
        /// Obtient ou definit la date de cette attribution.
        /// </summary>
        public DateTime DateAttribution { get; set; }

        /// <summary>
        /// Obtient ou definit le commentaire de cette attribution.
        /// </summary>
        public string CommentaireAttribution { get; set; }


        /// <summary>
        /// Obtient ou definit l'enseignant de cette attribution.
        /// </summary>
        public Enseignant UnEnseignant { get; set; }


        /// <summary>
        /// Obtient ou definit le materiel de cette attribution.
        /// </summary>
        public Materiel UnMateriel { get; set; }

        /// <summary>
        /// Cree une attribution vide.
        /// </summary>
        public Attribution()
        {
        }

        /// <summary>
        /// Cree une attribution.
        /// </summary>
        /// <param name="fK_IdMateriel"> un entier donnant la cle unique du materiel de cette attribution</param>
        /// <param name="fK_IdPersonnel"> un entier donnant la cle unique de l'enseignant(e) de cette attribution</param>
        /// <param name="dateAttribution"> un dateTime donnant la date de cette attribution</param>
        /// <param name="commentaireAttribution"> une chaine de caractere donnant le commentaire de cette attribution</param>
        public Attribution(int fK_IdMateriel, int fK_IdPersonnel, DateTime dateAttribution, string commentaireAttribution)
        {
            this.FK_IdMateriel = fK_IdMateriel;
            this.FK_IdPersonnel = fK_IdPersonnel;
            this.DateAttribution = dateAttribution;
            this.CommentaireAttribution = commentaireAttribution;
        }

        /// <summary>
        /// Cree dans la base de donnees cette attribution.
        /// </summary>
        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData($"INSERT INTO EST_ATTRIBUE(idMateriel, idPersonnel, dateAttribution, commentaireAttribution) VALUES('{this.FK_IdMateriel}','{this.FK_IdPersonnel}','{this.DateAttribution.ToString("yyyy-MM-dd")}','{this.CommentaireAttribution}');");
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Met a jour dans la base de donnees cette attribution.
        /// </summary>
        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData($"UPDATE EST_ATTRIBUE SET commentaireAttribution='{this.CommentaireAttribution}' WHERE idMateriel='{this.FK_IdMateriel}' AND idPersonnel='{this.FK_IdPersonnel}' AND dateAttribution='{this.DateAttribution.ToString("yyyy-MM-dd")}';");
        }

        /// <summary>
        /// Supprime de la base de donnees cette attribution.
        /// </summary>
        public void Delete()
        {
            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData($"DELETE FROM EST_ATTRIBUE WHERE idMateriel={this.FK_IdMateriel} AND idPersonnel='{this.FK_IdPersonnel}';");
        }

        /// <summary>
        /// Parcours la table EST_ATTRIBUE de la base de donnees.
        /// </summary>
        /// <returns> ObservableCollection<Attribution> regroupant toutes les attributions mise dans la base de donnee</returns>
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