using Microsoft.EntityFrameworkCore;
using RepositryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositryLayer
{
    public class Repositry<Tentity> : IRepositry<Tentity> where Tentity : class
    {
        protected readonly DbContext Context;
        public Repositry(DbContext context)
        {
            Context= context;
        }
        public void Add(Tentity entity)
        {
            Context.Set<Tentity>().Add(entity);
        }
        public void Update(Tentity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                Context.ChangeTracker.Clear();
                Context.Set<Tentity>().Add(entity);
                Context.Entry<Tentity>(entity).State = EntityState.Modified;
            }
        }
        public void Delete(Tentity entity)
        {
            Context.Set<Tentity>().Remove(entity);
        }

        public IQueryable<Tentity> FindBy(Expression<Func<Tentity, bool>> predicate)
        {
            return Context.Set<Tentity>().Where(predicate);
        }

        public Tentity Get(int id)
        {
            //TODO
            return Context.Set<Tentity>().Find(id);
        }

        public IEnumerable<Tentity> GetAll()
        {
            return Context.Set<Tentity>().ToList();
        }
       
    }
}
