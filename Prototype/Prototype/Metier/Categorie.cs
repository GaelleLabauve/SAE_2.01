using Prototype.Metier;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace Prototype.Metier
{
    /// <summary>
    /// Stocke 3 informations :
    /// 1 chaines : le nom de la cat�gorie
    /// 1 entier : l'idCategorie entrer dans la base de donn�e
    /// 1 ObservableCollection<Materiel> : la liste des materiel de cette cat�gorie
    /// </summary>
    public class Categorie : Crud<Categorie>
    {
        /// <summary>
        /// Obtient ou d�finit l'idCategorie de cette cat�gorie �
        /// </summary>
        public int IdCategorie { get; set; }

        /// <summary>
        /// Obtient ou d�finit le nom de cette cat�gorie �
        /// </summary>
        public string NomCategorie { get; set; }

        /// <summary>
        /// Obtient ou d�finit la liste des materiels de cette cat�gorie �
        /// </summary>
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

        /// <summary>
        /// Cr�e dans la base de donn�e cette cat�gorie
        /// </summary>
        public void Create()
        {   
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"INSERT INTO CATEGORIE_MATERIEL(nomCategorie) VALUES('{this.NomCategorie}');");
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modifie dans la base de donn�e le nom de cette cat�gorie
        /// </summary>
        public void Update()
        {   
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"UPDATE CATEGORIE_MATERIEL SET nomCategorie='{this.NomCategorie}' WHERE idCategorie='{this.IdCategorie}';");
        }

        /// <summary>
        /// Supprime dans la base de donn�e cette cat�gorie
        /// </summary>
        public void Delete()
        {   
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"DELETE FROM CATEGORIE_MATERIEL WHERE idCategorie='{this.IdCategorie}';");
        }

        /// <summary>
        /// Cherche dans la base de donn�e toute les catgories enregistr�es
        /// </summary>
        /// /// <returns>L'ObservableCollection regroupant toutes les cat�gories entr�es dans la base de donn�e</returns>
        public ObservableCollection<Categorie> FindAll()
        {
            
            ObservableCollection<Categorie> lesCategories = new ObservableCollection<Categorie>();

            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData("SELECT * FROM CATEGORIE_MATERIEL ODER BY idCategorie;");

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