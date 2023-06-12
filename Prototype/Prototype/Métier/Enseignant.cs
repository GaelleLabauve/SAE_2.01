using System;
using System.Collections.ObjectModel;

public class Enseignant : CRUD
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

    public override void Create()
    {

    }

    public override void Remove()
    {

    }

    public override void Update()
    {

    }

    public override void Delete()
    {

    }
}