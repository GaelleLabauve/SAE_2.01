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
            sp_categorie.IsEnabled = false;
            sp_date.IsEnabled= false;
            sp_materiel.IsEnabled=false;
            sp_nomEns.IsEnabled = false;
            sp_prenomEns.IsEnabled = false;
            sp_commentaire.IsEnabled = false;
        }

        private void bt_modif_Click(object sender, RoutedEventArgs e)
        {
            //Champs non modifiable
            sp_categorie.IsEnabled = false;
            sp_materiel.IsEnabled = false;
            sp_nomEns.IsEnabled = false;
            sp_prenomEns.IsEnabled = false;
            //Chanps modifiabe
            sp_date.IsEnabled = true;
            sp_commentaire.IsEnabled = true;
        }

        private void bt_ajout_Click(object sender, RoutedEventArgs e)
        {

            sp_categorie.IsEnabled = true;
            sp_date.IsEnabled = true;
            sp_materiel.IsEnabled = true;
            sp_nomEns.IsEnabled = true;
            sp_prenomEns.IsEnabled = true;
            sp_commentaire.IsEnabled = true;
        }
    }
}
