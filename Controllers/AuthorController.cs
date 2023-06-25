using AutoMapper;
using AS_Orientacao_Objetos.Domain.DTOs;
using AS_Orientacao_Objetos.Domain.ViewModels;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AS_Orientacao_Objetos.Controllers
{
    [Route("api/author/")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthoRepository authorRepo;
        private readonly IMapper mapper;

        public AuthorController(IAuthoRepository _authorRepo, IMapper _mapper)
        {
            authorRepo = _authorRepo;
            mapper = _mapper;
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var allthors = await authorRepo.GetAll();
            var DTOAuthors = mapper.Map<List<AuthorDTO>>(allthors);
            foreach (var DThors in DTOAuthors)
            {
                var author = allthors.FirstOrDefault(a=>a.Id==DThors.Id);
                if(author != null)
                {
                    DThors.AuthorBooks = mapper.Map<List<AuthorBooksReferenceDTO>>(author.AuthorBooks);
                }
            }
            return Ok(DTOAuthors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var auth = await authorRepo.GetById(id);
            if(auth!=null)
            {
                var DTOauthor = mapper.Map<AuthorDTO>(auth);
                DTOauthor.AuthorBooks = mapper.Map<List<AuthorBooksReferenceDTO>>(auth.AuthorBooks);
            }
            return Ok(auth);
        }

        [HttpPost]
        public IActionResult Save([FromBody]AuthorViewModel nAuthor)
        {
            var authorEnt = mapper.Map<Author>(nAuthor);
            if(authorRepo.Save(authorEnt))
            {
                return Ok(mapper.Map<AuthorDTO>(authorEnt));
            }else{
                return Conflict(new {
                    sttsCode = 409,
                    message = "Conflito ao salvar"
                });
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id, [FromBody]AuthorViewModel uauthor)
        {
            var author = authorRepo.GetById(id).Result;
            if(author!=null)
            {
                var authorEnt = mapper.Map<Author>(uauthor);
                authorEnt.Id = id;
                if(authorRepo.Update(authorEnt))
                {
                    var authMod = mapper.Map<AuthorDTO>(authorEnt);
                    authMod.AuthorBooks = mapper.Map<List<AuthorBooksReferenceDTO>>(author.AuthorBooks);
                    return Ok(authMod);
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
            if(authorRepo.Delete(id))
            {
                return Ok(new {
                    sttsCode = 200,
                    message = "Autor removido com sucesso"
                });
            }else{
                return BadRequest(new {
                    sttsCode = 400,
                    message = "Id não encontrada"
                });
            }
        }
    }
}