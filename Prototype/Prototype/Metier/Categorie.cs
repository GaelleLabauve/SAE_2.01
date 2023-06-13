using Prototype.Metier;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace Prototype.Metier
{
    public class Categorie : Crud<Categorie>
    {
        public int IdCategorie { get; set; }
        public string NomCategorie { get; set; }
        public ObservableCollection<Materiel> LesMateriels { get; set; }


        public Categorie()
        { }
        public Categorie(int idCategorie, string nomCategorie)
        {
            this.IdCategorie = idCategorie;
            this.NomCategorie = nomCategorie;
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

        public ObservableCollection<Categorie> FindAll()
        {
            ObservableCollection<Categorie> lesCategories = new ObservableCollection<Categorie>();

            DataAccess accesBD = new DataAccess();
            String requete = "SELECT * FROM Categorie";
            DataTable datas = accesBD.GetData(requete);

            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Categorie c = new Categorie(int.Parse(row["idCategorie"].ToString()), (String)row["nomCategorie"]);
                    lesCategories.Add(c);
                }
            }
            return lesCategories;
        }
    }
}