using AutoMapper;
using System;
using System.Collections.Generic;

namespace MyCompany.MyProject.Extensions.AutoMapper
{
    public static class AutoMapper
    {  /// <summary>
       /// Convert raw model to new model(item)
       /// </summary>
       /// <typeparam name="TSource">Class of the raw data.</typeparam>
       /// <typeparam name="TDestination">Class of target</typeparam>
       /// <param name="resource">the raw resource which you want to map(convert)</param>

        #region Common Func
        public static TDestination MapToItem<TSource, TDestination>(this TSource resource, Action<IMappingExpression<TSource, TDestination>> action = null)
            where TSource : class
            where TDestination : class
        {
            MapperConfiguration config = new MapperConfiguration(x =>
            {
                if (action != null)
                    action(x.CreateMap<TSource, TDestination>());
                else
                    x.CreateMap<TSource, TDestination>();
            });

            IMapper mapper = config.CreateMapper();
            return resource != null ? mapper.Map<TDestination>(resource) : null;
        }
        /// <summary>
        /// Convert raw model to new model(list or enumerable)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="resource">the raw resource which you want to map</param>
        public static IEnumerable<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> resource, Action<IMappingExpression<TSource, TDestination>> action = null)
    where TSource : class
    where TDestination : class
        {
            MapperConfiguration config = new MapperConfiguration(x =>
            {
                if (action != null)
                    action(x.CreateMap<TSource, TDestination>());
                else
                    x.CreateMap<TSource, TDestination>();
            });

            IMapper mapper = config.CreateMapper();
            return resource != null ? mapper.Map<IEnumerable<TDestination>>(resource) : null;
        }
        #endregion
    }
}
