using DapperMvcDemo.Data.DataAccess;
using DapperMvcDemo.Data.Models.Domain;

namespace DapperMvcDemo.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ISqlDataAccess _db;

        public PersonRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Person person)
        {
            try
            {
                // Get the auto-generated Id after insert
                int newId = await _db.SaveDataWithReturn("sp_create_person", new
                {
                    person.Name,
                    person.Email,
                    person.Address
                });

                person.Id = newId; // Optional: set the generated Id to the model

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            try
            {
                await _db.SaveData("sp_update_person", person);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_delete_person", new { Id = id });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            var result = await _db.GetData<Person, dynamic>("sp_get_person_by_id", new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            string query = "sp_get_people";
            return await _db.GetData<Person, dynamic>(query, new { });
        }
    }
}
