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
        public Categorie( string nomCategorie)
        {
            this.NomCategorie = nomCategorie;
        }

        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"INSERT INTO CATEGORIE_MATERIEL(nomCategorie) VALUES('{this.NomCategorie}');");
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"UPDATE CATEGORIE_MATERIEL SET nomCategorie='{this.NomCategorie}' WHERE idCategorie='{this.IdCategorie}';");
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"DELETE FROM CATEGORIE_MATERIEL WHERE idCategorie='{this.IdCategorie}';");
        }

        public ObservableCollection<Categorie> FindAll()
        {
            ObservableCollection<Categorie> lesCategories = new ObservableCollection<Categorie>();

            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData("SELECT * FROM CATEGORIE_MATERIEL;");

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