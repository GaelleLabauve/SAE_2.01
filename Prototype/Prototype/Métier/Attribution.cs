using System;
using System.Collections.ObjectModel;

namespace Prototype.Métier
{
    public class Attribution : Crud<Attribution>
    {
        public int getidMateriel()
        {
            return idMateriel;
        }

        public void setidMateriel(int newIdMateriel)
        {
            this.idMateriel = newIdMateriel;
        }

        public int getidPersonnel()
        {
            return idPersonnel;
        }

        public void setidPersonnel(int newIdPersonnel)
        {
            this.idPersonnel = newIdPersonnel;
        }

        public DateTime get_dateAttribution()
        {
            return dateAttribution;
        }

        public void setdateAttribution(DateTime newDateAttribution)
        {
            this.dateAttribution = newDateAttribution;
        }

        public String getcommentaire()
        {
            return commentaire;
        }

        public void setcommentaire(String newCommentaire)
        {
            this.commentaire = newCommentaire;
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

        public ObservableCollection<Attribution> FindAll()
        {
            throw new NotImplementedException();
        }

        public Attribution()
        {
            // TODO: implement
        }

        private int idMateriel;
        private int idPersonnel;
        private DateTime dateAttribution;
        private String commentaire;

    }
}