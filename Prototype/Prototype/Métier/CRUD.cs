using System;
using System.Collections.ObjectModel;

namespace Prototype.M�tier
{
    public interface Crud<T>
    {
        void Create();

        void Read();

        void Update();

        void Delete();

        ObservableCollection<T> FindAll();

        ObservableCollection<T> FindBySelection(string criteres);

    }
}