using Microsoft.VisualBasic;
using Prototype.Metier;
using System;
using System.Collections.Generic;
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

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            if (!(Verif_TextBoxVide() || Verif_Style()))
            {
                // Initialisation du nouvel enseignant et ajout des informations
                Enseignant enseignant = new Enseignant(tbMail.Text, tbNom.Text, tbPrenom.Text);

                // Création de l'enseignant dans la base de données et ajout à la liste LesEnseignants
                ((ApplicationData)this.DataContext).Add(enseignant);
                // Rafraîchissement de la ListeView
                lvEnseignant.Items.Refresh();

                // Message de confirmation
                MessageBox.Show("Enseignant ajouté !", "Ajout enseignant", MessageBoxButton.OK);

                // Reset des champs de saisie
                Reset();
            }
        }

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
                }
            }
        }


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
        /// <returns>True si une des TextBox a le style "Obligatoire" (aux bordures rouges).</returns>
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
        /// Réinitialise toutes les TextBox, les styles de celles-ci, ainsi que les labels des messages d'erreur.
        /// </summary>
        private void Reset()
        {
            tbNom.Text = "";
            tbPrenom.Text = "";
            tbMail.Text = "";

            tbNom.Style = new Style();
            tbPrenom.Style = new Style();
            tbMail.Style = new Style();

            lbNomError.Content = " ";
            lbPrenomError.Content = " ";
            lbMailError.Content = " ";
        }
    }
}
