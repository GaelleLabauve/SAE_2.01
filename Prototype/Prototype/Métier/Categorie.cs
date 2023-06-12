using System;

public class Categorie : CRUD
{
   public int getidCategorie()
   {
      return idCategorie;
   }
   
   public void setidCategorie(int newIdCategorie)
   {
      this.idCategorie = newIdCategorie;
   }
   
   public String getnomCategorie()
   {
      return nomCategorie;
   }
   
   public void setnomCategorie(String newNomCategorie)
   {
      this.nomCategorie = newNomCategorie;
   }
   
   public Categorie()
   {
      // TODO: implement
   }

   public System.Collections.ArrayList materiel;
   
   /// <pdGenerated>default getter</pdGenerated>
   public System.Collections.ArrayList GetMateriel()
   {
      if (materiel == null)
         materiel = new System.Collections.ArrayList();
      return materiel;
   }
   
   /// <pdGenerated>default setter</pdGenerated>
   public void SetMateriel(System.Collections.ArrayList newMateriel)
   {
      RemoveAllMateriel();
      foreach (Materiel oMateriel in newMateriel)
         AddMateriel(oMateriel);
   }
   
   /// <pdGenerated>default Add</pdGenerated>
   public void AddMateriel(Materiel newMateriel)
   {
      if (newMateriel == null)
         return;
      if (this.materiel == null)
         this.materiel = new System.Collections.ArrayList();
      if (!this.materiel.Contains(newMateriel))
         this.materiel.Add(newMateriel);
   }
   
   /// <pdGenerated>default Remove</pdGenerated>
   public void RemoveMateriel(Materiel oldMateriel)
   {
      if (oldMateriel == null)
         return;
      if (this.materiel != null)
         if (this.materiel.Contains(oldMateriel))
            this.materiel.Remove(oldMateriel);
   }
   
   /// <pdGenerated>default removeAll</pdGenerated>
   public void RemoveAllMateriel()
   {
      if (materiel != null)
         materiel.Clear();
   }

   private int idCategorie;
   private String nomCategorie;

}