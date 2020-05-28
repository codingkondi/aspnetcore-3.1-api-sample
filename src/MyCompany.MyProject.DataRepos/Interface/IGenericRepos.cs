using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyCompany.MyProject.DataRepos.Interface
{
    public interface IGenericRepos<TEntity, TViewModel>
        where TEntity : class
        where TViewModel : class
    {
        IEnumerable<TViewModel> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>>[] includeProperties = null, 
            int? limit = null);

        bool IsExisted(Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>>[] includeProperties = null);
        TViewModel GetById(object Id);
        TViewModel GetNewItem();
        void Insert(TViewModel viewModel);
        void InsertRange(List<TViewModel> insertList);
        TViewModel InsertAndSaveChange(TViewModel viewModel);
        void DeleteById(object Id);
        void DeleteList(List<TViewModel> DeleteList);
        void Delete(TViewModel entityToDelete);
        void Update(TViewModel entityToUpdate);
        void UpdateRange(List<TViewModel> updateList);
        TViewModel UpdateAndSaveChange(TViewModel updateItem);
    }
}
