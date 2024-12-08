using Microsoft.EntityFrameworkCore;
using QuizWorkShop.IRepository;
using QuizWorkShop.Models;

namespace QuizWorkShop.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly QuizContext context;
        private readonly DbSet<T> dbSet;
        public GenericRepository(QuizContext _context)
        {
            context = _context;
            dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = dbSet.Find(id);
            if (entity != null) { 
                dbSet.Remove(entity);
            }
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }


        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
            //context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }
    }
}
