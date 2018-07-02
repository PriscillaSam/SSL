using S.S.L.Domain.Enums;
using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.S.S.L.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        /// <summary>
        /// Gets all mentees currently being mentored
        /// </summary>
        /// <returns>List of User models</returns>
        public async Task<List<UserModel>> GetMentees(bool mentored)
        {
            var mentees = from mentee in _context.Mentees
                          let isMentored = mentored ? mentee.Facilitator != null : mentee.Facilitator == null
                          from user in _context.Users
                          where user.UserType == UserType.Mentee
                          where mentee.UserId == user.Id && isMentored
                          select new UserModel
                          {
                              FirstName = user.FirstName,
                              LastName = user.LastName,
                              Email = user.Email,
                              Id = user.Id
                          };

            return await mentees.ToListAsync();

        }


        public async Task AssignOrUpdateFacilitator(int menteeId, int facilitatorId)
        {
            var mentee = await _context.Mentees.FirstOrDefaultAsync(m => m.UserId == menteeId);
            if (mentee == null) throw new Exception("Sorry, this mentee does not exist");

            var facilitator = await _context.Facilitators.FirstOrDefaultAsync(f => f.UserId == facilitatorId);
            mentee.FacilitatorId = facilitator.Id;
            _context.Entry(mentee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }


        public async Task<UserModel> GetMentee(int userId)
        {
            var mentee = await _context.Mentees
                .Where(m => m.UserId == userId)
                .Include(m => m.User)
                .Select(m => new UserModel
                {
                    Id = m.UserId,
                    FirstName = m.User.FirstName,
                    LastName = m.User.LastName,
                    Gender = m.User.Gender,

                }).FirstOrDefaultAsync();

            if (mentee == null) throw new Exception("Sorry, this user does not exist.");

            return mentee;
        }


    }
}
