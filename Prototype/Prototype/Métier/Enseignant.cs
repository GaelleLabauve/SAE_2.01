using System;
using System.Collections.ObjectModel;

namespace Prototype.Métier
{
    public class Enseignant : Crud<Enseignant>
    {
        public int IdPersonnel { get; set; }
        public string Email { get; set; }
        public string NomPersonnel { get; set; }
        public string PrenomPersonnel { get; set; }
        public ObservableCollection<Attribution> LesAttributions;

        public Enseignant()
        {
        }
        public Enseignant(int idPersonnel, string email, string nomPersonnel, string prenomPersonnel)
        {
            this.IdPersonnel = idPersonnel;
            this.Email = email;
            this.NomPersonnel = nomPersonnel;
            this.PrenomPersonnel = prenomPersonnel;
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

        public ObservableCollection<Enseignant> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}