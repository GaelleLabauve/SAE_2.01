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
            DateTime today = DateTime.Today;
            today = new DateTime(today.Year, today.Month, today.Day);

            Attribution a = new Attribution(1,1,today,"TestCreate");
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

            DateTime today = DateTime.Today;
            today = new DateTime(today.Year, today.Month, today.Day);

            Attribution a1 = new Attribution(1, 1, today, "TestUpdate");
            a1.Create();

            Attribution a2 = a1.FindAll().ToList().Find(x => x.FK_IdPersonnel == a1.FK_IdPersonnel && x.FK_IdMateriel == a1.FK_IdMateriel && x.DateAttribution == a1.DateAttribution);
            a2.CommentaireAttribution = "UpdateTest";
            a2.Update();

            int nb1 = accesDB.GetData($"SELECT 'X' FROM EST_ATTRIBUE WHERE idMateriel='{a2.FK_IdMateriel} ' AND idPersonnel=' {a2.FK_IdPersonnel} ', AND dateAttribution=' {a2.DateAttribution}' AND commentaireAttribution='{a1.CommentaireAttribution}'").Rows.Count;
            int nb2 = accesDB.GetData($"SELECT 'X' FROM EST_ATTRIBUE WHERE idMateriel='{a2.FK_IdMateriel}' AND idPersonnel='{a2.FK_IdPersonnel}', AND dateAttribution='{a2.DateAttribution}' AND commentaireAttribution='{a2.CommentaireAttribution}'").Rows.Count;

            Assert.AreEqual(0, nb1, $"Aucune attribution n'a la clé primaire (1,1,'{today}') et le commentaire 'TestUpdate.");
            Assert.AreEqual(1, nb2, $"Une attribution a la clé primaire (1,1,'{today}') et le commentaire 'UpdateTest.");

            new DataAccess().SetData($"DELETE FROM EST_ATTRIBUE WHERE idMateriel='{a2.FK_IdMateriel}' AND idPersonnel='{a2.FK_IdPersonnel}', AND dateAttribution='{a2.DateAttribution}'");
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