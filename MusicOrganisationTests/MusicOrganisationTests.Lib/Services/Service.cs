using MusicOrganisationTests.Lib.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Services
{
    public abstract class Service<T> where T : class, ITable, new()
    {
        protected TableConnection<T> _table;

        public Service(string path)
        {
            _table = new(path);
        }

        public async Task ClearData()
        {
            await _table.ClearDataAsync();
        }

        public async Task InsertAync(T value)
        {
            await _table.InsertAsync(value);
        }

        public async Task InsertAllAsync(IEnumerable<T> values)
        {
            await _table.InsertAllAsync(values);
        }

        public async Task UpdateAsync(T value)
        {
            await _table.UpdateAsync(value);
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