using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text.RegularExpressions;

namespace Prototype.Metier
{
    public class Enseignant : Crud<Enseignant>
    {
        /// <summary>
        /// Obtient le NomPrenom de cet(te) enseignant(e). Il s'agit de la concaténation de NomPersonnel et PrenomPersonnel –
        /// </summary>
        public string NomPrenom { get; }

        /// <summary>
        /// Obtient ou definit l'IdPersonnel de cet(te) enseignant(e).
        /// </summary>
        public int IdPersonnel { get; set; }

        /// <summary>
        /// Obtient ou definit l'EmailPersonnel de cet(te) enseignant(e).
        /// </summary>
        public string EmailPersonnel { get; set; }

        /// <summary>
        /// Obtient ou definit le nom de cet(te) enseignant(e).
        /// </summary>
        public string NomPersonnel { get; set; }

        /// <summary>
        /// Obtient ou definit le prenom de cet(te) enseignant(e).
        /// </summary>
        public string PrenomPersonnel { get; set; }


        /// <summary>
        /// Obtient ou definit l'ObservableCollection des attribution de cet(te) enseignant(e).
        /// </summary>
        public ObservableCollection<Attribution> LesAttributions { get; set; }


        /// <summary>
        /// Cree un(e) enseignant(e) vide.
        /// </summary>
        public Enseignant()
        { }

        /// <summary>
        /// Cree un(e) enseignant(e).
        /// </summary>
        /// <param name="idPersonnel"> un entier unique à ce(tte) enseignant(e)</param>
        /// <param name="emailPersonnel"> une chaine de caractère unique à ce(tte) enseignant(e), contenant forcément un symbole @</param>
        /// <param name="nomPersonnel"> une chaine de caractère indiquant le nom de ce(tte) enseignant(e)</param>
        /// <param name="prenomPersonnel"> une chaine de caractère indiquant le prenom de ce(tte) enseignant(e)</param>
        public Enseignant(int idPersonnel, string emailPersonnel, string nomPersonnel, string prenomPersonnel)
        {
            this.IdPersonnel = idPersonnel;
            this.EmailPersonnel = emailPersonnel;
            this.NomPersonnel = nomPersonnel;
            this.PrenomPersonnel = prenomPersonnel;
            this.NomPrenom = nomPersonnel+" "+prenomPersonnel;
        }

        /// <summary>
        /// Cree un(e) enseignant(e).
        /// </summary>
        /// <param name="emailPersonnel"> une chaine de caractere unique à ce(tte) enseignant(e), contenant forcement un symbole @</param>
        /// <param name="nomPersonnel"> une chaine de caractere indiquant le nom de ce(tte) enseignant(e)</param>
        /// <param name="prenomPersonnel"> une chaine de caractere indiquant le prenom de ce(tte) enseignant(e)</param>
        public Enseignant(string emailPersonnel, string nomPersonnel, string prenomPersonnel)
        {
            this.EmailPersonnel = emailPersonnel;
            this.NomPersonnel = nomPersonnel;
            this.PrenomPersonnel = prenomPersonnel;
            this.NomPrenom = nomPersonnel + " " + prenomPersonnel;
        }


        /// <summary>
        /// Cree dans la base de donnee ce/cette enseignat(e).
        /// </summary>
        public void Create()
        {
            DataAccess accessDB = new DataAccess();
            accessDB.SetData($"INSERT INTO PERSONNEL(nomPersonnel,prenomPersonnel,emailPersonnel) VALUES ('{this.NomPersonnel}','{this.PrenomPersonnel}','{this.EmailPersonnel}');");
        }

        public void Read()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Mets à jour dans la base de donnee ce/cette enseignat(e).
        /// </summary>
        public void Update()
        {
            DataAccess accessDB = new DataAccess();
            accessDB.GetData($"UPDATE PERSONNEL SET emailPersonnel='{this.EmailPersonnel}', nomPersonnel='{this.NomPersonnel}', prenomPersonnel='{this.PrenomPersonnel}' WHERE idPersonnel={this.IdPersonnel};");
        }


        /// <summary>
        /// Supprime dans la base de donnee ce/cette enseignat(e).
        /// </summary>
        public void Delete()
        {
            DataAccess accessDB = new DataAccess();
            accessDB.SetData($"DELETE FROM PERSONNEL WHERE idPersonnel='{this.IdPersonnel}';");
        }


        /// <summary>
        /// Parcours la table PERSONNEL de la base de donnee.
        /// </summary>
        /// <returns> ObservableCollection<Enseignant> regroupant tous les enseignants mis dans la base de donnee</Enseignant></returns>
        public ObservableCollection<Enseignant> FindAll()
        {
            ObservableCollection<Enseignant> lesEnseignants = new ObservableCollection<Enseignant>();

            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData("SELECT * FROM PERSONNEL ORDER BY idPersonnel;");

            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Enseignant e = new Enseignant(int.Parse(row["idPersonnel"].ToString()), (String)row["emailPersonnel"], (String)row["nomPersonnel"], (String)row["prenomPersonnel"]);
                    lesEnseignants.Add(e);
                }
            }
            return lesEnseignants;
        }
    }
}