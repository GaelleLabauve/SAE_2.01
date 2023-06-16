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