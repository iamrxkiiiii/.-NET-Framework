using DapperMvcDemo.Data.Models.Domain;

namespace DapperMvcDemo.Data.Repository
{
    public interface IPersonRepository
    {
        Task<bool> AddAsync(Person person);        // Inserts and returns generated Id (via implementation)
        Task<bool> UpdateAsync(Person person);     // Updates existing person
        Task<bool> DeleteAsync(int id);            // Deletes by Id
        Task<Person?> GetByIdAsync(int id);        // Fetches by Id
        Task<IEnumerable<Person>> GetAllAsync();   // Fetches all
    }
}
