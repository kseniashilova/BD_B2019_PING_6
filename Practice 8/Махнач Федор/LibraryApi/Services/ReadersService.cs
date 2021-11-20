using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Dto;
using LibraryApi.Domain.Exceptions;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Repositories;
using LibraryApi.Domain.Services;

namespace LibraryApi.Services
{
    public class ReadersService : IReadersService
    {
        private readonly IReadersRepository _readersRepository;

        public ReadersService(IReadersRepository readersRepository)
        {
            _readersRepository = readersRepository;
        }

        public async Task<Reader> GetAsync(long readerId, CancellationToken ct)
        {
            var reader = await _readersRepository.GetAsync(readerId, ct);

            if (reader is null)
            {
                throw new EntityNotFoundException($"Reader with ID='{readerId}' not found.");
            }

            return reader;
        }

        public Task<IReadOnlyCollection<Reader>> GetAllAsync(CancellationToken ct)
        {
            return _readersRepository.GetAllAsync(ct);
        }

        public Task<long> CreateAsync(CreateReaderDto newReaderDto, CancellationToken ct)
        {
            return _readersRepository.CreateAsync(newReaderDto, ct);
        }

        public async Task UpdateAsync(Reader readerToUpdate, CancellationToken ct)
        {
            var readerExists = await _readersRepository.DoesExistAsync(readerToUpdate.Id, ct);

            if (!readerExists)
            {
                throw new EntityNotFoundException($"Reader with ID='{readerToUpdate.Id}' does not exist.");
            }

            await _readersRepository.UpdateAsync(readerToUpdate, ct);
        }

        public async Task DeleteAsync(long readerId, CancellationToken ct)
        {
            var readerExists = await _readersRepository.DoesExistAsync(readerId, ct);

            if (!readerExists)
            {
                throw new EntityNotFoundException($"Reader with ID='{readerId}' does not exist.");
            }

            await _readersRepository.DeleteAsync(readerId, ct);
        }
    }
}