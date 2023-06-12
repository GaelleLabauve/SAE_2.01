using System;
using System.Collections.ObjectModel;

namespace Prototype.Métier
{
    public class Materiel : Crud<Materiel>
    {
        public int IdMateriel { get; set; }
        public int IdCategorie { get; set; }
        public string CodeBarre { get; set; }
        public string Refconstructeur { get; set; }

   
        public Materiel()
        { }
        public Materiel(int idMateriel, int idCategorie, string codeBarre, string refconstructeur)
        {
            this.IdMateriel = idMateriel;
            this.IdCategorie = idCategorie;
            this.CodeBarre = codeBarre;
            this.Refconstructeur = refconstructeur;
        }

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

        public ObservableCollection<Materiel> FindAll()
        {
            throw new NotImplementedException();
        }        
    }
}