using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prototype.Metier;
using System;
using System.Collections.Generic;
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
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindAllTest()
        {
            Assert.Fail();
        }
    }
}