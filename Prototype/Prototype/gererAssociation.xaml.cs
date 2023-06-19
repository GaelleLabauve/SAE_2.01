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
    /// Logique d'interaction pour gererAssociation.xaml
    /// </summary>
    public partial class gererAssociation : Window
    {
        /// <summary>
        /// Creation de la fenetre modal servant a la gestion des attributions.
        /// </summary>
        public gererAssociation()
        {
            InitializeComponent();
        }

        private void btModifier_Click(object sender, RoutedEventArgs e)
        {
            if (lvAttribution.SelectedItem is null)
            {
                MessageBox.Show("Sélectionner une attribution pour modifier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Attribution a = (Attribution)lvAttribution.SelectedItem;

                // Affiche le formulaire de saisie
                AfficheForm();

                // Renseigne les champs
                tbCommentaire.Text = a.CommentaireAttribution;
                datePickerDate.SelectedDate = a.DateAttribution;
                cbMateriel.SelectedItem = a.UnMateriel;
                cbNomPrenomEns.SelectedItem = a.UnEnseignant;
            }
        }

        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Affiche le formulaire de saisie
            AfficheForm();

            // Réinitialise la sélection
            lvAttribution.SelectedIndex = -1;
        }

        private void btSuppr_Click(object sender, RoutedEventArgs e)
        {
            if (lvAttribution.SelectedItem is null)
            {
                MessageBox.Show("Sélectionner une attribution pour supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Attribution attribution = (Attribution)lvAttribution.SelectedItem;
                MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment supprimer l'attribution :  \n Materiel : {attribution.UnMateriel.NomMateriel} \n Enseignant : {attribution.UnEnseignant.NomPrenom} \n Date : {attribution.DateAttribution} \n Commentaire :{attribution.CommentaireAttribution}", "Suppression Attribution", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    ((ApplicationData)this.DataContext).Remove(attribution);
                    MessageBox.Show("Suppression réalisée avec succès !", "Suppression attribution", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            // Réinitialise et cache les champs
            Reset();

            // Réinitialisation de la sélection
            lvAttribution.SelectedIndex = -1;
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (!(VerifChampsVides() || VerifStyle()))
            {
                Attribution a = new Attribution();
                String message, titre;

                if (lvAttribution.SelectedIndex != -1)
                {
                    a = (Attribution)lvAttribution.SelectedItem;
                    message = $"Voulez-vous vraiment modifier l'attribution :  \n Materiel : {a.UnMateriel.NomMateriel} \n Enseignant : {a.UnEnseignant.NomPrenom} \n Date : {a.DateAttribution} \n Commentaire :{a.CommentaireAttribution}";
                    titre = "Modification attribution";
                }
                else
                {
                    message = $"Voulez-vous vraiment supprimer l'attribution :  \n Materiel : {((Materiel)cbMateriel.SelectedItem).NomMateriel} \n Enseignant : {((Enseignant)cbNomPrenomEns.SelectedItem).NomPrenom} \n Date : {datePickerDate.SelectedDate.Value.Date.ToString("yyyy-mm-dd")} \n Commentaire :{tbCommentaire.Text}";
                    titre = "Ajout attribution";
                }

                MessageBoxResult result = MessageBox.Show(message, titre, MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);

                if (result == MessageBoxResult.OK)
                {
                    // Modification des informations
                    a.FK_IdMateriel = ((Materiel)cbMateriel.SelectedItem).IdMateriel;
                    a.FK_IdPersonnel = ((Enseignant)cbNomPrenomEns.SelectedItem).IdPersonnel;
                    a.DateAttribution = (DateTime)datePickerDate.SelectedDate;
                    a.CommentaireAttribution = tbCommentaire.Text;

                    if (lvAttribution.SelectedIndex != -1)
                    {
                        // Modification de l'enseignant
                        ((ApplicationData)this.DataContext).Update(a);

                        // Message de confirmation
                        MessageBox.Show("Attribution modifiée !", "Modification attribution", MessageBoxButton.OK);
                    }
                    else
                    {
                        // Création de l'enseignant dans la base de données et ajout à la liste LesEnseignants
                        ((ApplicationData)this.DataContext).Add(a);

                        // Message de confirmation
                        MessageBox.Show("Attribution ajoutée !", "Ajout attribution", MessageBoxButton.OK);
                    }

                    // Rafraîchissement de la ListeView
                    lvAttribution.ItemsSource = ((ApplicationData)this.DataContext).LesAttributions;
                }

                // Réinitialisation de la sélection
                lvAttribution.SelectedIndex = -1;

                // Reset des champs de saisie
                Reset();
            }
            else
            {
                MessageBox.Show("Veuillez renseigner les champs obligatoires de manière conforme.", "Erreur modification attribution", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbCommentaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 1000)
            {
                // Application du style avec bordures rouges
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Ajout du message 
                lbCommentaireError.Content = "Trop long ( > 1000 caractères)";
            }
            else
            {
                // Suppression du style (remplacement par un style par défaut)
                tb.Style = new Style();

                // Réinitialisation du label
                lbCommentaireError.Content = "";
            }
        }

        private bool VerifChampsVides()
        {
            bool result = false;

            if (String.IsNullOrWhiteSpace(tbCommentaire.Text))
            {
                // Application du style avec bordures rouges
                tbCommentaire.Style = (Style)Application.Current.FindResource("Obligatoire");
                lbCommentaireError.Content = "Commentaire manquant";
                result = true;
            }
            if (cbMateriel.SelectedIndex == -1)
            {
                lbMaterielError.Content = "Matériel manquant";
                result = true;
            }
            if (cbNomPrenomEns.SelectedIndex == -1)
            {
                lbEnseignantError.Content = "Enseignant manquant";
                result = true;
            }
            if (datePickerDate.SelectedDate is null)
            {
                lbDateError.Content = "Date manquante";
                result = true;
            }

            return result;
        }

        private bool VerifStyle()
        {
            if (tbCommentaire.Style == (Style)Application.Current.FindResource("Obligatoire"))
            {
                return true;
            }
            if (!String.IsNullOrWhiteSpace(lbDateError.Content.ToString()))
            {
                return true;
            }
            if (!String.IsNullOrWhiteSpace(lbEnseignantError.Content.ToString()))
            {
                return true;
            }
            if (!String.IsNullOrWhiteSpace(lbMaterielError.Content.ToString()))
            {
                return true;
            }
            return false;
        }

        private void Reset()
        {
            // Réinitialise et cache le champs de saisie
            tbCommentaire.Text = "";
            cbMateriel.SelectedIndex = -1;
            cbNomPrenomEns.SelectedIndex = -1;
            datePickerDate.SelectedDate = null;

            // Réinitialisation des styles
            tbCommentaire.Style = new Style();

            // Suppression des messages d'erreur
            lbCommentaireError.Content = "";
            lbDateError.Content = "";
            lbEnseignantError.Content = "";
            lbMaterielError.Content = "";

            // Cache le formulaire de saisie
            spForm.Visibility = Visibility.Hidden;

            // Cache les boutons supprimer,modifier,ajouter
            gridBouton.Visibility = Visibility.Visible;
        }

        private void AfficheForm()
        {
            // Réinitalise les champs et labels d'erreur
            Reset();

            // Affiche le champs de saisie
            spForm.Visibility = Visibility.Visible;

            // Affichage des boutons supprimer,modifier,ajouter
            gridBouton.Visibility = Visibility.Hidden;
        }

        private void datePickerDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerDate.SelectedDate != null)
            {
                lbDateError.Content = "";
            }
        }
    }
}
