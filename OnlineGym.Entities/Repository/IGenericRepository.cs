﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        IEnumerable<T> GetAll(Expression<Func<T,bool>>?predicate=null,string? IncludeWord = null);


        T GetFirstOrDefualt(Expression<Func<T, bool>>? predicate=null, string? IncludeWord = null);

		T last(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null);

		void Add(T entity);

        void Delete(T entity);  

    }
}
