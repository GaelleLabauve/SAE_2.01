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
    /// Logique d'interaction pour AjoutEnseignant.xaml
    /// </summary>
    public partial class AjoutEnseignant : Window
    {
        public AjoutEnseignant()
        {
            InitializeComponent();
        }

        private void bt_Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
