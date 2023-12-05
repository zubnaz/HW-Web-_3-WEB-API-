using System.Linq.Expressions;

namespace BusinessLogic.Interfaces
{
    public interface IDataServices<TEntity> where TEntity : class
    {
         Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");



        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task SaveAsync();
         TEntity GetByID(object id, string includeProperties = "");
        Task<TEntity> GetByIDAsync(object id, string includeProperties = "");
        IEnumerable<TEntity> Sort(string type,string by);
        


         void Insert(TEntity entity);


        void Delete(object id);



         void Delete(TEntity entityToDelete);


         void Update(TEntity entityToUpdate);

    }
    /*Task Add(Auto auto);
    Task Remove(int id);
    Task Update(Auto auto);
    List<Auto> Get();
    Task<List<Auto>> GetAsync();
    Auto Get(int id);
    Task<Auto> GetAsync(int id);*/
}
//}
