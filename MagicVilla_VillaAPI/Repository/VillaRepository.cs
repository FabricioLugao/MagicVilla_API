using MagicVilla_VillaAPI.Repository.IRepository;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDBContext _db;

        public VillaRepository(ApplicationDBContext db): base(db)
        {
            _db = db;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
