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
    public class FacilitatorRepository : IFacilitatorRepository
    {
        private readonly Entities _context;

        public FacilitatorRepository(Entities context)
        {
            _context = context;
        }

        public async Task<List<MenteeUserModel>> GetMenteesByFacilitator(int userId)
        {

            //this id is the userid
            //use it to get the facilitator
            //then get the mentees
            var facilitator = await _context.Facilitators.FirstOrDefaultAsync(f => f.UserId == userId);
            //if (facilitator.User == null) throw new Exception("Sorry, this user does not exist");

            var mentees = await _context.Mentees
                .Where(m => m.FacilitatorId == facilitator.Id)
                .Include(m => m.User)
                .Select(m => new MenteeUserModel
                {
                    User = new UserModel
                    {
                        Id = m.UserId,
                        FirstName = m.User.FirstName,
                        LastName = m.User.LastName,
                        Email = m.User.Email,
                        MobileNumber = m.User.MobileNumber,
                        Gender = m.User.Gender
                    },

                    FinishedClasses = m.FinishedClass

                }).ToListAsync();

            return mentees;
        }

        public async Task<List<UserModel>> GetFacilitators(string gender)
        {

            var facilitators = await _context.Facilitators
                .Include(f => f.User)
                .Where(fu => !fu.User.IsDeleted)
                .Select(fu => new UserModel
                {
                    Id = fu.UserId,
                    FirstName = fu.User.FirstName,
                    LastName = fu.User.LastName,
                    Gender = fu.User.Gender

                }).ToListAsync();

            if (gender == "Male" || gender == "Female")
            {
                return facilitators.Where(u => u.Gender == gender).ToList();
            }

            return facilitators;

        }

        public async Task UpdateMenteeProgressAsync(int menteeId, int facilitatorId)
        {

            //check that facilitator exists

            var facilitator = await _context.Facilitators.FirstOrDefaultAsync(f => f.UserId == facilitatorId);

            if (facilitator == null) throw new Exception("This facilitator does not exist");

            //check that facilitator is mentoring the mentee 
            var mentee = await _context.Mentees.FirstOrDefaultAsync(m => m.UserId == menteeId);
            if (mentee == null)
                throw new Exception("This mentee does not exist");
            else if (mentee.FacilitatorId != facilitator.Id)
                throw new Exception("You are not authorized to perform this operation");

            mentee.FinishedClass = true;
            _context.Entry(mentee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}
