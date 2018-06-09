using S.S.L.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.S.L.Domain.Interfaces.Repositories
{
    public interface IStateCountryRepository
    {
        Task<List<StateModel>> GetStatesAsync(int? countryId);
        Task<List<CountryModel>> GetCountriesAsync();
    }
}
