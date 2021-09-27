using HomeLibrary_API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary_API.Contracts
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<bool> DoesBookExist(int id);
    }
}
