using Microsoft.EntityFrameworkCore;
using OnlineGym.DataAccess.Data;
using OnlineGym.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.DataAccess.RepositoryImplementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly OnlineGymContext _context;
        private DbSet<T> _dbSet;


        public GenericRepository(OnlineGymContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet?.Remove(entity);
        }

        public  IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate=null, string? IncludeWord = null)
        {
            IQueryable<T> query = _dbSet;

            if(predicate != null)
            {
                query = query.Where(predicate);
            }
           
            if (IncludeWord != null)
            {
                foreach (var item in IncludeWord.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                 
                   query= query.Include(item.Trim());

                }
            }

         
            return query.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (IncludeWord != null)
            {
                foreach (var item in IncludeWord.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(item.Trim());

                }
            }


            return await query.ToListAsync();
        }

        public T GetFirstOrDefualt(Expression<Func<T, bool>>? predicate=null, string? IncludeWord = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (IncludeWord != null)
            {
                foreach (var item in IncludeWord.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                   query= query.Include(item);

                }
            }


            return query.FirstOrDefault();
        }

        public async Task<T> GetFirstOrDefualtAsync(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (IncludeWord != null)
            {
                foreach (var item in IncludeWord.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);

                }
            }


            return await query.FirstOrDefaultAsync();
        }

        public T last(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null)
		{
			IQueryable<T> query = _dbSet;

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (IncludeWord != null)
			{
				foreach (var item in IncludeWord.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query=query.Include(item);

				}
			}

            return query.ToList().LastOrDefault();
		}
	}
}
