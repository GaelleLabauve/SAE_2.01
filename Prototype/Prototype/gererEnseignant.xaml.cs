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
            if (!(Verif_TextBoxVide() || Verif_Donnees()))
            {
                // Ajouter l'ajout à la liste et à la base de données
                MessageBox.Show("Enseignant ajouté !", "Ajout enseignant", MessageBoxButton.OK);
            }
        }

        private void NomPrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 50)
            {
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");
            } else
            {
                tb.Style = new Style();
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
        private bool Verif_Donnees()
        {
            bool result = false;
            if (tbNom.Text.Length > 50)
            {
                MessageBox.Show("Le nom invalide : trop long (>50 caractères)");
                tbNom.Style = (Style)Application.Current.FindResource("Obligatoire");
                result = true;
            }
            if (tbPrenom.Text.Length > 50)
            {
                MessageBox.Show("Le prénom invalide : trop long (>50 caractères)");
                tbPrenom.Style = (Style)Application.Current.FindResource("Obligatoire");
                result = true;
            }
            if (tbMail.Text.Length > 100 || !tbMail.Text.Contains('@'))
            {
                MessageBox.Show("Le mail invaldie : trop long (>100 caractères) ou mauvais format (..@..)");
                tbMail.Style = (Style)Application.Current.FindResource("Obligatoire");
                result = true;
            }
            return result;
        }
    }
}
