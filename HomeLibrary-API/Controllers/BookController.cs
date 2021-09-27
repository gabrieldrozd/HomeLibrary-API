using AutoMapper;
using HomeLibrary_API.Contracts;
using HomeLibrary_API.Data.Models;
using HomeLibrary_API.DTOs.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary_API.Controllers
{
    /// <summary>
    /// Endpoint to interact with Books
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Books
        /// </summary>
        /// <returns>List of Books</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookRepository.GetAllAsync();
                var response = _mapper.Map<IList<BookDTO>>(books);

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        /// <summary>
        /// Get Book with specified id
        /// </summary>
        /// <param name="id">Book identification number</param>
        /// <returns>Book object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                var response = _mapper.Map<BookDTO>(book);

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        /// <summary>
        /// Create new Book
        /// </summary>
        /// <param name="book">Book data</param>
        /// <returns>New Book object</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAuthor([FromBody] BookCreateDTO bookDTO)
        {
            try
            {
                if (bookDTO == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var book = _mapper.Map<Book>(bookDTO);

                var isSuccess = await _bookRepository.CreateAsync(book);
                if (!isSuccess)
                {
                    return InternalError("Author creation failed");
                }

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        /// <summary>
        /// Update existing Book
        /// </summary>
        /// <param name="id">Book identification number</param>
        /// <param name="author">Book new data</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDTO bookDTO)
        {
            try
            {
                if (id < 1 || bookDTO == null || id != bookDTO.Id)
                {
                    return BadRequest();
                }

                var doesItExist = await _bookRepository.DoesBookExist(id);
                if (!doesItExist)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var book = _mapper.Map<Book>(bookDTO);

                var isSuccess = await _bookRepository.UpdateAsync(book);
                if (!isSuccess)
                {
                    return InternalError("Author update failed");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        /// <summary>
        /// Delete existing Book
        /// </summary>
        /// <param name="id">Book identification number</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }

                var doesItExist = await _bookRepository.DoesBookExist(id);
                if (!doesItExist)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var book = await _bookRepository.GetByIdAsync(id);

                var isSuccess = await _bookRepository.DeleteAsync(book);
                if (!isSuccess)
                {
                    return InternalError("Author delete failed");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        private ObjectResult InternalError(Exception exception)
        {
            return StatusCode(500, $"{exception.Message}\n{exception.InnerException}");
        }

        private ObjectResult InternalError(string message)
        {
            return StatusCode(500, $"{message}");
        }
    }
}
