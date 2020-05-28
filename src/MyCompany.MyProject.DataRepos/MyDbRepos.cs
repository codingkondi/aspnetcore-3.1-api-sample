using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Database;
using MyCompany.MyProject.DataRepos.Interface;
using MyCompany.MyProject.Extensions.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyCompany.MyProject.DataRepos
{
    public class MyDbRepos<TEntity, TViewModel> : IGenericRepos<TEntity, TViewModel>
        where TEntity : class
        where TViewModel : class
    {
        private MyDbContext myDbContext;
        private DbSet<TEntity> dbSet;
        private TEntity newEntity;
        public MyDbRepos(MyDbContext context)
        {
            myDbContext = context;
            dbSet = myDbContext.Set<TEntity>();
        }
        public void Delete(TViewModel DeleteItem)
        {
            TEntity item = DeleteItem.MapToItem<TViewModel, TEntity>();
            dbSet.Remove(item);
        }

        public void DeleteById(object Id)
        {
            TEntity DeleteItem = dbSet.Find(Id);

            if (DeleteItem != null)
            {
                if (myDbContext.Entry(DeleteItem).State == EntityState.Deleted)
                {
                    dbSet.Attach(DeleteItem);
                }
                dbSet.Remove(DeleteItem);
            }
        }

        public void DeleteList(List<TViewModel> DeleteList)
        {
            IEnumerable<TEntity> list = DeleteList.MapToList<TViewModel, TEntity>();
            dbSet.RemoveRange(list);
        }

        public IEnumerable<TViewModel> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, object>>[] includeProperties = null, int? limit = null)
        {
            IQueryable<TEntity> query = dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }
            }
            if (orderBy != null)
            {
                return (limit != null ? orderBy(query).Take((int)limit) : orderBy(query)).MapToList<TEntity, TViewModel>();
            }

            return (limit != null ? query.Take((int)limit) : query).MapToList<TEntity, TViewModel>();
        }

        public TViewModel GetById(object Id)
        {
            TEntity entity = dbSet.Find(Id);
            if (entity is null)
                return null;

            myDbContext.Entry(entity).State = EntityState.Detached;
            return entity.MapToItem<TEntity, TViewModel>();
        }

        public TViewModel GetNewItem()
        {
            return newEntity?.MapToItem<TEntity, TViewModel>();
        }

        public void Insert(TViewModel newViewModel)
        {
            newEntity = newViewModel.MapToItem<TViewModel, TEntity>();
            dbSet.Add(newEntity);
        }

        public TViewModel InsertAndSaveChange(TViewModel newViewModel)
        {
            TEntity newEnitity = newViewModel.MapToItem<TViewModel, TEntity>();
            dbSet.Add(newEnitity);
            myDbContext.SaveChanges();
            myDbContext.Entry(newEnitity).State = EntityState.Detached;
            return newEnitity.MapToItem<TEntity, TViewModel>();
        }

        public void InsertRange(List<TViewModel> insertList)
        {
            IEnumerable<TEntity> item = insertList.MapToList<TViewModel, TEntity>();
            dbSet.AddRange(item);
        }

        public bool IsExisted(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (Expression<Func<TEntity, object>> property in includeProperties)
                {
                    query = query.Include(property);
                }
            }
            return query.Any();
        }

        public void Update(TViewModel updateItem)
        {
            TEntity item = updateItem.MapToItem<TViewModel, TEntity>();
            dbSet.Attach(item);
            myDbContext.Entry(item).State = EntityState.Modified;
        }

        public TViewModel UpdateAndSaveChange(TViewModel updateItem)
        {
            TEntity item = updateItem.MapToItem<TViewModel, TEntity>();
            dbSet.Attach(item);
            myDbContext.Entry(item).State = EntityState.Modified;
            myDbContext.SaveChanges();
            myDbContext.Entry(item).State = EntityState.Detached;
            return item.MapToItem<TEntity, TViewModel>();
        }

        public void UpdateRange(List<TViewModel> updateList)
        {
            IEnumerable<TEntity> item = updateList.MapToList<TViewModel, TEntity>();
            dbSet.UpdateRange(item);
        }
    }
}
