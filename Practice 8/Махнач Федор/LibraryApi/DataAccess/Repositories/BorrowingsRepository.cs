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
    public class BorrowingsRepository : IBorrowingsRepository
    {
        private readonly ILibraryDbConnectionFactory _connectionFactory;

        public BorrowingsRepository(ILibraryDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IReadOnlyCollection<Borrowing>> SearchAsync(SearchBorrowingDto searchDto, CancellationToken ct)
        {
            // language=sql
            const string queryTemplate = @"
                  SELECT br.reader_id,
                         r.last_name,
                         r.first_name,
                         b.isbn,
                         b.title,
                         b.author,
                         br.copy_number,
                         br.return_date
                  FROM borrowing AS br
                    INNER JOIN reader r ON br.reader_id = r.id
                    INNER JOIN book b ON br.isbn = b.isbn
                  /**where**/
            ";

            var builder = new SqlBuilder();
            var selector = builder.AddTemplate(queryTemplate);

            if (searchDto.ReaderId.HasValue)
            {
                builder.Where("br.reader_id = :ReaderId");
            }

            if (!string.IsNullOrEmpty(searchDto.ISBN))
            {
                builder.Where("br.isbn = :ISBN");
            }

            if (searchDto.ReturnDateFrom.HasValue)
            {
                builder.Where("br.return_date >= :ReturnDateFrom");
            }

            if (searchDto.ReturnDateTo.HasValue)
            {
                builder.Where("br.return_date <= :ReturnDateTo");
            }

            var command = new CommandDefinition(
                selector.RawSql,
                searchDto,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetRandomConnectionAsync(ct);

            var result = await connection.QueryAsync<Borrowing>(command);

            return result.ToArray();
        }

        public async Task CreateAsync(CreateBorrowingDto newBorrowingDto, CancellationToken ct)
        {
            // language=sql
            const string query = @"
                  INSERT INTO borrowing (
                         reader_id,
                         isbn,
                         copy_number,
                         return_date)
                  VALUES (:ReaderId,
                          :ISBN,
                          :CopyNumber,
                          :ReturnDate);
            ";

            var command = new CommandDefinition(
                query,
                newBorrowingDto,
                cancellationToken: ct);

            await using var connection = await _connectionFactory.GetMasterConnectionAsync(ct);

            await connection.ExecuteAsync(command);
        }
    }
}