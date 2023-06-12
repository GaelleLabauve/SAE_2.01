using System;
using System.Collections.ObjectModel;

namespace Prototype.Métier
{
    public class Attribution : Crud<Attribution>
    {
        public int FK_IdMateriel { get; set; }
        public int FK_IdPersonnel { get; set; }
        public DateTime DateAttribution { get; set; }
        public string Commentaire { get; set; }
        public Enseignant UnEnseignant { get; set; }
        public Materiel UnMateriel { get; set; }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Attribution> FindAll()
        {
            throw new NotImplementedException();
        }

        public Attribution()
        {
        }
        public Attribution(int fK_IdMateriel, int fK_IdPersonnel, DateTime dateAttribution, string commentaire)
        {
            this.FK_IdMateriel = fK_IdMateriel;
            this.FK_IdPersonnel = fK_IdPersonnel;
            this.DateAttribution = dateAttribution;
            this.Commentaire = commentaire;
        }
    }
}