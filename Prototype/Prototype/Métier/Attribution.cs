using System;

public class Attribution : CRUD
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
   
   public Attribution()
   {
      // TODO: implement
   }

   private int idMateriel;
   private int idPersonnel;
   private DateTime dateAttribution;
   private String commentaire;

}