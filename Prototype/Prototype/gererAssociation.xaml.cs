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
    /// Logique d'interaction pour gererAssociation.xaml
    /// </summary>
    public partial class gererAssociation : Window
    {
        /// <summary>
        /// Creation de la fenetre modal servant a la gestion des attributions.
        /// </summary>
        public gererAssociation()
        {
            InitializeComponent();
        }

        private void bt_modif_Click(object sender, RoutedEventArgs e)
        {
            ((Attribution)lv_Attribution.SelectedItem).Update();
        }

        private void bt_ajout_Click(object sender, RoutedEventArgs e)
        {
            Attribution a = new Attribution(((Materiel)cb_materiel.SelectedItem).IdMateriel, ((Enseignant)cb_nomPrenomEns.SelectedItem).IdPersonnel, DateTime.Parse(datePicker_date.Text), tb_commentaire.Text);
            ((ApplicationData)DataContext).Add(a);
            lv_Attribution.ItemsSource = ((ApplicationData)this.DataContext).LesAttributions;
        }

        private void lv_Attribution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lv_Attribution.SelectedItem == null)
            {
                sp_materiel.IsEnabled = true;
                sp_nomEns.IsEnabled = true;
            }
            else
            {

                foreach (Materiel i in cb_materiel.Items)
                {
                    if(i.NomMateriel == ((Attribution)lv_Attribution.SelectedItem).UnMateriel.NomMateriel)
                        cb_materiel.SelectedItem = i;
                }

                foreach (Enseignant i in cb_nomPrenomEns.Items)
                {
                    if(i.NomPrenom == ((Attribution)lv_Attribution.SelectedItem).UnEnseignant.NomPrenom)
                        cb_nomPrenomEns.SelectedItem = i;
                }
                //Champs non modifiable
                sp_materiel.IsEnabled = false;
                sp_nomEns.IsEnabled = false;
            }
            //Champs modifiabe
            sp_date.IsEnabled = true;
            sp_commentaire.IsEnabled = true;
        }

        private void bt_supp_Click(object sender, RoutedEventArgs e)
        {
            ((Attribution)lv_Attribution.SelectedItem).Delete();
            ((ApplicationData)this.DataContext).Remove((Attribution)lv_Attribution.SelectedItem);
        }

        private void tb_commentaire_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length > 1000)
            {
                // Application du style avec bordures rouges
                tb.Style = (Style)Application.Current.FindResource("Obligatoire");

                // Ajout du message 
                if (tb == tb_commentaire)
                {
                    lbCommentaireError.Content = "Trop long ( > 1000 caractères)";
                }
            }
            else
            {
                // Suppression du style (remplacement par un style par défaut)
                tb.Style = new Style();

                // Réinitialisation du label
                if (tb == tb_commentaire)
                {
                    lbCommentaireError.Content = "";
                }

            }
        }
    }
}
