using System;
using System.Collections.ObjectModel;

namespace Prototype.Metier
{
    /// <summary>
    /// Interface servant initialisant les methodes Create(), Read(), Update(), Delete() et FindAll().
    /// </summary>
    public interface Crud<T>
    {
        void Create();

        void Read();

        void Update();

        void Delete();

        ObservableCollection<T> FindAll();
    }
}