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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbNom.Text))
            {
                MessageBox.Show("Renseigner les champs manquants");
            }
            if (String.IsNullOrWhiteSpace(tbPrenom.Text))
            {
                MessageBox.Show("Renseigner les champs manquants");
            }
            if (String.IsNullOrWhiteSpace(tbMail.Text))
            {
                MessageBox.Show("Renseigner les champs manquants");
            }
        }
    }
}
