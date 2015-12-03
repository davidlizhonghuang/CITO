using NumtoWord.Base.Interfaces;
using System.Data.Entity.Core.Objects;

namespace NumtoWord.EDMX.Repository.core
{
    public sealed class ObjectContextManager : IObjectContextManager
    {
        private ObjectContext _objectContext;
        private string _containerName = "NumEntities";

        /// <summary>
        /// Name of the container that will be used by Object Context.
        /// </summary>
        public string ContainerName
        {
            get { return _containerName; }
            set { _containerName = value; }
        }

        /// <summary>
        /// Return instance of Object Context.
        /// </summary>
        /// <returns></returns>
        public ObjectContext ObjectContext
        {
            get { return _objectContext ?? CreateObjectContext(); }
        }

        private ObjectContext CreateObjectContext()
        {
            _objectContext = new ObjectContext("name=" + ContainerName);
            _objectContext.DefaultContainerName = ContainerName;
            _objectContext.ContextOptions.LazyLoadingEnabled = true;
            _objectContext.CommandTimeout = 600;

            return _objectContext;
        }

        /// <summary>
        /// Freeing resource used by ObjectContext.
        /// </summary>
        public void Dispose()
        {
            _objectContext.Dispose();
        }
    }
}
