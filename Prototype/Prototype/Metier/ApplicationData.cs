using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows;

namespace Prototype.Metier
{
    public class ApplicationData
    {
        public ObservableCollection<Enseignant> LesEnseignants { get; set; }
        public ObservableCollection<Categorie> LesCategories { get; set; }
        public ObservableCollection<Materiel> LesMateriels { get; set; }
        public ObservableCollection<Attribution> LesAttributions { get; set; }
        public ApplicationData()
        {
            Enseignant e = new Enseignant();
            LesEnseignants = e.FindAll();

            Categorie c = new Categorie();
            LesCategories = c.FindAll();

            Materiel m = new Materiel();
            LesMateriels = m.FindAll();

            Attribution a = new Attribution();
            LesAttributions = a.FindAll();

            // Catégorie et attributions d'un matériel
            foreach (Materiel unMateriel in LesMateriels.ToList<Materiel>())
            {
                unMateriel.UneCategorie = LesCategories.ToList<Categorie>().Find(c => c.IdCategorie == unMateriel.FK_IdCategorie);
                unMateriel.LesAttributions = new ObservableCollection<Attribution>(LesAttributions.ToList<Attribution>().FindAll(a => a.FK_IdMateriel == unMateriel.IdMateriel));
            }

            // Matériels d'une catégorie
            foreach (Categorie uneCategorie in LesCategories.ToList<Categorie>())
            {
                uneCategorie.LesMateriels = new ObservableCollection<Materiel>(LesMateriels.ToList<Materiel>().FindAll(m => m.FK_IdCategorie == uneCategorie.IdCategorie));
            }

            // Attributions d'un enseignant
            foreach (Enseignant unEnseignant in LesEnseignants.ToList<Enseignant>())
            {
                unEnseignant.LesAttributions = new ObservableCollection<Attribution>(LesAttributions.ToList<Attribution>().FindAll(a => a.FK_IdPersonnel == unEnseignant.IdPersonnel));
            }

            // Enseignant et matériel d'une attribution
            foreach (Attribution uneAttribution in LesAttributions.ToList<Attribution>())
            {
                uneAttribution.UnEnseignant = LesEnseignants.ToList<Enseignant>().Find(e => e.IdPersonnel == uneAttribution.FK_IdPersonnel);
                uneAttribution.UnMateriel = LesMateriels.ToList<Materiel>().Find(m => m.IdMateriel == uneAttribution.FK_IdMateriel);
                
            }

        }

        public void Add(object obj)
        {
            if (obj is Categorie)
            {
                ((Categorie)obj).Create();
                this.LesCategories.Add((Categorie)obj);
            }
            else if (obj is Attribution)
            {
                ((Attribution)obj).Create();
                this.LesAttributions.Add((Attribution)obj);
            }
            else if (obj is Enseignant)
            {
                ((Enseignant)obj).Create();
                this.LesEnseignants.Add((Enseignant)obj);
            }
            else if (obj is Materiel)
            {
                ((Materiel)obj).Create();
                this.LesMateriels.Add((Materiel)obj);
            }
        }
        public void Remove(object obj)
        {
            if (obj is Categorie)
            {
                ((Categorie)obj).Delete();
                this.LesCategories.Remove((Categorie)obj);
            }
            else if (obj is Attribution)
            {
                ((Attribution)obj).Delete();
                this.LesAttributions.Remove((Attribution)obj);
            }
            else if (obj is Enseignant)
            {
                ((Enseignant)obj).Delete();
                this.LesEnseignants.Remove((Enseignant)obj);
            }
            else if (obj is Materiel)
            {
                ((Materiel)obj).Delete();
                this.LesMateriels.Remove((Materiel)obj);
            }
        }
    }
}
