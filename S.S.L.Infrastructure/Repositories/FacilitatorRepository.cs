﻿using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.S.S.L.Entities;
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

        public async Task<List<UserModel>> GetMenteesByFacilitator(int userId)
        {

            //this id is the userid
            //use it to get the facilitator
            //then get the mentees
            var facilitator = await _context.Facilitators.FirstOrDefaultAsync(f => f.UserId == userId);
            //if (facilitator.User == null) throw new Exception("Sorry, this user does not exist");

            var mentees = await _context.Mentees
                .Where(m => m.FacilitatorId == facilitator.Id)
                .Include(m => m.User)
                .Select(m => new UserModel
                {
                    Id = m.UserId,
                    FirstName = m.User.FirstName,
                    LastName = m.User.LastName,
                    Email = m.User.Email,
                    MobileNumber = m.User.MobileNumber,
                    Gender = m.User.Gender

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
    }
}
