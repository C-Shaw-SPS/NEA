using MusicOrganisationTests.Lib.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Services
{
    public abstract class Service<T> where T : class, ISqlStorable, new()
    {
        protected TableConnection<T> _table;

        public Service(string path)
        {
            _table = new(path);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _table.GetAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.GetAllAsync();
        }
    }
}