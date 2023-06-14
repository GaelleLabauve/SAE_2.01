using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text.RegularExpressions;

namespace Prototype.Metier
{
    public class Enseignant : Crud<Enseignant>
    {
        public string NomPrenom { get; }
        public int IdPersonnel { get; set; }
        public string EmailPersonnel { get; set; }
        public string NomPersonnel { get; set; }
        public string PrenomPersonnel { get; set; }
        public ObservableCollection<Attribution> LesAttributions { get; set; }

        public Enseignant()
        { }
        public Enseignant(int idPersonnel, string emailPersonnel, string nomPersonnel, string prenomPersonnel)
        {
            this.IdPersonnel = idPersonnel;
            this.EmailPersonnel = emailPersonnel;
            this.NomPersonnel = nomPersonnel;
            this.PrenomPersonnel = prenomPersonnel;
            this.NomPrenom = nomPersonnel+" "+prenomPersonnel;
        }
        public Enseignant(string emailPersonnel, string nomPersonnel, string prenomPersonnel)
        {
            this.EmailPersonnel = emailPersonnel;
            this.NomPersonnel = nomPersonnel;
            this.PrenomPersonnel = prenomPersonnel;
            this.NomPrenom = nomPersonnel + " " + prenomPersonnel;
        }

        public void Create()
        {
            DataAccess accessDB = new DataAccess();
            accessDB.SetData($"INSERT INTO PERSONNEL(nomPersonnel,prenomPersonnel,emailPersonnel) VALUES ('{this.NomPersonnel}','{this.PrenomPersonnel}','{this.EmailPersonnel}');");
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
            DataAccess accessDB = new DataAccess();
            accessDB.SetData($"DELETE FROM PERSONNEL WHERE idPersonnel='{this.IdPersonnel}';");
        }

        public ObservableCollection<Enseignant> FindAll()
        {
            ObservableCollection<Enseignant> lesEnseignants = new ObservableCollection<Enseignant>();

            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData("SELECT * FROM PERSONNEL;");

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