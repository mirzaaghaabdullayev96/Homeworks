using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_24_EF_MtoM.Models
{
    public static class CrudOperations
    {
        public static void CreateEntity<T>(T entity) where T : class
        {
            using var dbContext = new GarageDbContext();
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
        }

        public static void RemoveEntityById<T>(int id) where T : class
        {
            using var dbContext = new GarageDbContext();
            T entity = dbContext.Set<T>().Find(id) ?? throw new NullReferenceException();
            dbContext.Remove(entity);
            dbContext.SaveChanges();
        }

        static List<T> GetAllEntities<T>() where T : class
        {
            using var dbContext = new GarageDbContext();
            return dbContext.Set<T>().ToList();
        }

        static T GetEntityId<T>(int id) where T : class
        {
            using var dbContext = new GarageDbContext();
            return dbContext.Set<T>().Find(id);
        }

        static void UpdateEntity<T>(int id, T updatedEntity) where T : class
        {
            using var dbContext = new GarageDbContext();
            var dbSet = dbContext.Set<T>();
            var existingEntity = dbSet.Find(id) ?? throw new NullReferenceException($"{typeof(T).Name} with ID {id} not found.");
            var entityType = typeof(T);
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                var newValue = property.GetValue(updatedEntity);
                property.SetValue(existingEntity, newValue);
            }

            dbContext.SaveChanges();
        }
    }
}
