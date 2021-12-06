﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.Services
{
    public interface IBooksService
    {
        Task<Book> GetAsync(string isbn, CancellationToken ct);

        Task<IReadOnlyCollection<Book>> GetAllAsync(CancellationToken ct);

        Task CreateAsync(Book newBook, CancellationToken ct);

        Task UpdateAsync(Book bookToUpdate, CancellationToken ct);

        Task DeleteAsync(string isbn, CancellationToken ct);
    }
}