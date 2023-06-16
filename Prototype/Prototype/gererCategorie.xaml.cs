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
            if(lv_categorie.SelectedItem is null)
            {
                MessageBox.Show("Séléctionner une catégorie pour supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                Categorie categorie = (Categorie)lv_categorie.SelectedItem;
                MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment supprimer {categorie.NomCategorie} de la liste des categories ?", "Suppression Categorie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    ((Categorie)lv_categorie.SelectedItem).Delete();            
                    ((ApplicationData)this.DataContext).Remove((Categorie)lv_categorie.SelectedItem);            
                    lv_categorie.SelectedIndex = 0;
                    MessageBox.Show("Suppression réaliser avec succés !", "Suppression Categorie", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
        }

        /// <summary>
        /// Ajoute (après vérification) une catégorie à la base de données et de la liste LesCategories.
        /// </summary>
        private void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Ajoute la catégorie dans la base de données et dans la liste LesCatégories
            if(string.IsNullOrEmpty(tbCategorie.Text))
            {
                MessageBox.Show("Remplir le champs !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

            ((ApplicationData)DataContext).LesCategories.Insert(0, new Categorie(tbCategorie.Text));
            lv_categorie.SelectedIndex = 0;
            ((Categorie)lv_categorie.SelectedItem).Create();
            MessageBox.Show("Ajout réaliser avec succés !", "Ajout Categorie", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Modifie (après vérification) une catégorie de la base de données et de la liste LesCategories.
        /// </summary>
        private void btModdifier_Click(object sender, RoutedEventArgs e)
        {
            if (lv_categorie.SelectedItem is null)
            {
                MessageBox.Show("Séléctionner une catégorie pour modifier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                // Modification de la catégorie dans la base de données et dans la liste LesCatégories
                ((Categorie)lv_categorie.SelectedItem).Update();
                MessageBox.Show("Modification réaliser avec succés !", "Modification Categorie", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void tbCategorie_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 50)
            {
                // Application du style avec bordures rouges
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Ajout du message 
                if (tb == tbCategorie)
                {
                    lbNomCateError.Content = "Trop long ( > 50 caractères)";
                }
            }

            else
            {
                // Suppression du style (remplacement par un style par défaut)
                tb.Style = new Style();

                // Réinitialisation du label
                if (tb == tbCategorie)
                {
                    lbNomCateError.Content = "";
                }

            }
        }
    }
}
