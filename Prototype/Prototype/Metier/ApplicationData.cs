using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows;

namespace Prototype.Metier
{
    /// <summary>
    /// Entre toutes les données de la base de donnée dans 4 ObservableCollection différentes.
    /// Sert lors du binding.
    /// </summary>
    public class ApplicationData
    {
        /// <summary>
        /// Obtient ou définit l'ObservableCollection des Enseignants –
        /// </summary>
        public ObservableCollection<Enseignant> LesEnseignants { get; set; }

        /// <summary>
        /// Obtient ou définit l'ObservableCollection des Catégories –
        /// </summary>
        public ObservableCollection<Categorie> LesCategories { get; set; }

        /// <summary>
        /// Obtient ou définit l'ObservableCollection des Materiels –
        /// </summary>
        public ObservableCollection<Materiel> LesMateriels { get; set; }

        /// <summary>
        /// Obtient ou définit l'ObservableCollection des Attributions –
        /// </summary>
        public ObservableCollection<Attribution> LesAttributions { get; set; }

        /// <summary>
        /// Définit toutes les ObservableCollection a partir des fonction des différente classes –
        /// </summary>
        public ApplicationData()
        {
            this.RecupData();
            this.AssociationFK();
        }

        /// <summary>
        /// Récupère les données de la base de données et les insère dans les ObservableCollection
        /// </summary>
        private void RecupData()
        {
            Enseignant e = new Enseignant();
            LesEnseignants = e.FindAll();

            Categorie c = new Categorie();
            LesCategories = c.FindAll();

            Materiel m = new Materiel();
            LesMateriels = m.FindAll();

            Attribution a = new Attribution();
            LesAttributions = a.FindAll();
        }

        /// <summary>
        /// Associe les FK aux objets concernés
        /// </summary>
        private void AssociationFK()
        {
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

        /// <summary>
        /// Ajoute l'obj entré en parametre dans la l'ObservableCollection concerné et dans la base de donnée –
        /// </summary>
        public void Add(object obj)
        {
            if (obj is Categorie)
            {
                ((Categorie)obj).Create();
            }
            else if (obj is Attribution)
            {
                ((Attribution)obj).Create();
            }
            else if (obj is Enseignant)
            {
                ((Enseignant)obj).Create();
            }
            else if (obj is Materiel)
            {
                ((Materiel)obj).Create();
            }
            this.RecupData();
            this.AssociationFK();
        }

        /// <summary>
        /// Supprime l'obj entré en parametre dans la l'ObservableCollection concerné et dans la base de donnée –
        /// </summary>
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
