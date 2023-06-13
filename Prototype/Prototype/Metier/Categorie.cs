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
            String requete = "insert into Categorie(nomCategorie) values('"+this.NomCategorie+"'";
            DataTable datas = accesBD.GetData(requete);
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            String requete = "update Categorie set nomcategorie='" + this.NomCategorie + "' where idCategorie =" + IdCategorie;
            DataTable datas = accesBD.GetData(requete);
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();
            String requete = "delete from Categorie where idcategorie=" + this.IdCategorie;
            DataTable datas = accesBD.GetData(requete);
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