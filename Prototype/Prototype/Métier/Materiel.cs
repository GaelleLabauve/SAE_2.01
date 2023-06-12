using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Prototype.Métier
{
    public class Materiel : Crud<Materiel>
    {
        public int IdMateriel { get; set; }
        public int FK_IdCategorie { get; set; }
        public string NomMateriel { get; set; }
        public string CodeBarre { get; set; }
        public string Refconstructeur { get; set; }
        public Categorie UneCategorie { get; set; }
        public ObservableCollection<Attribution> LesAttributions { get; set; }

   
        public Materiel()
        { }
        public Materiel(int idMateriel, int fk_idCategorie, string nomMateriel, string codeBarre, string refconstructeur)
        {
            this.IdMateriel = idMateriel;
            this.FK_IdCategorie = fk_idCategorie;
            this.NomMateriel = nomMateriel;
            this.CodeBarre = codeBarre;
            this.Refconstructeur = refconstructeur;
        }

        public void Create()
        {
            throw new NotImplementedException();
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

        public ObservableCollection<Materiel> FindAll()
        {
            ObservableCollection<Materiel> lesMateriels = new ObservableCollection<Materiel>();

            DataAccess accesBD = new DataAccess();
            String requete = "SELECT * FROM Materiel";
            DataTable datas = accesBD.GetData(requete);

            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Materiel m = new Materiel(int.Parse(row["idMateriel"].ToString()), int.Parse(row["idCategorie"].ToString()), (String)row["nomMateriel"], (String)row["codeBarre"], (String)row["refConstructeur"]);
                    lesMateriels.Add(m);
                }
            }
            return lesMateriels;
        }        
    }
}