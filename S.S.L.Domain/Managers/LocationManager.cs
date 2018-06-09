using S.S.L.Domain.Interfaces.Repositories;
using S.S.L.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.S.L.Domain.Managers
{
    public class LocationManager
    {
        private readonly IStateCountryRepository _repo;

        public LocationManager(IStateCountryRepository repo)
        {
            _repo = repo;
        }


        public async Task<List<StateModel>> GetStates(int? countryId)
        {
            var states = await _repo.GetStatesAsync(countryId);
            return states;
        }

        public async Task<List<CountryModel>> GetCountries()
        {
            var countries = await _repo.GetCountriesAsync();
            return countries;

        }

    }
}
