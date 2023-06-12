using System;

public class Materiel : CRUD
{
   public int getidMateriel()
   {
      return idMateriel;
   }
   
   public void setidMateriel(int newIdMateriel)
   {
      this.idMateriel = newIdMateriel;
   }
   
   public int getidCategorie()
   {
      return idCategorie;
   }
   
   public void setidCategorie(int newIdCategorie)
   {
      this.idCategorie = newIdCategorie;
   }
   
   public String getcodeBarre()
   {
      return codeBarre;
   }
   
   public void setcodeBarre(String newCodeBarre)
   {
      this.codeBarre = newCodeBarre;
   }
   
   public String getrefConstructeur()
   {
      return refConstructeur;
   }
   
   public void setrefConstructeur(String newRefConstructeur)
   {
      this.refConstructeur = newRefConstructeur;
   }
   
   public Materiel()
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

   private int idMateriel;
   private int idCategorie;
   private String codeBarre;
   private String refConstructeur;

}