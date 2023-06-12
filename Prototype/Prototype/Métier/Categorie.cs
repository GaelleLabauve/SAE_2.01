using Prototype.Métier;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Prototype.Métier
{
    public class Categorie : Crud<Categorie>
    {
        public int IdCategorie { get; set; }
        public string NomCategorie { get; set; }


        public Categorie()
        { }
        public Categorie(int idCategorie, string nomCategorie)
        {
            this.IdCategorie = idCategorie;
            this.NomCategorie = nomCategorie;
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

        public ObservableCollection<Categorie> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}