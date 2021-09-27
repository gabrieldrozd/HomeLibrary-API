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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthorRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<IList<Author>> GetAllAsync()
        {
            var authors = await _db.Authors.ToListAsync();
            return authors;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var author = await _db.Authors.FindAsync(id);
            return author;
        }

        public async Task<bool> CreateAsync(Author entity)
        {
            await _db.Authors.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(Author entity)
        {
            _db.Authors.Update(entity);
            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(Author entity)
        {
            _db.Authors.Remove(entity);
            return await SaveAsync ();
        }

        public async Task<bool> SaveAsync()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> DoesAuthorExist(int id)
        {
            var doesAuthorExist = await _db.Authors.AnyAsync(x => x.Id == id);
            return doesAuthorExist;
        }
    }
}
