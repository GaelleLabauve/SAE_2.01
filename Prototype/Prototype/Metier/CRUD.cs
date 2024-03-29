using System;
using System.Collections.ObjectModel;

namespace Prototype.Metier
{
    /// <summary>
    /// Interface initialisant les methodes Create(), Read(), Update(), Delete() et FindAll().
    /// </summary>
    public interface Crud<T>
    {
        void Create();

        bool Read();

        void Update();

        void Delete();

        ObservableCollection<T> FindAll();
    }
}