using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private bool tout;

        /// <summary>
        /// Genere la fenetre principale.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            MessageBox.Show("EMERIC FINI LES TESTS EN RAPPORT AVEC LES CONTRAINTE D'UNICITE !!!!!!!!!!", "EMERIC TU DOIS", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            MessageBox.Show("GAELLE MET EN PLACE LES TESTS DANS LES CLASSES !!!!!!!!!!", "GAELLE TU DOIS", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(dg_attributions.ItemsSource);
            view.Filter = FiltrerAttributions;
        }

        private void MenuItem_ClickQuitter(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mi_gererAsso_Click(object sender, RoutedEventArgs e)
        {
            AnnulerTousLesFiltre();
            gererAssociation gererAsso = new gererAssociation();
            gererAsso.DataContext= this.DataContext;
            gererAsso.ShowDialog();
        }

        private void mi_gererCate_Click(object sender, RoutedEventArgs e)
        {
            AnnulerTousLesFiltre();
            gererCategorie gererCate = new gererCategorie();
            gererCate.DataContext = this.DataContext;
            gererCate.ShowDialog();
        }

        private void mi_gererEnseinant_Click(object sender, RoutedEventArgs e)
        {
            AnnulerTousLesFiltre();
            gererEnseignant gererEns = new gererEnseignant();
            gererEns.DataContext = this.DataContext;
            gererEns.ShowDialog();
        }

        private void mi_gererMateriel_Click(object sender, RoutedEventArgs e)
        {
            AnnulerTousLesFiltre();
            gererMateriel gererMate= new gererMateriel();
            gererMate.DataContext = this.DataContext;
            gererMate.ShowDialog();
        }

        private void bt_tousMateriel_Click(object sender, RoutedEventArgs e)
        {
            lv_MaterielChoix.SelectedItem = null;
            CollectionViewSource.GetDefaultView(dg_attributions.ItemsSource).Refresh();
        }

        private bool FiltrerAttributions(object item)
        {
            Attribution a = item as Attribution;
            if (tout)
                return true;
            else if (lv_cateChoix.SelectedItem == null)
                return false;
            else
            {
                if(lv_MaterielChoix.SelectedItem != null)
                {
                    return a.UnMateriel == ((Materiel)lv_MaterielChoix.SelectedItem);
                }
                else
                    return a.UnMateriel.UneCategorie == ((Categorie)lv_cateChoix.SelectedItem);
            }
        }

        private void lv_cateChoix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tout = false;
            CollectionViewSource.GetDefaultView(dg_attributions.ItemsSource).Refresh();
        }

        private void lv_MaterielChoix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dg_attributions.ItemsSource).Refresh();
        }

        private void bt_tousCategorie_Click(object sender, RoutedEventArgs e)
        {
            AnnulerTousLesFiltre();
        }

        private void AnnulerTousLesFiltre()
        {
            lv_cateChoix.SelectedItem = null;
            tout = true;
            CollectionViewSource.GetDefaultView(dg_attributions.ItemsSource).Refresh();
        }
    }
}
