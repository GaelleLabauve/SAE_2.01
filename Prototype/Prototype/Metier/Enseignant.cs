using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text.RegularExpressions;

namespace Prototype.Metier
{
    public class Enseignant : Crud<Enseignant>
    {
        public int IdPersonnel { get; set; }
        public string Email { get; set; }
        public string NomPersonnel { get; set; }
        public string PrenomPersonnel { get; set; }
        public ObservableCollection<Attribution> LesAttributions { get; set; }

        public Enseignant()
        { }
        public Enseignant(int idPersonnel, string email, string nomPersonnel, string prenomPersonnel)
        {
            this.IdPersonnel = idPersonnel;
            this.Email = email;
            this.NomPersonnel = nomPersonnel;
            this.PrenomPersonnel = prenomPersonnel;
        }
        public Enseignant(string email, string nomPersonnel, string prenomPersonnel)
        {
            this.Email = email;
            this.NomPersonnel = nomPersonnel;
            this.PrenomPersonnel = prenomPersonnel;
        }

        public void Create()
        {
            DataAccess accessDB = new DataAccess();
            accessDB.SetData($"INSERT INTO ENSEIGNANT(nomPersonnel,prenomPersonnel,mail) VALUES ('{this.NomPersonnel}','{this.PrenomPersonnel}','{this.Email}')");
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
            accessDB.SetData($"DELETE FROM ENSEIGNANT WHERE idPersonnel='{this.IdPersonnel}'");
        }

        public ObservableCollection<Enseignant> FindAll()
        {
            ObservableCollection<Enseignant> lesEnseignants = new ObservableCollection<Enseignant>();

            DataAccess accesBD = new DataAccess();
            String requete = "SELECT * FROM Enseignant";
            DataTable datas = accesBD.GetData(requete);

            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Enseignant e = new Enseignant(int.Parse(row["idPersonnel"].ToString()), (String)row["mail"], (String)row["nomPersonnel"], (String)row["prenomPersonnel"]);
                    lesEnseignants.Add(e);
                }
            }
            return lesEnseignants;
        }
    }
}