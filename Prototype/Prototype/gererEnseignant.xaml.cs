using Microsoft.VisualBasic;
using Prototype.Metier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
    /// Logique d'interaction pour gererEnseignant.xaml
    /// </summary>
    public partial class gererEnseignant : Window
    {
        public gererEnseignant()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ajoute (apres verification) un enseignant a la base de donnees et a la liste LesEnseignants.
        /// </summary>
        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Affiche le formulaire de saisie
            AfficheForm();

            // Réinitialisation de la sélection
            lvEnseignant.SelectedIndex = -1;
        }

        /// <summary>
        /// Modification (apres confirmation) de l'item selectionne dans la ListView.
        /// </summary>
        private void btModif_Click(object sender, RoutedEventArgs e)
        {
            if (lvEnseignant.SelectedIndex != -1)
            {
                Enseignant enseignant = (Enseignant)lvEnseignant.SelectedItem;
                
                // Affiche le formulaire de saisie
                AfficheForm();

                // Renseigne les champs avec les infos de l'enseignant sélectionné
                tbNom.Text = enseignant.NomPersonnel;
                tbPrenom.Text = enseignant.PrenomPersonnel;
                tbMail.Text = enseignant.EmailPersonnel;

                // Réinitialise le style et le message d'erreur
                tbMail.Style = new Style();
                lbMailError.Content = "";
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un enseignant pour le modifier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Suppression (apres confirmation) de l'item selectionne dans la ListView.
        /// </summary>
        private void btSuppr_Click(object sender, RoutedEventArgs e)
        {
            if (lvEnseignant.SelectedIndex != -1)
            {
                Enseignant enseignant = (Enseignant)lvEnseignant.SelectedItem;
                MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment supprimer {enseignant.NomPersonnel} {enseignant.PrenomPersonnel} de la liste des enseignants ?", "Suppression enseignant", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.OK)
                {
                    // Suppression de l'enseignant dans la base de données et dans la liste LesEnseignants
                    ((ApplicationData)this.DataContext).Remove(enseignant);
                    // Rafraîchissement de la ListeView
                    lvEnseignant.Items.Refresh();

                    // Réinitialisation de la sélection
                    lvEnseignant.SelectedIndex = -1;

                    // Message de confirmation
                    MessageBox.Show("Enseignant supprimé !", "Suppression enseignant", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un enseignant pour le supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            // Réinitialise et cache les champs
            Reset();

            // Réinitialisation de la sélection
            lvEnseignant.SelectedIndex = -1;
        }

        private void btValider_Click(object sender, RoutedEventArgs e)
        {
            if (!(Verif_TextBoxVides() || VerifStyle()))
            {
                Enseignant enseignant = new Enseignant();
                String message, titre;

                if (lvEnseignant.SelectedIndex != -1)
                {
                    enseignant = (Enseignant)lvEnseignant.SelectedItem;
                    message = $"Voulez-vous vraiment modifier l'enseignant {enseignant.NomPersonnel} {enseignant.PrenomPersonnel} ?";
                    titre = "Modification enseignant";
                } else
                {

                    message = $"Voulez-vous vraiment ajouter l'enseignant {tbNom} {tbPrenom} ?";
                    titre = "Ajout enseignant";
                }
                
                MessageBoxResult result = MessageBox.Show(message, titre, MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);

                if (result == MessageBoxResult.OK)
                {
                    // Modification/Ajout des informations
                    enseignant.EmailPersonnel = tbMail.Text;
                    enseignant.NomPersonnel = tbNom.Text.Substring(0, 1).ToUpper() + tbNom.Text.Substring(1).ToLower();
                    enseignant.PrenomPersonnel = tbPrenom.Text.Substring(0, 1).ToUpper() + tbPrenom.Text.Substring(1).ToLower();

                    if (lvEnseignant.SelectedIndex != -1)
                    {
                        // Modification de l'enseignant
                        ((ApplicationData)this.DataContext).Update(enseignant);

                        // Message de confirmation
                        MessageBox.Show("Enseignant modifié !", "Modification enseignant", MessageBoxButton.OK);
                    }
                    else
                    {
                        // Création de l'enseignant dans la base de données et ajout à la liste LesEnseignants
                        ((ApplicationData)this.DataContext).Add(enseignant);

                        // Message de confirmation
                        MessageBox.Show("Enseignant ajouté !", "Ajout enseignant", MessageBoxButton.OK);
                    }

                    // Rafraîchissement de la ListeView
                    lvEnseignant.ItemsSource = ((ApplicationData)this.DataContext).LesEnseignants;
                }

                // Réinitialisation de la sélection
                lvEnseignant.SelectedIndex = -1;

                // Reset des champs de saisie
                Reset();
            }
            else
            {
                MessageBox.Show("Veuillez renseigner les champs obligatoires de manière conforme.", "Erreur modification enseignant", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Applique le style "Obligatoire" (bordures rouges) a la TextBox en fonction du texte (ici de la longueur du texte)
        /// </summary>
        private void NomPrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 50)
            {
                // Application du style avec bordures rouges
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Ajout du message 
                if (tb == tbNom)
                {
                    lbNomError.Content = "Trop long ( > 50 caractères)";
                } else
                {
                    lbPrenomError.Content = "Trop long ( > 50 caractères)";
                }
            } else
            {
                // Suppression du style (remplacement par un style par défaut)
                tb.Style = new Style();

                // Réinitialisation du label
                if (tb == tbNom)
                {
                    lbNomError.Content = "";
                }
                else
                {
                    lbPrenomError.Content = "";
                }
            }
        }

        /// <summary>
        /// Applique le style "Obligatoire" (bordures rouges) a la TextBox en fonction du texte (ici de la longueur du texte et la presence ou non d'un @)
        /// </summary>
        private void Mail_TextChanged(object sender, TextChangedEventArgs e)
        {
            // récupération de l'email renseigné
            String email = tbMail.Text;

            // Création d'un enseignant avec l'email rentré
            Enseignant enseignant = new Enseignant();
            enseignant.EmailPersonnel = email;

            // Si un enseignant est sélectionné (donc modification de l'enseignant) et que l'email n'a pas été modifié OU que l'email est conforme
            if ((lvEnseignant.SelectedItem != null && ((Enseignant)lvEnseignant.SelectedItem).EmailPersonnel == email) || (email.Length <= 100 && email.Contains('@') && enseignant.Read()))
            {
                tbMail.Style = new Style();
                lbMailError.Content = "";
            } else
            {
                tbMail.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Si la taille du mail dépasse 100 caractères alors il n'est pas valide
                if (email.Length > 100)
                {
                    lbMailError.Content = "Trop long";

                }

                // Si le mail ne contient pas de @ alors il n'est pas valide
                if (!email.Contains('@'))
                {
                    String content = lbMailError.Content.ToString();

                    // Ajout du message s'il n'est pas déjà affiché
                    if (String.IsNullOrWhiteSpace(content) || !content.Contains("Invalide (manque @)"))
                    {
                        lbMailError.Content += "\tInvalide (manque @)";
                    }
                }

                // Vérification de l'unicité de l'email
                if (!enseignant.Read())
                {
                    String content = lbMailError.Content.ToString();

                    // Ajout du message s'il n'est pas déjà affiché
                    if (!content.Contains("Cet email extiste déjà"))
                    {
                        lbMailError.Content += "\tCet email extiste déjà";
                    }
                }
            }
        }

        /// <summary>
        /// Vérifie les TextBox vide. Applique un style (au bordure rouge) pour chaque TextBox vide.
        /// </summary>
        /// <returns>True si une des TextBox est vide. False si toutes les TextBox contient du texte.</returns>
        private bool Verif_TextBoxVides()
        {
            bool result = false;
            if (String.IsNullOrWhiteSpace(tbNom.Text))
            {
                tbNom.Style = (Style)Application.Current.FindResource("Obligatoire");
                result = true;
            }
            if (String.IsNullOrWhiteSpace(tbPrenom.Text))
            {
                tbPrenom.Style = (Style)Application.Current.FindResource("Obligatoire");
                result = true;
            }
            if (String.IsNullOrWhiteSpace(tbMail.Text))
            {
                tbMail.Style = (Style)Application.Current.FindResource("Obligatoire");
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Vérifie les styles des TextBox.
        /// </summary>
        /// <returns>True si une des TextBox a le style "Obligatoire" (aux bordures rouges). False sinon.</returns>
        private bool VerifStyle()
        {
            if (tbNom.Style == (Style)Application.Current.FindResource("Obligatoire"))
            {
                return true;
            }
            if (tbPrenom.Style == (Style)Application.Current.FindResource("Obligatoire"))
            {
                return true;
            }
            if (tbMail.Style == (Style)Application.Current.FindResource("Obligatoire"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reinitialise le contenu, le style de toutes les TextBox, ainsi que les labels des messages d'erreur et les boutons. Puis les cache.
        /// </summary>
        private void Reset()
        {
            // Réinitialisation du texte des TextBox
            tbNom.Text = "";
            tbPrenom.Text = "";
            tbMail.Text = "";

            // Réinitialisation du style des TextBox
            tbNom.Style = new Style();
            tbPrenom.Style = new Style();
            tbMail.Style = new Style();

            // Réinitialisation du texte des Label
            lbNomError.Content = " ";
            lbPrenomError.Content = " ";
            lbMailError.Content = " ";

            // Cache les StackPanel
            spNom.Visibility = Visibility.Hidden;
            spPrenom.Visibility = Visibility.Hidden;
            spMail.Visibility = Visibility.Hidden;

            // Affichage des boutons supprimer,modifier,ajouter
            gridBouton.Visibility = Visibility.Visible;

            // Cache les boutons valider et annuler
            spBouton.Visibility = Visibility.Hidden;
        }

        private void AfficheForm()
        {
            // Réinitalise les champs et labels d'erreur
            Reset();

            // Affiche les StackPanel
            spNom.Visibility = Visibility.Visible;
            spPrenom.Visibility = Visibility.Visible;
            spMail.Visibility = Visibility.Visible;

            // Cache les boutons supprimer,modifier,ajouter
            gridBouton.Visibility = Visibility.Hidden;

            // Affichage des boutons valider et annuler
            spBouton.Visibility = Visibility.Visible;
        }
    }
}
