using CrimeService.Data;
using CrimeService.Models;
using MongoDB.Driver;

namespace CrimeService.Services.Repositories
{
    public class CrimeRepository : ICrimeRepository
    {
        private readonly ICrimeContext _context;
        public CrimeRepository(ICrimeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Crime>> GetCrimes()
        {
            return await _context
                .Crimes
                .Find(p => true)
                .ToListAsync();
        }

        public async Task AddCrime(Crime crime)
        {
            await _context.Crimes.InsertOneAsync(crime);
        }

        public async Task<Crime> GetCrimeById(string id)
        {
            return await _context
                .Crimes
                .Find(h => h.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateCrime(Crime crime)
        {
            var updateResult = await _context
                .Crimes
                .ReplaceOneAsync(filter: g => g.Id == crime.Id, replacement: crime);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCrime(string id)
        {
            FilterDefinition<Crime> filter = Builders<Crime>.Filter.Eq(p => p.Id, id);

            var deleteResult = await _context
                .Crimes
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
