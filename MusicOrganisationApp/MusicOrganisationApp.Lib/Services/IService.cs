using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.Services
{
    public interface IService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task DeleteAsync(T value);

        public Task UpdateAsync(T value);
    }
}