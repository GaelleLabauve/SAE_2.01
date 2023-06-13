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
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Style = new Style();
        }
    }
}
