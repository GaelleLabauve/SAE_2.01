using System;

public class Enseignant : CRUD
{
   public int getidPersonnel()
   {
      return idPersonnel;
   }
   
   public void setidPersonnel(int newIdPersonnel)
   {
      this.idPersonnel = newIdPersonnel;
   }
   
   public String getmail()
   {
      return mail;
   }
   
   public void setmail(String newMail)
   {
      this.mail = newMail;
   }
   
   public String getnomPersonnel()
   {
      return nomPersonnel;
   }
   
   public void setnomPersonnel(String newNomPersonnel)
   {
      this.nomPersonnel = newNomPersonnel;
   }
   
   public String getprenomPersonnel()
   {
      return prenomPersonnel;
   }
   
   public void setprenomPersonnel(String newPrenomPersonnel)
   {
      this.prenomPersonnel = newPrenomPersonnel;
   }
   
   public Enseignant()
   {
      // TODO: implement
   }

   public System.Collections.ArrayList attribution;
   
   /// <pdGenerated>default getter</pdGenerated>
   public System.Collections.ArrayList GetAttribution()
   {
      if (attribution == null)
         attribution = new System.Collections.ArrayList();
      return attribution;
   }
   
   /// <pdGenerated>default setter</pdGenerated>
   public void SetAttribution(System.Collections.ArrayList newAttribution)
   {
      RemoveAllAttribution();
      foreach (Attribution oAttribution in newAttribution)
         AddAttribution(oAttribution);
   }
   
   /// <pdGenerated>default Add</pdGenerated>
   public void AddAttribution(Attribution newAttribution)
   {
      if (newAttribution == null)
         return;
      if (this.attribution == null)
         this.attribution = new System.Collections.ArrayList();
      if (!this.attribution.Contains(newAttribution))
         this.attribution.Add(newAttribution);
   }
   
   /// <pdGenerated>default Remove</pdGenerated>
   public void RemoveAttribution(Attribution oldAttribution)
   {
      if (oldAttribution == null)
         return;
      if (this.attribution != null)
         if (this.attribution.Contains(oldAttribution))
            this.attribution.Remove(oldAttribution);
   }
   
   /// <pdGenerated>default removeAll</pdGenerated>
   public void RemoveAllAttribution()
   {
      if (attribution != null)
         attribution.Clear();
   }

   private int idPersonnel;
   private String mail;
   private String nomPersonnel;
   private String prenomPersonnel;

}