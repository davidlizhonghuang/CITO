using System;
using System.Data.Entity.Core.Objects;

namespace NumtoWord.Base.Interfaces
{
    public interface IObjectContextManager : IDisposable
    {
        string ContainerName { get; set; }

        ObjectContext ObjectContext { get; }
    }
}

 