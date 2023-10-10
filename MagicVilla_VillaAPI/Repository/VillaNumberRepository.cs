using MagicVilla_VillaAPI.Repository.IRepository;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDBContext _db;

        public VillaNumberRepository(ApplicationDBContext db): base(db)
        {
            _db = db;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            
            _db.VillasNumbers.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
