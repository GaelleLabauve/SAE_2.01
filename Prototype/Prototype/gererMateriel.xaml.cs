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
    /// Logique d'interaction pour gererMateriel.xaml
    /// </summary>
    public partial class gererMateriel : Window
    {
        public gererMateriel()
        {
            InitializeComponent();
        }

        private void bt_ajouter_Click(object sender, RoutedEventArgs e)
        {
            ((ApplicationData)DataContext).LesMateriels.Insert(0, new Materiel());
            lv_materiel.SelectedIndex = 0;
        }

        private void bt_valideajout_Click(object sender, RoutedEventArgs e)
        {
            ((Materiel)lv_materiel.SelectedItem).Create();
        }

        private void lv_materiel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lv_materiel.SelectedItem != null)
            {

                foreach (Categorie i in cb_categorie.Items)
                {
                    if (i.NomCategorie == ((Materiel)lv_materiel.SelectedItem).UneCategorie.NomCategorie)
                        cb_categorie.SelectedItem = i;
                }
            }
        }
    }
}
