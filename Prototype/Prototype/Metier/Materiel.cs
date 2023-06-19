using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Prototype.Metier
{
    public class Materiel : Crud<Materiel>
    {
        public const int LIMITE_CARACTERE = 100;

        /// <summary>
        /// Obtient ou definit l'IdMateriel de ce materiel.
        /// </summary>
        public int IdMateriel { get; set; }
        //public int IdMateriel { get { return this.IdMateriel; } set 
        //    {
        //        if (value < 0)
        //            throw new ArgumentOutOfRangeException("L'Id ne peut pas être négatif.");
        //        this.IdMateriel = value;
        //    } 
        //}

        /// <summary>
        /// Obtient ou definit l'idCategorie de ce materiel.
        /// </summary>
        public int FK_IdCategorie { get; set; }

        /// <summary>
        /// Obtient ou definit le nom de ce materiel.
        /// </summary>
        public string NomMateriel { get; set; }
        //public string NomMateriel { get { return this.NomMateriel; } set
        //    {
        //        if (value.Length >= LIMITE_CARACTERE)
        //            throw new ArgumentOutOfRangeException("Le nom ne peut pas dépasser " + LIMITE_CARACTERE + " caractères.");
        //        else if (string.IsNullOrEmpty(value))
        //            throw new ArgumentNullException("Le nom ne peut pas être null");
        //        this.NomMateriel = value;
        //    }
        //}

        /// <summary>
        /// Obtient ou definit le code barre d'inventaire de ce materiel.
        /// </summary>
        public string CodeBarreInventaire { get; set; }
        //public string CodeBarreInventaire { get { return this.CodeBarreInventaire; }
        //    set
        //    {
        //        if (value.Length >= LIMITE_CARACTERE)
        //            throw new ArgumentOutOfRangeException("Le code barre ne peut pas dépasser " + LIMITE_CARACTERE + " caractères.");
        //        else if (string.IsNullOrEmpty(value))
        //            throw new ArgumentNullException("Le code barre ne peut pas être null");
        //        this.CodeBarreInventaire = value;
        //    }
        //}

        /// <summary>
        /// Obtient ou definit la reference constructeur de ce materiel.
        /// </summary>
        public string ReferenceConstructeurMateriel { get; set; }
        //public string ReferenceConstructeurMateriel { get { return this.ReferenceConstructeurMateriel; }
        //    set
        //    {
        //        if (value.Length >= LIMITE_CARACTERE)
        //            throw new ArgumentOutOfRangeException("La reference ne peut pas dépasser " + LIMITE_CARACTERE + " caractères.");
        //        else if (string.IsNullOrEmpty(value))
        //            throw new ArgumentNullException("La reference ne peut pas être null");
        //        this.ReferenceConstructeurMateriel = value;
        //    }
        //}

        /// <summary>
        /// Obtient ou definit la categorie de ce materiel.
        /// </summary>
        public Categorie UneCategorie { get; set; }

        /// <summary>
        /// Obtient ou definit les attributions appelant ce materiel.
        /// </summary>
        public ObservableCollection<Attribution> LesAttributions { get; set; }


        /// <summary>
        /// Cree un materiel vide.
        /// </summary>
        public Materiel()
        { }

        /// <summary>
        /// Cree un materiel.
        /// </summary>
        /// <param name="fk_idCategorie"> un entier permettant de retrouver la categorie de ce materiel</param>
        /// <param name="nomMateriel"> une chaine de caractere donnant le nom de ce materiel</param>
        /// <param name="codeBarreInventaire"> une chaine de caractere donnant le code barre inventaire unique de ce materiel</param>
        /// <param name="referenceConstructeurMateriel"> une chaine de caractere donnant la reference constructeur de ce materiel</param>
        public Materiel(int fk_idCategorie, string nomMateriel, string codeBarreInventaire, string referenceConstructeurMateriel)
        {
            this.FK_IdCategorie = fk_idCategorie;
            this.NomMateriel = nomMateriel;
            this.CodeBarreInventaire = codeBarreInventaire;
            this.ReferenceConstructeurMateriel = referenceConstructeurMateriel;
        }

        /// <summary>
        /// Cree un materiel.
        /// </summary>
        /// <param name="idMateriel"> un entier unique a ce materiel</param>
        /// <param name="fk_idCategorie"> un entier permettant de retrouver la categorie de ce materiel</param>
        /// <param name="nomMateriel"> une chaine de caractere donnant le nom de ce materiel</param>
        /// <param name="codeBarreInventaire"> une chaine de caractere donnant le code barre inventaire unique de ce materiel</param>
        /// <param name="referenceConstructeurMateriel"> une chaine de caractere donnant la reference constructeur de ce materiel</param>
        public Materiel(int idMateriel, int fk_idCategorie, string nomMateriel, string codeBarreInventaire, string referenceConstructeurMateriel)
        {
            this.IdMateriel = idMateriel;
            this.FK_IdCategorie = fk_idCategorie;
            this.NomMateriel = nomMateriel;
            this.CodeBarreInventaire = codeBarreInventaire;
            this.ReferenceConstructeurMateriel = referenceConstructeurMateriel;
        }

        /// <summary>
        /// Cree dans la base de donnee ce materiel.
        /// </summary>
        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"INSERT INTO MATERIEL(idCategorie, nomMateriel, codeBarreInventaire, referenceConstructeurMateriel) VALUES('{this.FK_IdCategorie}','{this.NomMateriel}','{this.CodeBarreInventaire}','{this.ReferenceConstructeurMateriel}');");
        }

        public bool Read()
        {
            DataAccess accesDB = new DataAccess();
            return accesDB.GetData($"SELECT 'X' FROM MATERIEL WHERE codeBarreInventaire='{this.CodeBarreInventaire}'").Rows.Count == 0;
        }

        /// <summary>
        /// Mets a jour dans la base de donnee ce materiel.
        /// </summary>
        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"UPDATE MATERIEL SET idCategorie='{this.FK_IdCategorie}', nomMateriel='{this.NomMateriel}', codeBarreInventaire='{this.CodeBarreInventaire}', referenceConstructeurMateriel='{this.ReferenceConstructeurMateriel}' WHERE idMateriel='{this.IdMateriel}';");
        }

        /// <summary>
        /// Supprime dans la base de donnee ce materiel.
        /// </summary>
        public void Delete()
        {
            DataAccess accessDB = new DataAccess();
            accessDB.SetData($"DELETE FROM MATERIEL WHERE idMateriel='{this.IdMateriel}'");
        }

        /// <summary>
        /// Parcours la table MATERIEL de la base de donnee.
        /// </summary>
        /// <returns> ObservableCollection<Materiel> regroupant tous les materiels mis dans la base de donnee</returns>
        public ObservableCollection<Materiel> FindAll()
        {
            ObservableCollection<Materiel> lesMateriels = new ObservableCollection<Materiel>();

            DataAccess accesBD = new DataAccess();
            DataTable datas = accesBD.GetData("SELECT * FROM MATERIEL ORDER BY idMateriel;");

            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Materiel m = new Materiel(int.Parse(row["idMateriel"].ToString()), int.Parse(row["idCategorie"].ToString()), (String)row["nomMateriel"], (String)row["codeBarreInventaire"], (String)row["referenceConstructeurMateriel"]);
                    lesMateriels.Add(m);
                }
            }
            return lesMateriels;
        }        
    }
}