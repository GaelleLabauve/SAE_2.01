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

        /// <summary>
        /// Ajouter 
        /// </summary>
        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Affiche le formulaire de saisie
            AfficheForm();

            // Réinitialisation de la sélection
            lvMateriel.SelectedIndex = -1;
        }

        private void btSupp_Click(object sender, RoutedEventArgs e)
        {
            if (lvMateriel.SelectedItem is null)
            {
                // Message d'erreur
                MessageBox.Show("Veuillez sélectionner un matériel", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Materiel materiel = (Materiel)lvMateriel.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment supprimer {materiel.NomMateriel} de la liste des matériels ?", "Suppression matériel", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Suppression du matériel
                    ((ApplicationData)this.DataContext).Remove(materiel);

                    // Réinitialisation de la sélection
                    lvMateriel.SelectedIndex = -1;

                    // Message de confirmation
                    MessageBox.Show("Matériel supprimé !", "Suppression matériel", MessageBoxButton.OK);
                }
            }
        }

        private void btModif_Click(object sender, RoutedEventArgs e)
        {
            if (lvMateriel.SelectedItem is null)
            {
                MessageBox.Show("Veuillez sélectionner un matériel", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Materiel m = (Materiel)lvMateriel.SelectedItem;

                // Affichage des champs de saisie
                AfficheForm();

                // Renseignement des champs en fonction du matériel sélectionné
                tbMateriel.Text = m.NomMateriel;
                tbCodeBarre.Text = m.CodeBarreInventaire;
                tbRefCons.Text = m.ReferenceConstructeurMateriel;
                cbCategorie.SelectedItem = m.UneCategorie;
            }
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            // Réinitialise et cache les champs
            Reset();

            // Réinitialisation de la sélection
            lvMateriel.SelectedIndex = -1;
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (!(VerifChampsVides() || VerifStyle()))
            {
                Materiel m = new Materiel();
                String message, titre;

                if (lvMateriel.SelectedIndex != -1)
                {
                    m = (Materiel)lvMateriel.SelectedItem;
                    message = $"Voulez-vous vraiment modifier le matériel {m.NomMateriel} ({m.UneCategorie.NomCategorie}) ?";
                    titre = "Modification matériel";
                } else
                {
                    message = $"Voulez-vous vraiment ajouter le matériel {tbMateriel} ({((Categorie)cbCategorie.SelectedItem).NomCategorie}) ?";
                    titre = "Ajout matériel";
                }

                MessageBoxResult result = MessageBox.Show(message, titre, MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);

                if (result == MessageBoxResult.OK)
                {
                    // Modification des informations
                    m.NomMateriel = tbMateriel.Text;
                    m.CodeBarreInventaire = tbCodeBarre.Text;
                    m.ReferenceConstructeurMateriel = tbRefCons.Text;
                    m.FK_IdCategorie = ((Categorie)cbCategorie.SelectedItem).IdCategorie;

                    if (lvMateriel.SelectedIndex != -1)
                    {
                        // Modification de l'enseignant
                        ((ApplicationData)this.DataContext).Update(m);

                        // Message de confirmation
                        MessageBox.Show("Matériel modifié !", "Modification matériel", MessageBoxButton.OK);
                    }
                    else
                    {
                        // Création de l'enseignant dans la base de données et ajout à la liste LesEnseignants
                        ((ApplicationData)this.DataContext).Add(m);

                        // Message de confirmation
                        MessageBox.Show("Matériel ajouté !", "Ajout matériel", MessageBoxButton.OK);
                    }

                    // Rafraîchissement de la ListeView
                    lvMateriel.ItemsSource = ((ApplicationData)this.DataContext).LesMateriels;
                }

                // Réinitialisation de la sélection
                lvMateriel.SelectedIndex = -1;

                // Reset des champs de saisie
                Reset();
            }
            else
            {
                MessageBox.Show("Veuillez renseigner les champs obligatoires de manière conforme.", "Erreur modification matériel", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
            String codeBarre = tbCodeBarre.Text;

            Materiel materiel = new Materiel();
            materiel.CodeBarreInventaire = codeBarre;

            if ((lvMateriel.SelectedItem != null && ((Materiel)lvMateriel.SelectedItem).CodeBarreInventaire == codeBarre) || (codeBarre.Length <= 100 && materiel.Read()))
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

                if (codeBarre.Length > 100)
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

            // Affiche les boutons supprimer,modifier,ajouter
            gridBouton.Visibility = Visibility.Visible;

            // Cache les boutons annuler,valider
            spBouton.Visibility = Visibility.Hidden;
        }

        private void AfficheForm()
        {
            // Réinitalise les champs et labels d'erreur
            Reset();

            // Affiche les StackPanel
            spNom.Visibility = Visibility.Visible;
            spRefConstructeur.Visibility = Visibility.Visible;
            spCodeBarre.Visibility = Visibility.Visible;
            spCategorie.Visibility = Visibility.Visible;

            // Cache les boutons supprimer,modifier,ajouter
            gridBouton.Visibility = Visibility.Hidden;

            // Affiche les boutons annuler,valider
            spBouton.Visibility = Visibility.Visible;
        }
    }
}
