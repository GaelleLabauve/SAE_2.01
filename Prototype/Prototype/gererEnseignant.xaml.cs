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
            if (String.IsNullOrWhiteSpace(tbNom.Text) || String.IsNullOrWhiteSpace(tbPrenom.Text) || String.IsNullOrWhiteSpace(tbMail.Text))
            {
                MessageBox.Show("Veuillez remplir les champs manquants.", "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (String.IsNullOrWhiteSpace(tbNom.Text))
                {
                    tbNom.Style = (Style)Application.Current.FindResource("Obligatoire");
                }
                if (String.IsNullOrWhiteSpace(tbPrenom.Text))
                {
                    tbPrenom.Style = (Style)Application.Current.FindResource("Obligatoire");
                }
                if (String.IsNullOrWhiteSpace(tbMail.Text))
                {
                    tbMail.Style = (Style)Application.Current.FindResource("Obligatoire");
                }
            }
            else if (tbNom.Text.Length > 50 || tbPrenom.Text.Length > 50 || tbMail.Text.Length > 100 || !tbMail.Text.Contains('@'))
            {
                if (tbNom.Text.Length > 50)
                {
                    MessageBox.Show("Le nom invalide : trop long (>50 caractères)");
                    tbNom.Style = (Style)Application.Current.FindResource("Obligatoire");
                }
                if (tbPrenom.Text.Length > 50)
                {
                    MessageBox.Show("Le prénom invalide : trop long (>50 caractères)");
                    tbPrenom.Style = (Style)Application.Current.FindResource("Obligatoire");
                }
                if (tbMail.Text.Length > 100 || !tbMail.Text.Contains('@'))
                {
                    MessageBox.Show("Le mail invaldie : trop long (>100 caractères) ou mauvais format (..@..)");
                    tbMail.Style = (Style)Application.Current.FindResource("Obligatoire");
                }
            } else
            {
                // Ajouter l'ajout à la liste et à la base de données
                MessageBox.Show("Enseignant ajouté !", "Ajout enseignant", MessageBoxButton.OK);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Style = new Style();
        }
    }
}
