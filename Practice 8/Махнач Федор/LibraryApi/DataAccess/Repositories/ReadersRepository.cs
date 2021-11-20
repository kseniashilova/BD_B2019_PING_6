using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LibraryApi.DataAccess.ConnectionFactories;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Repositories;

namespace LibraryApi.DataAccess.Repositories
{
    public class ReadersRepository : IReadersRepository
    {
        private readonly ILibraryDbConnectionFactory _connectionFactory;

        public ReadersRepository(ILibraryDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Reader?> GetAsync(long readerId, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  SELECT id,
                         last_name,
                         first_name,
                         address,
                         birth_date
                  FROM reader
                  WHERE id = :id;
            ";

            var parameters = new
            {
                id = readerId
            };

            var command = new CommandDefinition(
                query,
                parameters,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetRandomConnectionAsync(ct);

            var result = await connection.QueryFirstAsync<Reader>(command);

            return result;
        }

        public async Task<IReadOnlyCollection<Reader>> GetAllAsync(CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  SELECT id,
                         last_name,
                         first_name,
                         address,
                         birth_date
                  FROM reader;
            ";

            var command = new CommandDefinition(
                query,
                null,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetRandomConnectionAsync(ct);

            var result = await connection.QueryAsync<Reader>(command);

            return result.ToArray();
        }

        public async Task<bool> DoesExistAsync(long readerId, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  SELECT EXISTS(
                      SELECT 1
                      FROM reader
                      WHERE id = :id
                  );
            ";

            var parameters = new
            {
                id = readerId
            };

            var command = new CommandDefinition(
                query,
                parameters,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetRandomConnectionAsync(ct);

            var result = await connection.ExecuteScalarAsync<bool>(command);

            return result;
        }

        public async Task<long> CreateAsync(CreateReaderDto newReaderDto, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  INSERT INTO reader (
                         last_name,
                         first_name,
                         address,
                         birth_date)
                  VALUES (:LastName,
                          :FirstName,
                          :Address,
                          :BirthDate)
                  RETURNING id;
            ";

            var command = new CommandDefinition(
                query,
                newReaderDto,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetMasterConnectionAsync(ct);

            var newReaderId = await connection.ExecuteScalarAsync<long>(command);

            return newReaderId;
        }

        public async Task UpdateAsync(Reader readerToUpdate, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  UPDATE reader
                    SET last_name = :LastName,
                        first_name = :FirstName,
                        address = :Address,
                        birth_date = :BirthDate
                    WHERE id = :Id;
            ";

            var command = new CommandDefinition(
                query,
                readerToUpdate,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetMasterConnectionAsync(ct);

            await connection.ExecuteAsync(command);
        }

        public async Task DeleteAsync(long readerId, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  DELETE FROM reader
                  WHERE id = :id;
            ";

            var parameters = new
            {
                id = readerId
            };

            var command = new CommandDefinition(
                query,
                parameters,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetMasterConnectionAsync(ct);

            await connection.ExecuteAsync(command);
        }
    }
}