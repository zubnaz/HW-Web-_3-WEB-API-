using BusinessLogic.Data.Entitys;
using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataProject
{
    public class WorkWithData<TEntity> : IDataServices<TEntity> where TEntity : class
    {
        internal AutoDbContext adc;
        internal DbSet<TEntity> dbSet;

        public WorkWithData(AutoDbContext adc)
        {
            this.dbSet = adc.Set<TEntity>();
        }
        public async Task SaveAsync()
        {
           await this.adc.SaveChangesAsync();
        }
        public virtual Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            return Task.Run(() => {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList() as IEnumerable<TEntity>;
                }
                else
                {
                    return query.ToList() as IEnumerable<TEntity>;
                }
            });
            
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }

        }

        public virtual TEntity GetByID(object id, string includeProperties = "")
        {
            //if (auto == null) throw new HttpException($"Auto with ID <{id}> not found!", HttpStatusCode.NotFound);
            IQueryable<TEntity> query = dbSet;
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return dbSet.Find(id);
        }
        public virtual Task<TEntity> GetByIDAsync(object id, string includeProperties = "")
        {
            //if (auto == null) throw new HttpException($"Auto with ID <{id}> not found!", HttpStatusCode.NotFound);
            return Task.Run(() =>
            {
                IQueryable<TEntity> query = dbSet;
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                return dbSet.Find(id);
            });
           
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (adc.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            adc.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /*public IEnumerable<TEntity> Sort(string type,string by)
        {
            IEnumerable<TEntity> auto;
            if (by == "price")
            {
                IQueryable<TEntity> query = dbSet;
                
                //auto = (IEnumerable<TEntity>)query.OrderBy(
                
            }
            else if (by == "mark")
            {
                 auto = (IEnumerable<TEntity>)adc.Autos.OrderBy(a => a.Mark);
            }
            else
            {
                 auto = (IEnumerable<TEntity>)adc.Autos.OrderBy(a => a.Model);
            }
            if (type == "up")
            {
                return auto;
            }
            else
            {
                return auto.Reverse();
            }
        }*/
    }
}
    /*public class WorkWithData : IDataServices
    {
        private readonly AutoDbContext Adc;

        public WorkWithData(AutoDbContext adc)
        {
            this.Adc = adc;
        }
        public async Task Add(Auto auto)
        {
            Adc.Autos.Add(auto);
            await Adc.SaveChangesAsync();
        }
        public async Task Remove(int id)
        {
            var auto = Adc.Autos.Find(id);
            if (auto == null) throw new HttpException($"Auto with ID <{id}> not found!", HttpStatusCode.NotFound);
            Adc.Autos.Remove(auto);
            await Adc.SaveChangesAsync();
        }

        public async Task Update(Auto auto)
        {
            Adc.Autos.Update(auto);
            await Adc.SaveChangesAsync();
        }
        public List<Auto> Get()
        {
            var list = Adc.Autos.Include(a => a.Color).ToList();
            if (list == null) throw new HttpException("List is empty!", HttpStatusCode.NotFound);
            return list;
        }

        public Auto Get(int id)
        {
            var auto = Adc.Autos.Include(a => a.Color).ToList().Find(a=>a.Id==id);
            if (auto == null) throw new HttpException($"Auto with ID <{id}> not found!", HttpStatusCode.NotFound);
            return auto;
        }

        public Task<List<Auto>> GetAsync()
        {
            return Task.Run(()=>{
                var list = Adc.Autos.Include(a => a.Color).ToList();
                if (list == null) throw new HttpException("List is empty!", HttpStatusCode.NotFound);
                return list;
            });
        }

        public Task<Auto> GetAsync(int id)
        {
            return Task.Run(() => {
                var auto = Adc.Autos.Include(a => a.Color).ToList().Find(a => a.Id == id);
                if (auto == null) throw new HttpException($"Auto with ID <{id}> not found!", HttpStatusCode.NotFound);
                return auto;
            });
        }

        
    }*/
//}
