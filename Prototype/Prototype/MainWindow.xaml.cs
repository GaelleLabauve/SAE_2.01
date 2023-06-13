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
using Prototype.Metier;

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
            gererAssociation gererAsso = new gererAssociation();
            gererAsso.DataContext= this.DataContext;
            gererAsso.ShowDialog();
        }

        private void mi_gererCate_Click(object sender, RoutedEventArgs e)
        {
            gererCategorie gererCate = new gererCategorie();
            gererCate.DataContext = this.DataContext;
            gererCate.ShowDialog();
        }

        private void mi_gererEnseinant_Click(object sender, RoutedEventArgs e)
        {
            gererEnseignant gererEns = new gererEnseignant();
            gererEns.DataContext = this.DataContext;
            gererEns.ShowDialog();
        }

        private void mi_gererMateriel_Click(object sender, RoutedEventArgs e)
        {
            gererMateriel gererMate= new gererMateriel();
            gererMate.DataContext = this.DataContext;
            gererMate.ShowDialog();
        }

        private void bt_tousMateriel_Click(object sender, RoutedEventArgs e)
        {
            lv_MaterielChoix.SelectedItem = null;
        }
    }
}
