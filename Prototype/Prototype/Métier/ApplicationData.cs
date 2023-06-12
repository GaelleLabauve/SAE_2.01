using System;

public class ApplicationData
{
   public ApplicationData()
   {
      // TODO: implement
   }

   public System.Collections.ArrayList categorie;
   
   /// <pdGenerated>default getter</pdGenerated>
   public System.Collections.ArrayList GetCategorie()
   {
      if (categorie == null)
         categorie = new System.Collections.ArrayList();
      return categorie;
   }
   
   /// <pdGenerated>default setter</pdGenerated>
   public void SetCategorie(System.Collections.ArrayList newCategorie)
   {
      RemoveAllCategorie();
      foreach (Categorie oCategorie in newCategorie)
         AddCategorie(oCategorie);
   }
   
   /// <pdGenerated>default Add</pdGenerated>
   public void AddCategorie(Categorie newCategorie)
   {
      if (newCategorie == null)
         return;
      if (this.categorie == null)
         this.categorie = new System.Collections.ArrayList();
      if (!this.categorie.Contains(newCategorie))
         this.categorie.Add(newCategorie);
   }
   
   /// <pdGenerated>default Remove</pdGenerated>
   public void RemoveCategorie(Categorie oldCategorie)
   {
      if (oldCategorie == null)
         return;
      if (this.categorie != null)
         if (this.categorie.Contains(oldCategorie))
            this.categorie.Remove(oldCategorie);
   }
   
   /// <pdGenerated>default removeAll</pdGenerated>
   public void RemoveAllCategorie()
   {
      if (categorie != null)
         categorie.Clear();
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
   public System.Collections.ArrayList enseignant;
   
   /// <pdGenerated>default getter</pdGenerated>
   public System.Collections.ArrayList GetEnseignant()
   {
      if (enseignant == null)
         enseignant = new System.Collections.ArrayList();
      return enseignant;
   }
   
   /// <pdGenerated>default setter</pdGenerated>
   public void SetEnseignant(System.Collections.ArrayList newEnseignant)
   {
      RemoveAllEnseignant();
      foreach (Enseignant oEnseignant in newEnseignant)
         AddEnseignant(oEnseignant);
   }
   
   /// <pdGenerated>default Add</pdGenerated>
   public void AddEnseignant(Enseignant newEnseignant)
   {
      if (newEnseignant == null)
         return;
      if (this.enseignant == null)
         this.enseignant = new System.Collections.ArrayList();
      if (!this.enseignant.Contains(newEnseignant))
         this.enseignant.Add(newEnseignant);
   }
   
   /// <pdGenerated>default Remove</pdGenerated>
   public void RemoveEnseignant(Enseignant oldEnseignant)
   {
      if (oldEnseignant == null)
         return;
      if (this.enseignant != null)
         if (this.enseignant.Contains(oldEnseignant))
            this.enseignant.Remove(oldEnseignant);
   }
   
   /// <pdGenerated>default removeAll</pdGenerated>
   public void RemoveAllEnseignant()
   {
      if (enseignant != null)
         enseignant.Clear();
   }

}