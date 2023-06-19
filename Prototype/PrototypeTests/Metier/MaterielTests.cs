using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prototype.Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Xml;

namespace Prototype.Metier.Tests
{
    [TestClass()]
    public class MaterielTests
    {
        // unicité de code barre

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Materiel_AvecIdNegatif_Test()
        {
            Materiel m = new Materiel(-2, 1, "nom", "codebarre", "ref");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Materiel_AvecIdCategorieNegatif_Test()
        {
            Materiel m = new Materiel(2,-1, "nom", "codebarre", "ref");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Materiel_AvecNomVide_Test()
        {
            Materiel m = new Materiel(1, "", "codebarre", "ref");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Materiel_AvecCodeBarreVide_Test()
        {
            Materiel m = new Materiel(1, "nom", "", "ref");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Materiel_AvecReferenceVide_Test()
        {
            Materiel m = new Materiel(1, "nom", "codeBarre", "");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Materiel_AvecReferenceDepasseLimiteCaractere_Test()
        {
            string reference= "";
            for (int i=0; i<=Materiel.LIMITE_CARACTERE; i++)
            {
                reference += "a";
            }
            Materiel m = new Materiel(1, "nom", "codeBarre", reference);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Materiel_AvecNomDepasseLimiteCaractere_Test()
        {
            string nom= "";
            for (int i=0; i<=Materiel.LIMITE_CARACTERE; i++)
            {
                nom += "a";
            }
            Materiel m = new Materiel(1, nom, "codeBarre", "bla");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Materiel_AvecCodeBarreDepasseLimiteCaractere_Test()
        {
            string codeBarre= "";
            for (int i=0; i<=Materiel.LIMITE_CARACTERE; i++)
            {
                codeBarre += "a";
            }
            Materiel m = new Materiel(1, "nom", codeBarre, "reference");
        }

        [TestMethod()]
        public void CreateTest()
        {
            Materiel m = new Materiel(1,1,"TestCreate","TestCreate","TestCreate");
            m.Create();

            Assert.AreEqual(1, new DataAccess().GetData($"SELECT * FROM MATERIEL WHERE codeBarreInventaire='{m.CodeBarreInventaire}'").Rows.Count, "Le matériel a été inséré dans la base de données.");

            new DataAccess().SetData($"DELETE FROM MATERIEL WHERE codeBarreInventaire='{m.CodeBarreInventaire}'");
        }

        [TestMethod()]
        public void ReadTest()
        {
            DataAccess accesDB = new DataAccess();

            Materiel m1 = new Materiel(1, 1, "TestRead", "TestRead", "TestRead");
            Assert.IsTrue(m1.Read(), "Le code barre de ce matériel est unique.");
            m1.Create();

            Materiel m2 = new Materiel(1, 1, "Test", "TestRead", "Test");
            Assert.IsFalse(m2.Read(), "Le code barre de ce matériel n'est pas unique.");

            accesDB.SetData($"DELETE FROM MATERIEL WHERE codeBarreInventaire='{m1.CodeBarreInventaire}'");
        }

        [TestMethod()]
        public void UpdateTest()
        {
            DataAccess accesDB = new DataAccess();

            Materiel m1 = new Materiel(1, 1, "TestUpdate", "TestUpdate", "TestUpdate");
            m1.Create();

            Materiel m2 = m1.FindAll().ToList().Find(x => x.CodeBarreInventaire == m1.CodeBarreInventaire);
            m2.NomMateriel = "UpdateTest";
            m2.Update();

            int nb1 = accesDB.GetData($"SELECT 'X' FROM MATERIEL WHERE nomMateriel='{m1.NomMateriel}'").Rows.Count;
            int nb2 = accesDB.GetData($"SELECT 'X' FROM MATERIEL WHERE nomMateriel='{m2.NomMateriel}'").Rows.Count;

            Assert.AreEqual(0, nb1, "Aucun matériel n'a le nom de TestUpdate.");
            Assert.AreEqual(1, nb2, "Un matériel a le nom de UpdateTest.");

            accesDB.SetData($"DELETE FROM MATERIEL WHERE codeBarreInventaire='{m1.CodeBarreInventaire}'");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            DataAccess accesDB = new DataAccess();

            Materiel m1 = new Materiel(1, 1, "TestUpdate", "TestUpdate", "TestUpdate");
            m1.Create();

            int nb1 = accesDB.GetData("SELECT 'X' FROM MATERIEL").Rows.Count;

            m1 = m1.FindAll().ToList().Find(x => x.CodeBarreInventaire == m1.CodeBarreInventaire);
            m1.Delete();

            int nb2 = accesDB.GetData("SELECT 'X' FROM CATEGORIE_MATERIEL").Rows.Count;

            Assert.AreEqual(nb1 - 1, nb2, "La catégorie a été supprimée de la base de données.");
        }

        [TestMethod()]
        public void FindAllTest()
        {
            DataAccess accesDB = new DataAccess();

            DataTable datas = accesDB.GetData("SELECT 'X' FROM MATERIEL");
            ObservableCollection<Materiel> lesMateriels = new Materiel().FindAll();

            Assert.AreEqual(datas.Rows.Count, lesMateriels.Count, "Toutes les données de la base de données sont dans la liste lesMateriels.");
        }
    }
}