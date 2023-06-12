using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Prototype.Métier
{
    public class ApplicationData
    {
        public ObservableCollection<Enseignant> LesEnseignants;
        public ObservableCollection<Categorie> LesCategories;
        public ObservableCollection<Materiel> LesMateriels;
        public ObservableCollection<Attribution> LesAttributions;
        public ApplicationData()
        {
            Enseignant e = new Enseignant();
            LesEnseignants = e.FindAll();
            Categorie c = new Categorie();
            LesCategories = c.FindAll();
            Materiel m = new Materiel();
            LesMateriels = m.FindAll();
            Attribution a = new Attribution();
            LesCategories = c.FindAll();

         
        }
    }
}
