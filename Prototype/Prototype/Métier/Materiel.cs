using System;

public class Materiel : CRUD
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
/*
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
   }*/


}