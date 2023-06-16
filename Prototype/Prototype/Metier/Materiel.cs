using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Prototype.Metier
{
    public class Materiel : Crud<Materiel>
    {
        public int IdMateriel { get; set; }
        public int FK_IdCategorie { get; set; }
        public string NomMateriel { get; set; }
        public string CodeBarreInventaire { get; set; }
        public string ReferenceConstructeurMateriel { get; set; }
        public Categorie UneCategorie { get; set; }
        public ObservableCollection<Attribution> LesAttributions { get; set; }

   
        public Materiel()
        { }
        public Materiel(int fk_idCategorie, string nomMateriel, string codeBarreInventaire, string referenceConstructeurMateriel)
        {
            this.FK_IdCategorie = fk_idCategorie;
            this.NomMateriel = nomMateriel;
            this.CodeBarreInventaire = codeBarreInventaire;
            this.ReferenceConstructeurMateriel = referenceConstructeurMateriel;
        }
        public Materiel(int idMateriel, int fk_idCategorie, string nomMateriel, string codeBarreInventaire, string referenceConstructeurMateriel)
        {
            this.IdMateriel = idMateriel;
            this.FK_IdCategorie = fk_idCategorie;
            this.NomMateriel = nomMateriel;
            this.CodeBarreInventaire = codeBarreInventaire;
            this.ReferenceConstructeurMateriel = referenceConstructeurMateriel;
        }

        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"INSERT INTO MATERIEL(idCategorie, nomMateriel, codeBarreInventaire, referenceConstructeurMateriel) VALUES('{this.FK_IdCategorie}','{this.NomMateriel}','{this.CodeBarreInventaire}','{this.ReferenceConstructeurMateriel}');");
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            accesBD.GetData($"UPDATE MATERIEL SET idCategorie='{this.FK_IdCategorie}', nomMateriel='{this.NomMateriel}', codeBarreInventaire='{this.CodeBarreInventaire}', referenceConstructeurMateriel='{this.ReferenceConstructeurMateriel}' WHERE idMateriel='{this.IdMateriel}';");
        }

        public void Delete()
        {
            DataAccess accessDB = new DataAccess();
            accessDB.SetData($"DELETE FROM MATERIEL WHERE idMateriel='{this.IdMateriel}'");
        }

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