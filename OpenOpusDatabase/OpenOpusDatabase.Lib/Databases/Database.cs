﻿using SQLite;
using Microsoft.Data.Sqlite;

namespace OpenOpusDatabase.Lib.Databases
{
    public class Database<T> where T : class, ISqlStorable, new()
    {
        private SQLiteAsyncConnection _connection;
        private readonly string _path;

        public Database(string path)
        {
            _path = path.FormatAsDatabasePath();
        }

        protected async Task InitAsync()
        {
            if (_connection is not null)
            {
                return;
            }

            _connection = new SQLiteAsyncConnection(_path, DatabaseProperties.FLAGS);
            await _connection.CreateTableAsync<T>();
        }

        public async Task InsertAsync(T value)
        {
            await InitAsync();
            InsertCommand<T> insertCommand = new();
            insertCommand.AddValue(value);
            await _connection.ExecuteAsync(insertCommand.ToString());
        }

        public async Task InsertAllAsync(IEnumerable<T> values)
        {
            await InitAsync();
            InsertCommand<T> insertCommand = new();
            foreach (T value in values)
            {
                insertCommand.AddValue(value);
            }
            await _connection.ExecuteAsync(insertCommand.ToString());
        }

        public async Task DeleteAsync(T value)
        {
            await InitAsync();
            await _connection.DeleteAsync(value);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            await InitAsync();
            IEnumerable<T> result = await _connection.QueryAsync<T>($"SELECT * FROM {T.TableName}");
            return result;
        }

        public async Task<T> GetAsync(int id)
        {
            await InitAsync();
            IList<T> result = await _connection.QueryAsync<T>($"SELECT * FROM {T.TableName} WHERE {nameof(ISqlStorable.Id)} = {id}");
            if (result.Count == 0)
                throw new Exception($"No row in {T.TableName} with {nameof(ISqlStorable.Id)} {id}");
            return result[0];
        }

        public async Task ClearAsync()
        {
            await InitAsync();
            await _connection.DeleteAllAsync<T>();
        }

        public async Task UpdateAsync(T value)
        {
            await InitAsync();
            await _connection.UpdateAsync(value);
        }

        public async Task<IEnumerable<int>> GetIdsAsync()
        {
            await InitAsync();
            IEnumerable<T> result = await _connection.QueryAsync<T>($"SELECT {nameof(ISqlStorable.Id)} FROM {T.TableName}");
            return result.Select(c => c.Id);
        }

        public async Task<int> GetNextIdAsync()
        {
            await InitAsync();
            IList<T> result = await _connection.QueryAsync<T>($"SELECT Max({nameof(ISqlStorable.Id)}) AS {nameof(ISqlStorable.Id)} FROM {T.TableName}");
            if (result.Count > 0)
            {
                return result[0].Id + 1;
            }
            else
            {
                return 0;
            }
        }
    }
}