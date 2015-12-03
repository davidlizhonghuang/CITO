using System.Collections.ObjectModel;

namespace NumtoWord.Base.Interfaces
{
    public interface IDALRepository<T> where T : class
    {

        IObjectContextManager ObjectContextManager { get; set; }

        Collection<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        int SaveChanges();

        Collection<T> GetEntitiesAjaxList(string whereClause, string orderBy, int page, int? maxRow);

        int GetEntitiesCount(string whereClause);
    }
}




