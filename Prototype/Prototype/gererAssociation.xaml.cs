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
    }
}
