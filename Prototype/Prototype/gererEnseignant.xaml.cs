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
        /// Ajoute (après vérification) un enseignant à la base de données et à la liste LesEnseignants.
        /// </summary>
        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (spNom.Visibility == Visibility.Hidden)
            {
                AfficheForm();
            }
            else if (!(Verif_TextBoxVide() || Verif_Style()))
            {
                // Initialisation du nouvel enseignant et ajout des informations
                Enseignant enseignant = new Enseignant(tbMail.Text, tbNom.Text.Substring(0,1).ToUpper() + tbNom.Text.Substring(1).ToLower(), tbPrenom.Text.Substring(0, 1).ToUpper() + tbPrenom.Text.Substring(1).ToLower());

                // Création de l'enseignant dans la base de données et ajout à la liste LesEnseignants
                ((ApplicationData)this.DataContext).Add(enseignant);
                lvEnseignant.ItemsSource = ((ApplicationData)this.DataContext).LesEnseignants;

                // Message de confirmation
                MessageBox.Show("Enseignant ajouté !", "Ajout enseignant", MessageBoxButton.OK);

                // Sélection de l'enseignant ajouté
                lvEnseignant.SelectedIndex = lvEnseignant.Items.Count - 1;

                // Reset des champs de saisie
                Reset();
            }
        }

        /// <summary>
        /// Modification (après confirmation) de l'item sélectionné dans la ListView.
        /// </summary>
        private void btModif_Click(object sender, RoutedEventArgs e)
        {
            if (lvEnseignant.SelectedIndex != -1)
            {
                Enseignant enseignant = (Enseignant)lvEnseignant.SelectedItem;
                if (spNom.Visibility == Visibility.Hidden)
                {
                    AfficheForm();
                    tbNom.Text = enseignant.NomPersonnel;
                    tbPrenom.Text = enseignant.PrenomPersonnel;
                    tbMail.Text = enseignant.EmailPersonnel;
                }
                else if (!(Verif_TextBoxVide() || Verif_Style()))
                {
                    MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment modifier l'enseignant {enseignant.NomPersonnel} {enseignant.PrenomPersonnel} ?", "Modification enseignant", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);

                    if (result == MessageBoxResult.OK)
                    {
                        // Modification des informations
                        enseignant.EmailPersonnel = tbMail.Text;
                        enseignant.NomPersonnel = tbNom.Text;
                        enseignant.PrenomPersonnel = tbPrenom.Text;

                        // Modificaiton de l'enseignant dans la base de données
                        enseignant.Update();
                        // Rafraîchissement de la ListeView
                        lvEnseignant.Items.Refresh();

                        // Message de confirmation
                        MessageBox.Show("Enseignant modifié !", "Modification enseignant", MessageBoxButton.OK);

                        // Reset des champs de saisie
                        Reset();
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez renseigner les champs de manière conforme.", "Erreur modification enseignant", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un enseignant pour le modifier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Suppression (après confirmation) de l'item sélectionné dans la ListView.
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
        }

        /// <summary>
        /// Applique le style "Obligatoire" (bordures rouges) à la TextBox en fonction du texte (ici de la longueur du texte)
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
        /// Applique le style "Obligatoire" (bordures rouges) à la TextBox en fonction du texte (ici de la longueur du texte et la présence ou non d'un @)
        /// </summary>
        private void Mail_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Si la taille du mail dépasse 100 caractères alors il n'est pas valide
            if (tbMail.Text.Length > 100)
            {
                tbMail.Style = (Style)Application.Current.FindResource("Obligatoire");
                lbMailError.Content = "Trop long";

            }
            // Si le mail ne contient pas de @ alors il n'est pas valide
            if (!tbMail.Text.Contains('@'))
            {
                tbMail.Style = (Style)Application.Current.FindResource("Obligatoire");
                String content = lbMailError.Content.ToString();

                // Ajout du message s'il n'est pas déjà affiché
                if (String.IsNullOrWhiteSpace(content) || !content.Contains("Invalide (manque @)"))
                {
                    lbMailError.Content += "\tInvalide (manque @)";
                }
            }

            // Si tout est en ordre l'email est valide
            if (tbMail.Text.Length <= 100 && tbMail.Text.Contains('@'))
            {
                tbMail.Style = new Style();
                lbMailError.Content = " ";
            }
        }

        /// <summary>
        /// Vérifie les TextBox vide. Applique un style (au bordure rouge) pour chaque TextBox vide.
        /// </summary>
        /// <returns>True si une des TextBox est vide. False si toutes les TextBox contient du texte.</returns>
        private bool Verif_TextBoxVide()
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
            if (result)
            {
                MessageBox.Show("Veuillez remplir les champs manquants.", "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return result;
        }

        /// <summary>
        /// Vérifie les styles des TextBox.
        /// </summary>
        /// <returns>True si une des TextBox a le style "Obligatoire" (aux bordures rouges). False sinon.</returns>
        private bool Verif_Style()
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
        /// Réinitialise le contenu, le style de toutes les TextBox, ainsi que les labels des messages d'erreur.
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

            //// Cache les labels
            //lbNom.Visibility = Visibility.Collapsed;
            //lbPrenom.Visibility = Visibility.Collapsed;
            //lbMail.Visibility = Visibility.Collapsed;

            //// Cache les TextBox
            //tbNom.Visibility = Visibility.Collapsed;
            //tbPrenom.Visibility = Visibility.Collapsed;
            //tbMail.Visibility = Visibility.Collapsed;

            //// Cache les labels d'erreur
            //lbNomError.Visibility = Visibility.Collapsed;
            //lbPrenomError.Visibility = Visibility.Collapsed;
            //lbMailError.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Met à jour les champs si un enseignant est sélectionné. Reset les champs s'il est désélectionné.
        /// </summary>
        //private void lvEnseignant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{


        //    if (lvEnseignant.SelectedIndex != -1)
        //    {
        //        Enseignant enseignant = (Enseignant)lvEnseignant.SelectedItem;

        //        tbNom.Text = enseignant.NomPersonnel;
        //        tbPrenom.Text = enseignant.PrenomPersonnel;
        //        tbMail.Text = enseignant.EmailPersonnel;
        //    }
        //    else
        //    {
        //        Reset();
        //    }
        //}

        private void AfficheForm()
        {
            // Affiche les StackPanel
            spNom.Visibility = Visibility.Visible;
            spPrenom.Visibility = Visibility.Visible;
            spMail.Visibility = Visibility.Visible;

            //// Affiche les labels
            //lbNom.Visibility = Visibility.Visible;
            //lbPrenom.Visibility = Visibility.Visible;
            //lbMail.Visibility = Visibility.Visible;

            //// Affiche les TextBox
            //tbNom.Visibility = Visibility.Visible;
            //tbPrenom.Visibility = Visibility.Visible;
            //tbMail.Visibility = Visibility.Visible;

            //// Affiche les labels d'erreur
            //lbNomError.Visibility = Visibility.Visible;
            //lbPrenomError.Visibility = Visibility.Visible;
            //lbMailError.Visibility = Visibility.Visible;
        }
    }
}
