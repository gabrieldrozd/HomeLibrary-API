using HomeLibrary_API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary_API.Contracts
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<bool> DoesAuthorExist(int id);
    }
}
