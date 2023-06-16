using Prototype.Metier;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace Prototype.Metier
{
    /// <summary>
    /// Stocke 3 informations :
    /// 1 chaines : le nom de la categorie
    /// 1 entier : l'idCategorie entrer dans la base de donnee
    /// 1 ObservableCollection<Materiel> : la liste des materiel de cette categorie
    /// </summary>
    public class Categorie : Crud<Categorie>
    {
        /// <summary>
        /// Obtient ou definit l'idCategorie de cette catégorie –
        /// </summary>
        public int IdCategorie { get; set; }

        /// <summary>
        /// Obtient ou definit le nom de cette categorie –
        /// </summary>
        public string NomCategorie { get; set; }

        /// <summary>
        /// Obtient ou definit la liste des materiels de cette categorie –
        /// </summary>
        public ObservableCollection<Materiel> LesMateriels { get; set; }


        /// <summary>
        /// Cree une categorie.
        /// </summary>
        public Categorie()
        { }

        /// <summary>
        /// Cree une categorie.
        /// </summary>
        /// <param name="idCategorie"> un entier unique a cette categorie</param>
        /// <param name="nomCategorie"> une chaine de caractere unique a cette categorie</param>
        public Categorie(int idCategorie, string nomCategorie)
        {
            this.IdCategorie = idCategorie;
            this.NomCategorie = nomCategorie;
        }

        /// <summary>
        /// Cree une categorie.
        /// </summary>
        /// <param name="nomCategorie"> une chaine de caractere unique a cette categorie</param>
        public Categorie( string nomCategorie)
        {
            this.NomCategorie = nomCategorie;
        }

        /// <summary>
        /// Crée dans la base de donnée cette catégorie
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
        /// Modifie dans la base de donnée le nom de cette catégorie
        /// </summary>
        public void Update()
        {   
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"UPDATE CATEGORIE_MATERIEL SET nomCategorie='{this.NomCategorie}' WHERE idCategorie='{this.IdCategorie}';");
        }

        /// <summary>
        /// Supprime dans la base de donnée cette catégorie
        /// </summary>
        public void Delete()
        {   
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"DELETE FROM CATEGORIE_MATERIEL WHERE idCategorie='{this.IdCategorie}';");
        }

        /// <summary>
        /// Cherche dans la base de donnée toute les catgories enregistrées
        /// </summary>
        /// /// <returns>L'ObservableCollection regroupant toutes les catégories entrées dans la base de donnée</returns>
        public ObservableCollection<Categorie> FindAll()
        {
            
            ObservableCollection<Categorie> lesCategories = new ObservableCollection<Categorie>();

            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData("SELECT * FROM CATEGORIE_MATERIEL ORDER BY idCategorie;");

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