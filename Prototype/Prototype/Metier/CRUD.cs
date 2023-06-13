using System;
using System.Collections.ObjectModel;

namespace Prototype.Métier
{
    public interface Crud<T>
    {
        void Create();

        void Read();

        void Update();

        void Delete();

        ObservableCollection<T> FindAll();
    }
}