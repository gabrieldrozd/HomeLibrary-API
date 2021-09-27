using HomeLibrary_API.Contracts;
using HomeLibrary_API.Data;
using HomeLibrary_API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary_API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IList<Book>> GetAllAsync()
        {
            var books = await _db.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var book = await _db.Books.FindAsync(id);
            return book;
        }

        public async Task<bool> CreateAsync(Book entity)
        {
            await _db.Books.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(Book entity)
        {
            _db.Books.Update(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Book entity)
        {
            _db.Books.Remove(entity);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public Task<bool> DoesBookExist(int id)
        {
            var doesBookExist = _db.Books.AnyAsync(x => x.Id == id);
            return doesBookExist;
        }
    }
}
