using Microsoft.VisualBasic;
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
                DataAccess accessDB = new DataAccess();
                accessDB.SetData($"INSERT INTO ENSEIGNANT(nomPersonnel,prenomPersonnel,mail) VALUES ('{tbNom.Text}','{tbPrenom.Text}','{tbMail.Text}')");
                MessageBox.Show("Enseignant ajouté !", "Ajout enseignant", MessageBoxButton.OK);
                Reset();
            }
        }

        private void NomPrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 50)
            {
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");
                if (tb == tbNom)
                {
                    lbNomError.Content = "Trop long ( > 50 caractères)";
                } else
                {
                    lbPrenomError.Content = "Trop long ( > 50 caractères)";
                }
            } else
            {
                tb.Style = new Style();
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
            if (tbMail.Text.Length > 100)
            {
                tbMail.Style = (Style)Application.Current.FindResource("Obligatoire");
                lbMailError.Content = "Trop long";

            }
            if (!tbMail.Text.Contains('@'))
            {
                tbMail.Style = (Style)Application.Current.FindResource("Obligatoire");
                String content = lbMailError.Content.ToString();
                if (String.IsNullOrWhiteSpace(content) || !content.Contains("Invalide (manque @)"))
                {
                    lbMailError.Content += "\tInvalide (manque @)";
                }
            }

            if (tbMail.Text.Length <= 100 && tbMail.Text.Contains('@'))
            {
                tbMail.Style = new Style();
                lbMailError.Content = " ";
            }
        }

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

        private void btSuppr_Click(object sender, RoutedEventArgs e)
        {
            if (lv_enseignant.SelectedIndex != -1)
            {
                Enseignant enseignant = (Enseignant)lv_enseignant.SelectedItem;
                MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment supprimer {enseignant.NomPersonnel} {enseignant.PrenomPersonnel} de la liste des enseignants ?", "Suppression enseignant", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.OK)
                {
                    DataAccess accessDB = new DataAccess();
                    accessDB.SetData($"DELETE FROM ENSEIGNANT WHERE idPersonnel='{enseignant.IdPersonnel}'");
                }
            }
        }
    }
}
