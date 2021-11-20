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
    public class BooksRepository : IBooksRepository
    {
        private readonly ILibraryDbConnectionFactory _connectionFactory;

        public BooksRepository(ILibraryDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Book?> GetAsync(string isbn, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  SELECT isbn,
                         title,
                         author,
                         pages_num,
                         publish_year,
                         publisher_name
                  FROM book
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

            var result = await connection.QueryFirstAsync<Book>(command);

            return result;
        }

        public async Task<IReadOnlyCollection<Book>> GetAllAsync(CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  SELECT isbn,
                         title,
                         author,
                         pages_num,
                         publish_year,
                         publisher_name
                  FROM book;
            ";

            var command = new CommandDefinition(
                query,
                null,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetRandomConnectionAsync(ct);

            var result = await connection.QueryAsync<Book>(command);

            return result.ToArray();
        }

        public async Task<bool> DoesExistAsync(string isbn, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  SELECT EXISTS(
                      SELECT 1
                      FROM book
                      WHERE isbn = :isbn
                  );
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

            var result = await connection.ExecuteScalarAsync<bool>(command);

            return result;
        }

        public async Task CreateAsync(Book newBook, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  INSERT INTO book (
                         isbn,
                         title,
                         author,
                         pages_num,
                         publish_year,
                         publisher_name)
                  VALUES (:ISBN,
                          :Title,
                          :Author,
                          :PagesNum,
                          :PublishYear,
                          :PublisherName);
            ";

            var command = new CommandDefinition(
                query,
                newBook,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetMasterConnectionAsync(ct);

            await connection.ExecuteAsync(command);
        }

        public async Task UpdateAsync(Book bookToUpdate, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  UPDATE book
                    SET title = :Title,
                        author = :Author,
                        pages_num = :PagesNum,
                        publish_year = :PublishYear,
                        publisher_name = :PublisherName
                    WHERE isbn = :ISBN;
            ";

            var command = new CommandDefinition(
                query,
                bookToUpdate,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetMasterConnectionAsync(ct);

            await connection.ExecuteAsync(command);
        }

        public async Task DeleteAsync(string isbn, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  DELETE FROM book
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

            await using var connection = await _connectionFactory.GetMasterConnectionAsync(ct);

            await connection.ExecuteAsync(command);
        }
    }
}