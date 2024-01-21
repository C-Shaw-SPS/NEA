using MusicOrganisation.Lib.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.Services
{
    public interface IService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task UpdateAsync(T value);

        public Task DeleteAsync(T value);
    }
}