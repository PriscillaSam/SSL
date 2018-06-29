using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Infrastructure.S.S.L.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Repositories
{
    public class MenteeRepository : IMenteeRepository
    {
        private readonly Entities _context;

        public MenteeRepository(Entities context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new mentee object with userId of a new user and adds it to the database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AddMentee(int userId)
        {

            //check if the user exists?

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new Exception("This user does not exist");


            //create new mentee object
            var mentee = new Mentee
            {
                UserId = userId,
            };

            _context.Mentees.Add(mentee);
            await _context.SaveChangesAsync();

        }
    }
}
