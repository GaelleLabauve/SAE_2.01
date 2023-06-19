using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prototype.Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Metier.Tests
{
    [TestClass()]
    public class CategorieTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Categorie_AvecUnNomNull_Test()
        {
            Categorie c= new Categorie("");
        }
        
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Categorie_AvecNomTropLong_Test()
        {
            string nom = "";
            for (int i=0; i<=Categorie.LIMITE_TAILLE_CARACTERE_NOM; i++)
            {
                nom += "a";
            }
            Categorie c= new Categorie(nom);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CategorieIdNegatifTest()
        {
            Categorie c= new Categorie(-1, "nomTest");
        }


        [TestMethod()]
        public void CreateTest()
        {
            Categorie c = new Categorie("CreateTest");
            c.Create();

            Assert.AreEqual(1, new DataAccess().GetData($"SELECT * FROM CATEGORIE_MATERIEL WHERE nomCategorie='{c.NomCategorie}'").Rows.Count, "La catégorie a été insérée dans la base de données.");

            new DataAccess().SetData($"DELETE FROM CATEGORIE_MATERIEL WHERE nomCategorie='{c.NomCategorie}'");
        }

        [TestMethod()]
        public void ReadTest()
        {
            DataAccess accesDB = new DataAccess();

            Categorie c1 = new Categorie("ReadTest");
            Assert.IsTrue(c1.Read(), "Le nom de cette catégorie est unique.");
            c1.Create();

            Categorie c2 = new Categorie("ReadTest");
            Assert.IsFalse(c2.Read(), "Le nom de cette catégorie n'est pas unique.");

            accesDB.SetData($"DELETE FROM CATEGORIE_MATERIEL WHERE nomCategorie='{c1.NomCategorie}'");
        }

        [TestMethod()]
        public void UpdateTest()
        {
            DataAccess accesDB = new DataAccess();

            Categorie c1 = new Categorie("UpdateTest");
            c1.Create();

            Categorie c2 = c1.FindAll().ToList().Find(x => x.NomCategorie == c1.NomCategorie);
            c2.NomCategorie = "TestUpdate";
            c2.Update();
            
            int nb1 = accesDB.GetData($"SELECT 'X' FROM CATEGORIE_MATERIEL WHERE nomCategorie='{c1.NomCategorie}'").Rows.Count;
            int nb2 = accesDB.GetData($"SELECT 'X' FROM CATEGORIE_MATERIEL WHERE nomCategorie='{c2.NomCategorie}'").Rows.Count;

            Assert.AreEqual(0, nb1, "Aucune catégorie n'a le nom de UpdateTest.");
            Assert.AreEqual(1, nb2, "Une catégorie a le nom de TestUpdate.");

            accesDB.SetData($"DELETE FROM CATEGORIE_MATERIEL WHERE idCategorie='{c2.IdCategorie}'");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            DataAccess accesDB = new DataAccess();

            Categorie c1 = new Categorie("DeleteTest");
            c1.Create();

            int nb1 = accesDB.GetData("SELECT 'X' FROM CATEGORIE_MATERIEL").Rows.Count;

            c1 = c1.FindAll().ToList().Find(x => x.NomCategorie == c1.NomCategorie);
            c1.Delete();

            int nb2 = accesDB.GetData("SELECT 'X' FROM CATEGORIE_MATERIEL").Rows.Count;

            Assert.AreEqual(nb1 - 1, nb2, "La catégorie a été supprimée de la base de données.");
        }

        [TestMethod()]
        public void FindAllTest()
        {
            DataAccess accesDB = new DataAccess();

            DataTable datas = accesDB.GetData("SELECT 'X' FROM CATEGORIE_MATERIEL");
            ObservableCollection<Categorie> lesCategories = new Categorie().FindAll();

            Assert.AreEqual(datas.Rows.Count, lesCategories.Count, "Toutes les données de la base de données sont dans la liste lesCategories.");
        }
    }
}