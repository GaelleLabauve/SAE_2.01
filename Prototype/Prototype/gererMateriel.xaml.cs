using Prototype.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (spNom.Visibility == Visibility.Hidden)
            {
                AfficheForm();
            }
            else if (!(VerifChampsVides() || VerifStyle()))
            {
                Materiel m = new Materiel(((Categorie)cbCategorie.SelectedItem).IdCategorie, tbMateriel.Text, tbCodeBarre.Text, tbRefCons.Text);
                ((ApplicationData)DataContext).Add(m);
                lvMateriel.ItemsSource = ((ApplicationData)this.DataContext).LesMateriels;

                MessageBox.Show("Matériel ajouté !", "Ajout matériel", MessageBoxButton.OK);
                Reset();
            }
        }

        private void btSupp_Click(object sender, RoutedEventArgs e)
        {
            if (lvMateriel.SelectedItem is null)
            {
                MessageBox.Show("Veuillez sélectionner un matériel", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Materiel materiel = (Materiel)lvMateriel.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment supprimer {materiel.NomMateriel} de la liste des matériels ?", "Suppression matériel", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ((ApplicationData)this.DataContext).Remove((Materiel)lvMateriel.SelectedItem);
                    MessageBox.Show("Matériel supprimé !", "Suppression matériel", MessageBoxButton.OK);
                }
            }
        }

        private void btModif_Click(object sender, RoutedEventArgs e)
        {
            if (lvMateriel.SelectedItem is null)
            {
                MessageBox.Show("Sélectionner un materiel pour modifier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                ((Materiel)lvMateriel.SelectedItem).Update();
                MessageBox.Show("Modification réalisée avec succès !", "Modification Materiel", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            // Réinitialise et cache les champs
            Reset();

            // Réinitialisation de la sélection
            lvMateriel.SelectedIndex = -1;
        }

        private void lvMateriel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMateriel.SelectedItem != null)
            {

                foreach (Categorie i in cbCategorie.Items)
                {
                    if (i.NomCategorie == ((Materiel)lvMateriel.SelectedItem).UneCategorie.NomCategorie)
                        cbCategorie.SelectedItem = i;
                }
            }
        }

        private void tbCodeBarre_TextChanged(object sender, TextChangedEventArgs e)
        {
            Materiel materiel = new Materiel();
            materiel.CodeBarreInventaire = tbCodeBarre.Text;

            if (tbCodeBarre.Text.Length <= 100 && materiel.Read())
            {
                // Suppression du style (remplacement par un style par défaut)
                tbCodeBarre.Style = new Style();

                // Suppression du message d'erreur
                lbCodeBarreError.Content = "";
            }
            else
            {
                // Application du style avec bordures rouges
                tbCodeBarre.Style = (Style)Application.Current.FindResource("Obligatoire");

                if (tbCodeBarre.Text.Length > 100)
                {
                    // Message d'erreur
                    lbCodeBarreError.Content = "Trop long ( > 100 caractères)";
                }

                if (!materiel.Read())
                {
                    String content = lbCodeBarreError.Content.ToString();

                    // Message d'erreur
                    if (!content.Contains("Ce code barre existe déjà"))
                    {
                        lbCodeBarreError.Content += "\tCe code barre existe déjà";
                    }
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

                // Suppression du message d'erreur
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

                // Suppression du message d'erreur
                lbNomError.Content = "";
            }
        }

        private void cbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategorie.SelectedItem != null)
            {
                lbCategorieError.Content = "";
            }
        }

        private bool VerifChampsVides()
        {
            bool result = false;

            if (String.IsNullOrWhiteSpace(tbMateriel.Text))
            {
                tbMateriel.Style = (Style)Application.Current.FindResource("Obligatoire");
                result = true;
            }
            if (String.IsNullOrWhiteSpace(tbCodeBarre.Text))
            {
                tbCodeBarre.Style = (Style)Application.Current.FindResource("Obligatoire");
                result = true;
            }
            if (String.IsNullOrWhiteSpace(tbRefCons.Text))
            {
                tbRefCons.Style = (Style)Application.Current.FindResource("Obligatoire");
                result = true;
            }
            if (cbCategorie.SelectedIndex == -1)
            {
                lbCategorieError.Content = "Catégorie manquante";
                result = true;
            }

            if (result)
            {
                MessageBox.Show("Veuillez renseigner tous les champs !", "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }

        private bool VerifStyle()
        {
            if (tbMateriel.Style == (Style)Application.Current.FindResource("Obligatoire"))
            {
                return true;
            }
            if (tbCodeBarre.Style == (Style)Application.Current.FindResource("Obligatoire"))
            {
                return true;
            }
            if (tbRefCons.Style == (Style)Application.Current.FindResource("Obligatoire"))
            {
                return true;
            }

            return false;
        }

        private void Reset()
        {
            // Réinitialisation des textes et de la sélection de la ComboBox
            tbMateriel.Text = "";
            tbRefCons.Text = "";
            tbCodeBarre.Text = "";
            cbCategorie.SelectedIndex = -1;

            // Réinitialisation des styles
            tbMateriel.Style = new Style();
            tbRefCons.Style = new Style();
            tbCodeBarre.Style = new Style();
            lbCategorieError.Style = new Style();

            // Suppression des messages d'erreur
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
    }
}
