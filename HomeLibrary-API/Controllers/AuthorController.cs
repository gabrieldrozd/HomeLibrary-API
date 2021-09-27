using AutoMapper;
using HomeLibrary_API.Contracts;
using HomeLibrary_API.Data.Models;
using HomeLibrary_API.DTOs.Author;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary_API.Controllers
{
    /// <summary>
    /// Endpoint to interact with Authors
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository,IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Authors
        /// </summary>
        /// <returns>List of Authors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();
                var response = _mapper.Map<IList<AuthorDTO>>(authors);

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        /// <summary>
        /// Get Author with specified id
        /// </summary>
        /// <param name="id">Author identification number</param>
        /// <returns>Author object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthor(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                var response = _mapper.Map<AuthorDTO>(author);

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        /// <summary>
        /// Create new Author
        /// </summary>
        /// <param name="author">Author data</param>
        /// <returns>New Author object</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDTO authorDTO)
        {
            try
            {
                if (authorDTO == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var author = _mapper.Map<Author>(authorDTO);
                
                var isSuccess = await _authorRepository.CreateAsync(author);
                if (!isSuccess)
                {
                    return InternalError("Author creation failed");
                }

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        /// <summary>
        /// Update existing Author
        /// </summary>
        /// <param name="id">Author identification number</param>
        /// <param name="author">Author new data</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorUpdateDTO authorDTO)
        {
            try
            {
                if (id < 1 || authorDTO == null || id != authorDTO.Id)
                {
                    return BadRequest();
                }

                var doesItExist = await _authorRepository.DoesAuthorExist(id);
                if (!doesItExist)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var author = _mapper.Map<Author>(authorDTO);

                var isSuccess = await _authorRepository.UpdateAsync(author);
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
        /// Delete existing Author
        /// </summary>
        /// <param name="id">Author identification number</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }

                var doesItExist = await _authorRepository.DoesAuthorExist(id);
                if (!doesItExist)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var author = await _authorRepository.GetByIdAsync(id);

                var isSuccess = await _authorRepository.DeleteAsync(author);
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
