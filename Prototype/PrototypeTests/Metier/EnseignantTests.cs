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
    public class EnseignantTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Enseignant_AvecPrenomCaractereSuperieurALimite_Test()
        {
            string prenom = "";
            for (int i = 0; i <= Enseignant.LIMITE_TAILLE_CARACTERE_NOMPRENOM; i++)
            {
                prenom += "a";
            }
            Enseignant e = new Enseignant(1, "bla@bla", "Christophe", prenom);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Enseignant_AvecNomCaractereSuperieurALimite_Test()
        {
            string nom = "";
            for (int i = 0; i <= Enseignant.LIMITE_TAILLE_CARACTERE_NOMPRENOM; i++)
            {
                nom += "a";
            }
            Enseignant e = new Enseignant(1, "bla@bla", nom, "Christophe");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Enseignant_AvecNomNull_Test()
        {
            string nom = "";
            Enseignant e = new Enseignant(1, "bla@bla", nom, "Christophe");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Enseignant_AvecPrenomNull_Test()
        {
            string prenom = "";
            Enseignant e = new Enseignant(1, "bla@bla", "Christophe", prenom);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Enseignant_AvecIdNegatif_Test()
        {
            Enseignant e = new Enseignant(-1, "christJesus@bla", "Christophe", "Jesus");
        }


        [TestMethod()]
        public void CreateTest()
        {
            Enseignant e = new Enseignant("enseignant@test", "enseignant", "test");
            e.Create();

            Assert.AreEqual(1, new DataAccess().GetData($"SELECT * FROM PERSONNEL WHERE emailPersonnel='{e.EmailPersonnel}'").Rows.Count, "L'enseignant a été insérée dans la base de données.");

            new DataAccess().SetData($"DELETE FROM PERSONNEL WHERE emailPersonnel='{e.EmailPersonnel}'");
        }

        [TestMethod()]
        public void ReadTest()
        {
            Enseignant e1 = new Enseignant("enseignant@test", "enseignant", "test");
            Assert.IsTrue(e1.Read(), "L'email de cet enseignant est unique.");
            e1.Create();

            Enseignant e2 = new Enseignant("enseignant@test", "enseignant2", "test2");
            Assert.IsFalse(e2.Read(), "L'email de cet enseignant n'est pas unique.");

            new DataAccess().SetData($"DELETE FROM PERSONNEL WHERE emailPersonnel='{e1.EmailPersonnel}'");
        }

        [TestMethod()]
        public void UpdateTest()
        {
            DataAccess accesDB = new DataAccess();

            Enseignant e1 = new Enseignant("enseignant@test", "enseignant", "test");
            e1.Create();

            Enseignant e2 = e1.FindAll().ToList().Find(x => x.EmailPersonnel == e1.EmailPersonnel);
            e2.NomPersonnel = "EnseignantTest";
            e2.PrenomPersonnel = "TestEnseignant";
            e2.EmailPersonnel = "enseignant@updateTest";
            e2.Update();

            int nb1 = accesDB.GetData($"SELECT 'X' FROM PERSONNEL WHERE nomPersonnel='{e1.NomPersonnel}' AND prenomPersonnel='{e1.PrenomPersonnel}' AND emailPersonnel='{e1.EmailPersonnel}'").Rows.Count;
            int nb2 = accesDB.GetData($"SELECT 'X' FROM PERSONNEL WHERE nomPersonnel='{e2.NomPersonnel}' AND prenomPersonnel='{e2.PrenomPersonnel}' AND emailPersonnel='{e2.EmailPersonnel}'").Rows.Count;

            Assert.AreEqual(0, nb1, "Aucune enseignant n'a le nom de 'enseignant', le prénom 'test' et l'email 'enseignant@test'.");
            Assert.AreEqual(1, nb2, "Un enseignant a le nom de 'EnseignantTest', le prénom 'TestEnseignant' et l'email 'enseignant@updateTest'.");

            accesDB.SetData($"DELETE FROM PERSONNEL WHERE idPersonnel='{e2.IdPersonnel}'");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            DataAccess accesDB = new DataAccess();

            Enseignant e1 = new Enseignant("enseignant@test", "enseignant", "test");
            e1.Create();

            int nb1 = accesDB.GetData("SELECT 'X' FROM PERSONNEL").Rows.Count;

            e1 = e1.FindAll().ToList().Find(x => x.EmailPersonnel == e1.EmailPersonnel);
            e1.Delete();

            int nb2 = accesDB.GetData("SELECT 'X' FROM PERSONNEL").Rows.Count;

            Assert.AreEqual(nb1 - 1, nb2, "L'enseignant a été supprimée de la base de données.");
        }

        [TestMethod()]
        public void FindAllTest()
        {
            DataAccess accesDB = new DataAccess();

            DataTable datas = accesDB.GetData("SELECT 'X' FROM PERSONNEL");
            ObservableCollection<Enseignant> lesEnseignants = new Enseignant().FindAll();

            Assert.AreEqual(datas.Rows.Count, lesEnseignants.Count, "Toutes les données de la base de données sont dans la liste lesEnseignants.");
        }
    }
}