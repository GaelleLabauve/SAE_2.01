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
    public class AttributionTests
    {
        [TestMethod()]
        public void AttributionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AttributionTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Attribution a = new Attribution(1,1,DateTime.Today,"TestCreate");
            a.Create();

            Assert.AreEqual(1, new DataAccess().GetData($"SELECT * FROM EST_ATTRIBUE WHERE idMateriel='{a.FK_IdMateriel}' AND idPersonnel='{a.FK_IdPersonnel}', AND dateAttribution='{a.DateAttribution}'").Rows.Count, "L'attribution a été inséré dans la base de données.");

            new DataAccess().SetData($"DELETE FROM EST_ATTRIBUE WHERE idMateriel='{a.FK_IdMateriel}' AND idPersonnel='{a.FK_IdPersonnel}', AND dateAttribution='{a.DateAttribution}'");
        }

        [TestMethod()]
        public void ReadTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            DataAccess accesDB = new DataAccess();

            Attribution a1 = new Attribution(1, 1, DateTime.Today, "TestUpdate");
            a1.Create();

            a1 = a1.FindAll().ToList().Find(x => x.FK_IdPersonnel == a1.FK_IdPersonnel && x.FK_IdMateriel == a1.FK_IdMateriel && x.DateAttribution == a1.DateAttribution);
            a1.CommentaireAttribution = "UpdateTest";
            a1.Update();

            int nb1 = accesDB.GetData($"SELECT 'X' FROM MATERIEL WHERE nomMateriel='{m1.NomMateriel}'").Rows.Count;
            int nb2 = accesDB.GetData($"SELECT 'X' FROM MATERIEL WHERE nomMateriel='{a1.NomMateriel}'").Rows.Count;

            Assert.AreEqual(0, nb1, "Aucun matériel n'a le nom de TestUpdate.");
            Assert.AreEqual(1, nb2, "Un matériel a le nom de UpdateTest.");

            accesDB.SetData($"DELETE FROM MATERIEL WHERE codeBarreInventaire='{m1.CodeBarreInventaire}'");
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