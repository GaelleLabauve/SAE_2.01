using System;
using System.Collections.ObjectModel;

namespace Prototype.M�tier
{
    public class ApplicationData
    {
        public ObservableCollection<Enseignant> LesEnseignants;
        public ObservableCollection<Categorie> LesCategories;
        public ObservableCollection<Materiel> LesMateriel;
        public ObservableCollection<Attribution> LesAttributions;
        public ApplicationData()
        {
        }
    }
}
