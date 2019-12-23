using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Voca.DAL.Exceptions;

namespace Voca.DAL.Repositories
{
    public class Repository<Entity> where Entity : class
    {
        protected DbContext Context;


        public Repository()
        {
            Context = new DbContext("VocaEntities");
        }


        public void Add(Entity entity)
        {
            Context.Set<Entity>().Add(entity);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DataException("There was an error while trying to add entity.", e);
            }
        }

        public void AddRange(IEnumerable<Entity> entities)
        {
            Context.Set<Entity>().AddRange(entities);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DataException("There was an error while trying to add range of entities.", e);
            }
        }


        public IEnumerable<Entity> GetAll()
        {
            return Context.Set<Entity>().ToList();
        }

        public Entity GetById(int id)
        {
            return Context.Set<Entity>().Find(id);
        }


        public void Remove(Entity entity)
        {
            Context.Set<Entity>().Remove(entity);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DataException("There was an error while trying to remove entity.", e);
            }
        }

        public void RemoveById(int id)
        {
            Entity entity = Context.Set<Entity>().Find(id);

            if (entity != null)
            {
                Context.Set<Entity>().Remove(entity);

                try
                {
                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new DataException("There was an error while trying to remove entity by id.", e);
                }
            }
        }

        public void RemoveRange(IEnumerable<Entity> entities)
        {
            Context.Set<Entity>().RemoveRange(entities);

            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DataException("There was an error while trying to remove range of entities.", e);
            }
        }
    }
}