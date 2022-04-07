using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface ICollectionService
    {

        Task<IEnumerable<Collection>> GetAll();
        Collection GetById(int id);
        public void AddCollection(Collection col);
        Collection Update(int id, Collection newColl);
        void Delete(int id);
    }
}
