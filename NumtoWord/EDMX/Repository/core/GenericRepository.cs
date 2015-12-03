using NumtoWord.Base.Interfaces;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace NumtoWord.EDMX.Repository.core
{

        public abstract class GenericRepository<T> : IDALRepository<T> where T : class
        {

        private IObjectContextManager _objectContextManager;

        private ObjectSet<T> _entities;

        public IObjectContextManager ObjectContextManager
            {
                get { return _objectContextManager; }

                set { _objectContextManager = value; }
            }
         
            protected ObjectContext Context
            {
                get { return _objectContextManager.ObjectContext; }
            }

                protected GenericRepository(IObjectContextManager objectContexManager)
            {
                _objectContextManager = objectContexManager;
            }

            #region IDALRepository

            /// <summary>
            /// Entities contained by this repository.
            /// </summary>        
            protected ObjectSet<T> Entities
            {
                get { return _entities ?? (_entities = Context.CreateObjectSet<T>()); }
            }

            /// <summary>
            /// Get All entities.
            /// </summary>        
            public virtual Collection<T> GetAll()
            {
                return new Collection<T>(Entities.ToList<T>());
            }

            /// <summary>
            /// Add entity to the repository.
            /// </summary>
            /// <param name="entity">the entity to add</param>
            /// <returns>The added entity</returns>
            public abstract void Add(T entity);

            /// <summary>
            /// Update entity in reporsitory.
            /// </summary>
            /// <param name="entity">The updated entity</param>
            public abstract void Update(T entity);

            /// <summary>
            /// Mark entity to be deleted within the repository.
            /// </summary>
            /// <param name="entity">The entity to delete</param>
            public abstract void Delete(T entity);

            /// <summary>
            /// Save all changes from repository to store.
            /// </summary>
            /// <returns>Total number of objects affected</returns>
            public virtual int SaveChanges()
            {
                return Context.SaveChanges();
            }

            /// <summary>
            /// Get the entities for the particular page filtered by whereClause parameter and sorted by orderBy parameter.
            /// </summary>
            /// <param name="whereClause">lookup criteria</param>
            /// <param name="orderBy">sorting criteria</param>
            /// <param name="page">page number</param>
            /// <returns>The filtered and sorted entities for the current page</returns>
            public virtual Collection<T> GetEntitiesAjaxList(string whereClause, string orderBy, int page, int? maxRow)
            {
                int _maxRow = maxRow ?? 120;
                int skip = (page - 1) * _maxRow;

                Collection<T> result = new Collection<T>();
                if (string.IsNullOrEmpty(whereClause))
                {
                    result = new Collection<T>(Entities.OrderBy(orderBy).Skip(skip).Take(_maxRow).ToList());
                }
                else
                {
                    result = new Collection<T>(Entities.Where(whereClause).OrderBy(orderBy).Skip(skip).Take(_maxRow).ToList());
                }

                return result;
            }

            /// <summary>
            /// Get the total number of the records
            /// </summary>
            /// <param name="whereClause">lookup criteria</param>
            /// <returns>the number of records</returns>
            public virtual int GetEntitiesCount(string whereClause)
            {
                int result;

                if (string.IsNullOrEmpty(whereClause))
                {
                    result = Entities.Count();
                }
                else
                {
                    result = Entities.Where(whereClause).Count();
                }

                return result;
            }
            #endregion

        }
    }

