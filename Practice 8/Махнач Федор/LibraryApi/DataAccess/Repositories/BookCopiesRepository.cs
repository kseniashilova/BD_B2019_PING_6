using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LibraryApi.DataAccess.ConnectionFactories;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Repositories;

namespace LibraryApi.DataAccess.Repositories
{
    public class BookCopiesRepository : IBookCopiesRepository
    {
        private readonly ILibraryDbConnectionFactory _connectionFactory;

        public BookCopiesRepository(ILibraryDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<BookCopy?> GetAsync(string isbn, int copyNumber, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  SELECT isbn,
                         copy_number,
                         shelf_position
                  FROM copy
                  WHERE isbn = :isbn 
                    AND copy_number = :copyNumber;
            ";

            var parameters = new
            {
                isbn,
                copyNumber
            };

            var command = new CommandDefinition(
                query,
                parameters,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetRandomConnectionAsync(ct);

            var result = await connection.QueryFirstAsync<BookCopy>(command);

            return result;
        }

        public async Task<IReadOnlyCollection<BookCopy>> GetAllByIsbnAsync(string isbn, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  SELECT isbn,
                         copy_number,
                         shelf_position
                  FROM copy
                  WHERE isbn = :isbn;
            ";

            var parameters = new
            {
                isbn
            };

            var command = new CommandDefinition(
                query,
                parameters,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetRandomConnectionAsync(ct);

            var result = await connection.QueryAsync<BookCopy>(command);

            return result.ToArray();
        }

        public async Task<IReadOnlyCollection<BookCopy>> GetAllAsync(CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  SELECT isbn,
                         copy_number,
                         shelf_position
                  FROM copy;
            ";

            var command = new CommandDefinition(
                query,
                null,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetRandomConnectionAsync(ct);

            var result = await connection.QueryAsync<BookCopy>(command);

            return result.ToArray();
        }

        public async Task<bool> DoesExistAsync(string isbn, int copyNumber, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  SELECT EXISTS(
                      SELECT 1
                      FROM copy
                      WHERE isbn = :isbn
                        AND copy_number = :copyNumber
                  );
            ";

            var parameters = new
            {
                isbn,
                copyNumber
            };

            var command = new CommandDefinition(
                query,
                parameters,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetRandomConnectionAsync(ct);

            var result = await connection.ExecuteScalarAsync<bool>(command);

            return result;
        }

        public async Task CreateAsync(BookCopy bookCopy, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  INSERT INTO copy (isbn,
                                    copy_number,
                                    shelf_position)
                  VALUES (:ISBN,
                          :CopyNumber,
                          :ShelfPosition);
            ";

            var command = new CommandDefinition(
                query,
                bookCopy,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetMasterConnectionAsync(ct);

            await connection.ExecuteAsync(command);
        }

        public async Task UpdateAsync(BookCopy bookCopy, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  UPDATE copy
                    SET shelf_position = :ShelfPosition
                    WHERE isbn = :ISBN
                      AND copy_number = :CopyNumber
            ";

            var command = new CommandDefinition(
                query,
                bookCopy,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetMasterConnectionAsync(ct);

            await connection.ExecuteAsync(command);
        }

        public async Task DeleteAsync(string isbn, int copyNumber, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  DELETE FROM copy
                  WHERE isbn = :isbn
                    AND copy_number = :copyNumber;
            ";

            var parameters = new
            {
                isbn,
                copyNumber
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