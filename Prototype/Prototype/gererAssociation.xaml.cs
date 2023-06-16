﻿using Prototype.Metier;
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
            if (lv_Attribution.SelectedItem is null)
            {
                MessageBox.Show("Séléctionner une catégorie pour modifier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                ((Attribution)lv_Attribution.SelectedItem).Update();
                MessageBox.Show("Modification réaliser avec succés !", "Suppression Attribution", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void bt_ajout_Click(object sender, RoutedEventArgs e)
        {
            if (cb_materiel.SelectedItem is null || cb_nomPrenomEns.SelectedItem is null || string.IsNullOrEmpty(tb_commentaire.Text) || datePicker_date.SelectedDate is null)
            {
                MessageBox.Show("Remplir tous les champs !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Attribution a = new Attribution(((Materiel)cb_materiel.SelectedItem).IdMateriel, ((Enseignant)cb_nomPrenomEns.SelectedItem).IdPersonnel, DateTime.Parse(datePicker_date.Text), tb_commentaire.Text);
            ((ApplicationData)DataContext).Add(a);
            lv_Attribution.ItemsSource = ((ApplicationData)this.DataContext).LesAttributions;
            MessageBox.Show("Ajout réaliser avec succés !", "Ajout Attribution", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
            if (lv_categorie.SelectedItem is null)
            {
                MessageBox.Show("Séléctionner une catégorie pour supprimer", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Attribution attribution = (Attribution)lv_Attribution.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Voulez-vous vraiment supprimer l'attribution :  \n Materiel : {attribution.UnMateriel.NomMateriel} \n Enseignant : {attribution.UnEnseignant.NomPrenom} \n Date : {attribution.DateAttribution} \n Commentaire :{attribution.CommentaireAttribution}", "Suppression Attribution", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    ((Attribution)lv_Attribution.SelectedItem).Delete();
                    ((ApplicationData)this.DataContext).Remove((Attribution)lv_Attribution.SelectedItem);
                    MessageBox.Show("Suppression réaliser avec succés !", "Modification attribution", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
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
