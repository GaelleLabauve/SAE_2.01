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
    /// Logique d'interaction pour gererCategorie.xaml
    /// </summary>
    public partial class gererCategorie : Window
    {
        public gererCategorie()
        {
            InitializeComponent();
        }

        private void BtSupprimer_Click(object sender, RoutedEventArgs e)
        {
            ((Categorie)lv_categorie.SelectedItem).Delete();
            ((ApplicationData)this.DataContext).Remove((Categorie)lv_categorie.SelectedItem);
            lv_categorie.SelectedIndex = 0;
            
        }

        private void BtAjouter_Click(object sender, RoutedEventArgs e)
        {
            ((Categorie)lv_categorie.SelectedItem).Create();
            ((ApplicationData)DataContext).LesCategories.Insert(0, new Categorie());
            lv_categorie.SelectedIndex = 0;
        }
    }
}
