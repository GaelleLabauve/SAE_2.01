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
    /// Logique d'interaction pour gererMateriel.xaml
    /// </summary>
    public partial class gererMateriel : Window
    {
        /// <summary>
        /// Creation de la fenetre modal servant a la gestion des materiels.
        /// </summary>
        public gererMateriel()
        {
            InitializeComponent();
        }

        private void bt_ajouter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbMateriel.Text) || string.IsNullOrEmpty(tbCodeBarre.Text) || string.IsNullOrEmpty(tbRefCons.Text) || cbCategorie.SelectedItem is null)
            {
                MessageBox.Show("Remplir tous les champs !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Materiel m = new Materiel(((Categorie)cbCategorie.SelectedItem).IdCategorie, tbMateriel.Text, tbCodeBarre.Text, tbRefCons.Text);
                ((ApplicationData)DataContext).Add(m);
                lvMateriel.ItemsSource = ((ApplicationData)this.DataContext).LesMateriels;
                MessageBox.Show("Ajout réaliser avec succés !", "Ajouter Materiel", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void lv_materiel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvMateriel.SelectedItem != null)
            {

                foreach (Categorie i in cbCategorie.Items)
                {
                    if (i.NomCategorie == ((Materiel)lvMateriel.SelectedItem).UneCategorie.NomCategorie)
                        cbCategorie.SelectedItem = i;
                }
            }
        }

        private void btSupp_Click(object sender, RoutedEventArgs e)
        {
            if (lvMateriel.SelectedItem is null)
            {
                MessageBox.Show("Séléctionner un materiel pour supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Materiel materiel = (Materiel)lvMateriel.SelectedItem;
                MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment supprimer {materiel.NomMateriel} de la liste des materiels ?", "Suppression Categorie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    ((Materiel)lvMateriel.SelectedItem).Delete();
                    ((ApplicationData)this.DataContext).Remove((Materiel)lvMateriel.SelectedItem);
                    MessageBox.Show("Suppression réaliser avec succés !", "Suppression Materiel", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btModif_Click(object sender, RoutedEventArgs e)
        {
            if (lvMateriel.SelectedItem is null)
            {
                MessageBox.Show("Séléctionner un materiel pour modifier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                ((Materiel)lvMateriel.SelectedItem).Update();
                MessageBox.Show("Modification réaliser avec succés !", "Modification Materiel", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void tbCodeBarre_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 100)
            {
                // Application du style avec bordures rouges
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Ajout du message 
                if (tb == tbCodeBarre)
                {
                    lbCodeBarreError.Content = "Trop long ( > 100 caractères)";
                }
            }
            else
            {
                // Suppression du style (remplacement par un style par défaut)
                tb.Style = new Style();

                // Réinitialisation du label
                if (tb == tbCodeBarre)
                {
                    lbCodeBarreError.Content = "";
                }
               
            }
        }

        private void tbRefCons_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbRefCons.Text.Length > 100)
            {
                // Application du style avec bordures rouges
                tbRefCons.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Message d'erreur
                lbRefError.Content = "Trop long ( > 100 caractères)";
            }
            else
            {
                // Suppression du style (remplacement par un style par défaut)
                tbRefCons.Style = new Style();

                // Réinitialisation du label
                lbRefError.Content = "";
            }
        }

        private void tbMateriel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbMateriel.Text.Length > 100)
            {
                // Application du style avec bordures rouges
                tbMateriel.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Message d'erreur 
                lbNomError.Content = "Trop long ( > 100 caractères)";
            }
            else
            {
                // Suppression du style (remplacement par un style par défaut)
                tbMateriel.Style = new Style();

                // Réinitialisation du label
                lbNomError.Content = "";
            }
        }

        private void Reset()
        {
            // Réinitialisation du texte des TextBox
            tbMateriel.Text = "";
            tbRefCons.Text = "";
            tbCodeBarre.Text = "";
            cbCategorie.SelectedIndex = -1;

            // Réinitialisation du style des TextBox
            tbMateriel.Style = new Style();
            tbRefCons.Style = new Style();
            tbCodeBarre.Style = new Style();
            cbCategorie.Style = new Style();

            // Réinitialisation du texte des Label
            lbNomError.Content = " ";
            lbRefError.Content = " ";
            lbCodeBarreError.Content = " ";
            lbCategorieError.Content = " ";

            // Cache les StackPanel
            spNom.Visibility = Visibility.Hidden;
            spRefConstructeur.Visibility = Visibility.Hidden;
            spCodeBarre.Visibility = Visibility.Hidden;
            spCategorie.Visibility = Visibility.Hidden;

            // Cache le boutton annuler
            btAnnuler.Visibility = Visibility.Hidden;
        }

        private void AfficheForm()
        {
            // Affiche les StackPanel
            spNom.Visibility = Visibility.Visible;
            spRefConstructeur.Visibility = Visibility.Visible;
            spCodeBarre.Visibility = Visibility.Visible;
            spCategorie.Visibility = Visibility.Visible;

            // Affichage du boutton annuler
            btAnnuler.Visibility = Visibility.Visible;
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            // Réinitialise et cache les champs
            Reset();

            // Réinitialisation de la sélection
            lvMateriel.SelectedIndex = -1;
        }
    }
}
