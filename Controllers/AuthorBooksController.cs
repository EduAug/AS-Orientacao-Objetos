using AutoMapper;
using AS_Orientacao_Objetos.Domain.DTOs;
using AS_Orientacao_Objetos.Domain.ViewModels;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AS_Orientacao_Objetos.Controllers
{
    [Route("api/writer/")]
    public class AuthorBooksController : ControllerBase
    {
        private readonly IAuthorBooksRepository<AuthorBooks> authbookRepo;
        private readonly IMapper mapper;
        
        public AuthorBooksController(IAuthorBooksRepository<AuthorBooks> abRepo, IMapper _mapper)
        {
            authbookRepo = abRepo;
            mapper = _mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            // var authBooks = await authbookRepo.GetAll();
            // var authBooksRet = mapper.Map<List<AuthorBooksDTO>>(authBooks);
            // foreach (var authorsAndBooks in authBooksRet)
            // {
            //     if(authorsAndBooks.Author == null && authorsAndBooks.Book == null)
            //     {
            //         var thisRelationAB = authBooks.FirstOrDefault(u=>u.AuthorId==authorsAndBooks.AuthorId&&u.BookId==authorsAndBooks.BookId);
            //         authorsAndBooks.Author = thisRelationAB.Author.Name;
            //         authorsAndBooks.Book = thisRelationAB.Book.Title;
            //     }
            // }
            // return Ok(authBooksRet);
            var ABs = mapper.Map<List<AuthorBooksDTO>>(await authbookRepo.GetAll());
            return Ok(ABs);
        }

        [HttpGet("{idBook:int}/{idAuthor:int}")]
        public async Task<IActionResult> GetById(int idBook, int idAuthor)
        {
            var ABRelation = mapper.Map<AuthorBooksDTO>(await authbookRepo.GetById(idBook, idAuthor));
            return Ok(ABRelation);
        }

        [HttpPost]
        public IActionResult Save([FromBody]AuthorBooksViewModel nWriter)
        {
            var auBookEnt = mapper.Map<AuthorBooks>(nWriter);
            if(authbookRepo.Save(auBookEnt))
            {
                return Ok(mapper.Map<AuthorBooksDTO>(auBookEnt));
            }else{      //Already exists
                return Conflict(new {
                    sttsCode = 409,
                    message = "Conflito ao salvar"
                });
            }
        }

        [HttpPut("{idBook:int}/{idAuthor:int}")]
        public IActionResult Update([FromRoute]int idBook, [FromRoute]int idAuthor, [FromBody]AuthorBooksViewModel uAB)
        {
            // var authBooks = authbookRepo.GetById(idBook,idAuthor).Result;
            // if(authBooks != null){
                var ABEnt = mapper.Map<AuthorBooks>(uAB);
                if(authbookRepo.Update(ABEnt))
                {
                    var ABMod = mapper.Map<AuthorBooksDTO>(ABEnt);
                    var bkpAB = authbookRepo.GetById(idBook,idAuthor).Result;
                    //o AuthorBooks "backup" está aqui pois o authBooks, do
                    //começo do método tem seus atributos "Autor" e "livro"
                    //apagados, uma vez que ele procou um dado, que foi
                    //alterado posteriormente, acredito que é por isso que
                    //"perde", então há o "backup", que faz essa consulta
                    //porém agora com os dados novos, que então são
                    //atribuidos ao DTO
                    ABMod.Author = bkpAB.Author.Name;
                    ABMod.Book = bkpAB.Book.Title;
                    return Ok(ABMod);
                }else{
                    return BadRequest(new {
                        sttsCode = 400,
                        message = "Id não encontrada"
                    });
                }
            // }else{
            //     return BadRequest(new {
            //         sttsCode = 400,
            //         message = "Id não encontrado"
            //     });
            // }
        }

        [HttpDelete("{idBook:int}/{idAuthor:int}")]
        public IActionResult Delete(int idBook, int idAuthor)
        {
            if(authbookRepo.Delete(idBook, idAuthor))
            {
                return Ok(new {
                    sttsCode = 200,
                    message = "Relação de autor e livro removida com sucesso"
                });
            }else{
                return BadRequest(new {
                    sttsCode = 400,
                    message = "Esse autor não escreve esse livro"
                });
            }
        }
        //----------------------------------------------
        [HttpGet("byAuthor/{authorId:int}")]
        public async Task<IActionResult> GetByAuthor(int authorId)
        {
            var booksBy = authbookRepo.GetBooksByAuthor(authorId).Result;
            var BooksDto = mapper.Map<List<BookDTO>>(booksBy);
            foreach (var booksFoundBy in BooksDto)
            {
                //Referência circular; Já estou procurando os books
                //pelo autor inserido, então authorbooks
                //deve voltar como [] ?
                var findRel = mapper.Map<List<AuthorBooksReferenceDTO>>(booksFoundBy.AuthorBooks);
                booksFoundBy.AuthorBooks = findRel;
            }
            return Ok(BooksDto);
        }
    }
}