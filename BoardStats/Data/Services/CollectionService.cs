using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardStats.Data.Services
{
    public class CollectionService : ICollectionService
    {

        private readonly ApplicationDbContext _db;

        public CollectionService(ApplicationDbContext db)
        {
            _db = db;
        }


       
        public void AddCollection([FromBody] Collection col)
        {

            _db.Collections.Add(col);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Collection>> GetAll()
        {
            var result = await _db.Collections.ToListAsync();
            return result;
        }

        public Collection GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Collection Update(int id, Collection newColl)
        {
            throw new NotImplementedException();
        }
    }
}
