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