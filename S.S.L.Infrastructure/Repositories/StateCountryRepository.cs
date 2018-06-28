using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using S.S.L.Infrastructure.S.S.L.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace S.S.L.Infrastructure.Repositories
{
    public class StateCountryRepository : IStateCountryRepository
    {
        private readonly Entities _context;

        public StateCountryRepository(Entities context)
        {
            _context = context;
        }


        /// <summary>
        /// Gets all countries from the database
        /// </summary>
        /// <returns>Task<List<CountryModel></returns>
        public async Task<List<CountryModel>> GetCountriesAsync()
        {
            var query = await _context.Countries
                                        .OrderBy(c => c.Name)
                                        .ToListAsync();

            var countries = query.Select(c =>
                                            new CountryModel
                                            {
                                                Id = c.Id,
                                                Name = c.Name
                                            })
                                            .ToList();

            return countries;

        }



        /// <summary>
        /// Gets all states with the matching countryId from the database
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns>List<StateModel></returns>
        public async Task<List<StateModel>> GetStatesAsync(int? countryId)
        {
            var query = await _context.States
                                .Where(s => s.CountryId == countryId)
                                .ToListAsync();

            var states = query.Select(s =>
                                        new StateModel
                                        {
                                            Name = s.Name,
                                            CountryId = s.CountryId
                                        })
                                         .ToList();
            return states;
        }


    }
}
