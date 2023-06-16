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
            Materiel m = new Materiel(((Categorie)cb_categorie.SelectedItem).IdCategorie, tb_materiel.Text, tb_codeBarre.Text, tb_refCons.Text);
            ((ApplicationData)DataContext).Add(m);
            lv_materiel.ItemsSource = ((ApplicationData)this.DataContext).LesMateriels;
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

        private void btSupp_Click(object sender, RoutedEventArgs e)
        {
            ((Materiel)lv_materiel.SelectedItem).Delete();
            ((ApplicationData)this.DataContext).Remove((Materiel)lv_materiel.SelectedItem);
        }

        private void btModif_Click(object sender, RoutedEventArgs e)
        {
            ((Materiel)lv_materiel.SelectedItem).Update();
        }

        private void tb_codeBarre_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 100)
            {
                // Application du style avec bordures rouges
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Ajout du message 
                if (tb == tb_codeBarre)
                {
                    lb_CodeBarreError.Content = "Trop long ( > 100 caractères)";
                }
            }
        }

        private void lb_RefError_TextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 100)
            {
                // Application du style avec bordures rouges
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Ajout du message 
               /*
                if (tb == tb_refCons)
                {
                    lb_RefError.Content = "Trop long ( > 100 caractères)";
                }*/
            }
        }
    }
}
