using Prototype.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Prototype
{
    /// <summary>
    /// Logique d'interaction pour gererCategorie.xaml
    /// </summary>
    public partial class gererCategorie : Window
    {
        public gererCategorie()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Supprime (après vérification) une catégorie de la base de données et de la liste LesCategories.
        /// </summary>

        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            // Suppression de la catégorie dans la base de données et dans la liste LesCatégories
            ((Categorie)lv_categorie.SelectedItem).Delete();
            ((ApplicationData)this.DataContext).Remove((Categorie)lv_categorie.SelectedItem);
            lv_categorie.SelectedIndex = 0;
            
        }

        /// <summary>
        /// Ajoute (après vérification) une catégorie à la base de données et de la liste LesCategories.
        /// </summary>
        private void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Ajoute la catégorie dans la base de données et dans la liste LesCatégories
            ((ApplicationData)DataContext).LesCategories.Insert(0, new Categorie(tbCategorie.Text));
            lv_categorie.SelectedIndex = 0;
            ((Categorie)lv_categorie.SelectedItem).Create();
        }

        /// <summary>
        /// Modifie (après vérification) une catégorie de la base de données et de la liste LesCategories.
        /// </summary>
        private void btModdifier_Click(object sender, RoutedEventArgs e)
        {
            // Modification de la catégorie dans la base de données et dans la liste LesCatégories
            ((Categorie)lv_categorie.SelectedItem).Update();
        }
    }
}
