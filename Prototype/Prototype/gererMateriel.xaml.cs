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
        /// <summary>
        /// Creation de la fenetre modal servant a la gestion des materiels.
        /// </summary>
        public gererMateriel()
        {
            InitializeComponent();
        }

        private void bt_ajouter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_materiel.Text) || string.IsNullOrEmpty(tb_codeBarre.Text) || string.IsNullOrEmpty(tb_refCons.Text) || cb_categorie.SelectedItem is null)
            {
                MessageBox.Show("Remplir tous les champs !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Materiel m = new Materiel(((Categorie)cb_categorie.SelectedItem).IdCategorie, tb_materiel.Text, tb_codeBarre.Text, tb_refCons.Text);
                ((ApplicationData)DataContext).Add(m);
                lv_materiel.ItemsSource = ((ApplicationData)this.DataContext).LesMateriels;
                MessageBox.Show("Ajout réaliser avec succés !", "Ajouter Materiel", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
            if (lv_materiel.SelectedItem is null)
            {
                MessageBox.Show("Séléctionner un materiel pour supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Materiel materiel = (Materiel)lv_materiel.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment supprimer {materiel.NomMateriel} de la liste des materiels ?", "Suppression Categorie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    ((Materiel)lv_materiel.SelectedItem).Delete();
                    ((ApplicationData)this.DataContext).Remove((Materiel)lv_materiel.SelectedItem);
                    MessageBox.Show("Suppression réaliser avec succés !", "Suppression Materiel", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btModif_Click(object sender, RoutedEventArgs e)
        {
            if (lv_materiel.SelectedItem is null)
            {
                MessageBox.Show("Séléctionner un materiel pour modifier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                ((Materiel)lv_materiel.SelectedItem).Update();
                MessageBox.Show("Modification réaliser avec succés !", "Modification Materiel", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
            else
            {
                // Suppression du style (remplacement par un style par défaut)
                tb.Style = new Style();

                // Réinitialisation du label
                if (tb == tb_codeBarre)
                {
                    lb_CodeBarreError.Content = "";
                }
               
            }
        }
       

        private void tb_refCons_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 100)
            {
                // Application du style avec bordures rouges
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Ajout du message 
                if (tb == tb_refCons)
                {
                    lbRefError.Content = "Trop long ( > 100 caractères)";
                }
            }
            else
            {
                // Suppression du style (remplacement par un style par défaut)
                tb.Style = new Style();

                // Réinitialisation du label
                if (tb == tb_refCons)
                {
                    lbRefError.Content = "";
                }

            }
        }

        private void tb_materiel_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 100)
            {
                // Application du style avec bordures rouges
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Ajout du message 
                if (tb == tb_materiel)
                {
                    lbNomError.Content = "Trop long ( > 100 caractères)";
                }
            }
            else
            {
                // Suppression du style (remplacement par un style par défaut)
                tb.Style = new Style();

                // Réinitialisation du label
                if (tb == tb_materiel)
                {
                    lbNomError.Content = "";
                }

            }
        }
    }
}
