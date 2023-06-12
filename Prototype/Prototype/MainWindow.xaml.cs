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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prototype.Métier;

namespace Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_ClickQuitter(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mi_gererAsso_Click(object sender, RoutedEventArgs e)
        {
            Window gererAsso = new gererAssociation();
            gererAsso.ShowDialog();
        }

        private void mi_gererCate_Click(object sender, RoutedEventArgs e)
        {
            Window gererCate = new gererCategorie();
            gererCate.ShowDialog();
        }

        private void mi_gererEnseinant_Click(object sender, RoutedEventArgs e)
        {
            Window gererEns = new gererEnseignant();
            gererEns.ShowDialog();
        }

        private void mi_gererMateriel_Click(object sender, RoutedEventArgs e)
        {
            Window gererMate= new gererMateriel();
            gererMate.ShowDialog();
        }

        private void bt_tousMateriel_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
