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
        /// Supprime (apres vérification) une categorie de la base de donnees et de la liste LesCategories.
        /// </summary>

        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            // Suppression de la categorie dans la base de données et dans la liste LesCategories
            if(lvCategorie.SelectedItem is null)
            {
                MessageBox.Show("Sélectionner une catégorie pour supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Categorie categorie = (Categorie)lvCategorie.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment supprimer {categorie.NomCategorie} de la liste des categories ?", "Suppression catégorie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ((ApplicationData)this.DataContext).Remove((Categorie)lvCategorie.SelectedItem);
                    
                    lvCategorie.SelectedIndex = -1;

                    MessageBox.Show("Suppression réaliser avec succès !", "Suppression catégorie", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        /// <summary>
        /// Ajoute (apres verification) une categorie a la base de donnees et de la liste LesCategories.
        /// </summary>
        private void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Affiche le formulaire de saisie
            AfficheForm();

            // Réinitialise la sélection
            lvCategorie.SelectedIndex = -1;
        }

        /// <summary>
        /// Modifie (apres verification) une categorie de la base de donnees et de la liste LesCategories.
        /// </summary>
        private void btModifier_Click(object sender, RoutedEventArgs e)
        {
            if (lvCategorie.SelectedItem is null)
            {
                MessageBox.Show("Sélectionner une catégorie pour modifier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Categorie c = (Categorie)lvCategorie.SelectedItem;

                // Affiche le formulaire de saisie
                AfficheForm();

                // Renseigne les champs avec les infos de l'enseignant sélectionné
                tbCategorie.Text = c.NomCategorie;

                // Réinitialise le style et le message d'erreur
                tbCategorie.Style = new Style();
                lbNomCateError.Content = "";
            }
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            // Réinitialise et cache les champs
            Reset();

            // Réinitialisation de la sélection
            lvCategorie.SelectedIndex = -1;
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (!(Verif_TextBoxVides() || VerifStyle()))
            {
                Categorie c = new Categorie();
                String message, titre;

                if (lvCategorie.SelectedIndex != -1)
                {
                    c = (Categorie)lvCategorie.SelectedItem;
                    message = $"Voulez-vous vraiment modifier la catégorie {c.NomCategorie} ?";
                    titre = "Modification catégorie";
                }
                else
                {

                    message = $"Voulez-vous vraiment ajouter la catégorie {tbCategorie.Text} ?";
                    titre = "Ajout catégorie";
                }

                MessageBoxResult result = MessageBox.Show(message, titre, MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);

                if (result == MessageBoxResult.OK)
                {
                    // Modification/Ajout des informations
                    c.NomCategorie = tbCategorie.Text;

                    if (lvCategorie.SelectedIndex != -1)
                    {
                        // Modification de l'enseignant
                        ((ApplicationData)this.DataContext).Update(c);

                        // Message de confirmation
                        MessageBox.Show("Catégorie modifié !", "Modification catégorie", MessageBoxButton.OK);
                    }
                    else
                    {
                        // Création de l'enseignant dans la base de données et ajout à la liste LesEnseignants
                        ((ApplicationData)this.DataContext).Add(c);

                        // Message de confirmation
                        MessageBox.Show("Catégorie ajouté !", "Ajout catégorie", MessageBoxButton.OK);
                    }

                    // Rafraîchissement de la ListeView
                    lvCategorie.ItemsSource = ((ApplicationData)this.DataContext).LesCategories;
                }

                // Réinitialisation de la sélection
                lvCategorie.SelectedIndex = -1;

                // Reset des champs de saisie
                Reset();
            }
            else
            {
                MessageBox.Show("Veuillez renseigner les champs obligatoires de manière conforme.", "Erreur modification catégorie", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbCategorie_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbCategorie.Text.Length > 50)
            {
                // Application du style avec bordures rouges
                tbCategorie.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Ajout du message 
                lbNomCateError.Content = "Trop long ( > 50 caractères)";
            }
            else
            {
                // Suppression du style (remplacement par un style par défaut)
                tbCategorie.Style = new Style();

                // Réinitialisation du label
                lbNomCateError.Content = "";
            }
        }

        private bool Verif_TextBoxVides()
        {
            if (String.IsNullOrWhiteSpace(tbCategorie.Text))
            {
                tbCategorie.Style = (Style)Application.Current.FindResource("Obligatoire");
                return true;
            }
            return false;
        }

        private bool VerifStyle()
        {
            if (tbCategorie.Style == (Style)Application.Current.FindResource("Obligatoire"))
            {
                return true;
            }
            return false;
        }

        private void Reset()
        {
            // Réinitialise et cache le champs de saisie
            tbCategorie.Text = "";
            spCategorie.Visibility = Visibility.Hidden;

            // Réinitialisation des styles
            tbCategorie.Style = new Style();

            // Suppression des messages d'erreur
            lbNomCateError.Content = "";

            // Cache les boutons supprimer,modifier,ajouter
            gridBouton.Visibility = Visibility.Visible;

            // Affichage des boutons valider et annuler
            spBouton.Visibility = Visibility.Hidden;
        }

        private void AfficheForm()
        {
            // Réinitalise les champs et labels d'erreur
            Reset();

            // Affiche le champs de saisie
            spCategorie.Visibility = Visibility.Visible;

            // Affichage des boutons supprimer,modifier,ajouter
            gridBouton.Visibility = Visibility.Hidden;

            // Cache les boutons valider et annuler
            spBouton.Visibility = Visibility.Visible;
        }
    }
}
