using AutoMapper;
using AS_Orientacao_Objetos.Domain.DTOs;
using AS_Orientacao_Objetos.Domain.ViewModels;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AS_Orientacao_Objetos.Controllers
{
    [Route("api/book/")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookRepo;
        private readonly IMapper mapper;

        public BookController(IBookRepository _bookRepo, IMapper _mapper)
        {
            bookRepo = _bookRepo;
            mapper = _mapper;
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var booksF = await bookRepo.GetAll();
            var DTObooks = mapper.Map<List<BookDTO>>(booksF);
            foreach(var B in DTObooks)
            {
                var findBook = booksF.FirstOrDefault(a=>a.Id==B.Id);
                if(findBook!=null)
                {
                    B.AuthorBooks = mapper.Map<List<AuthorBooksReferenceDTO>>(findBook.AuthorBooks);
                }
            }
            return Ok(DTObooks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await bookRepo.GetById(id);
            if(book!=null)
            {
                var DTOBook = mapper.Map<BookDTO>(book);
                DTOBook.AuthorBooks = mapper.Map<List<AuthorBooksReferenceDTO>>(book.AuthorBooks);
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Save([FromBody]BookViewModel nBook)
        {
            var bookEnt = mapper.Map<Book>(nBook);
            if(bookRepo.Save(bookEnt))
            {
                return Ok(mapper.Map<BookDTO>(bookEnt));
            }else{      //Already exists
                return Conflict(new {
                    sttsCode = 409,
                    message = "Conflito ao salvar"
                });
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id, [FromBody]BookViewModel ubook)
        {
            var book = bookRepo.GetById(id).Result;
            if(book!=null)
            {
                var bookEnt = mapper.Map<Book>(ubook);
                bookEnt.Id = id;
                if(bookRepo.Update(bookEnt))
                {
                    // var retBookEnt = mapper.Map<BookDTO>(bookEnt);
                    // retBookEnt.GenreName = bookEnt.Genre.Name;
                    // retBookEnt.DonatorName = bookEnt.DonatedBy.Name;
                    // return Ok(retBookEnt);
                    var bookMod = mapper.Map<BookDTO>(bookEnt);
                    bookMod.AuthorBooks = mapper.Map<List<AuthorBooksReferenceDTO>>(book.AuthorBooks);
                    bookMod.DonatorName = book.DonatedBy.Name;
                    bookMod.GenreName = book.Genre.Name;
                    return Ok(bookMod);
                }else{
                    return BadRequest(new {
                        sttsCode = 400,
                        message = "Id não encontrada"
                    });
                }
            }else{
                return BadRequest(new {
                    sttsCode = 400,
                    message = "Id não encontrada"
                });
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if(bookRepo.Delete(id))
            {
                return Ok(new {
                    sttsCode = 200,
                    message = "Livro removido com sucesso"
                });
            }else{
                return BadRequest(new {
                    sttsCode = 400,
                    message = "Id não encontrada"
                });
            }
        }
        //-------------------------------
        
        [HttpGet("available")]
        public async Task<IActionResult> GetAllAvailable()
        {
            var booksAvailable = await bookRepo.GetAvailable();
            var DTObookAvailable = mapper.Map<List<BookDTO>>(booksAvailable);
            foreach (var booksAval in DTObookAvailable)
            {
                var availaBook = booksAvailable.FirstOrDefault(u=>u.Id==booksAval.Id);
                booksAval.AuthorBooks = mapper.Map<List<AuthorBooksReferenceDTO>>(availaBook.AuthorBooks);
            }
            return Ok(DTObookAvailable);
        }
        [HttpGet("donor/{donorId:int}")]
        public async Task<IActionResult> GetAllDonator(int donorId)
        {
            var booksAvailable = await bookRepo.GetByDonator(donorId);
            var DTObookAvailable = mapper.Map<List<BookDTO>>(booksAvailable);
            foreach (var booksAval in DTObookAvailable)
            {
                var availaBook = booksAvailable.FirstOrDefault(u=>u.Id==booksAval.Id);
                booksAval.AuthorBooks = mapper.Map<List<AuthorBooksReferenceDTO>>(availaBook.AuthorBooks);
                booksAval.DonatorName = availaBook.DonatedBy.Name;
                booksAval.GenreName = availaBook.Genre.Name;
            }
            return Ok(DTObookAvailable);
        }
        [HttpGet("byGenre/{genreId:int}")]
        public async Task<IActionResult> GetAllGenre(int genreId)
        {
            var booksGenre = await bookRepo.GetByGenre(genreId);
            var DTObookAvailable = mapper.Map<List<BookDTO>>(booksGenre);
            foreach (var booksAval in DTObookAvailable)
            {
                var availaBook = booksGenre.FirstOrDefault(u=>u.Id==booksAval.Id);
                booksAval.AuthorBooks = mapper.Map<List<AuthorBooksReferenceDTO>>(availaBook.AuthorBooks);
                booksAval.DonatorName = availaBook.DonatedBy.Name;
                booksAval.GenreName = availaBook.Genre.Name;
            }
            return Ok(DTObookAvailable);
        }
    }
}