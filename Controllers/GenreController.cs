using AutoMapper;
using AS_Orientacao_Objetos.Domain.DTOs;
using AS_Orientacao_Objetos.Domain.ViewModels;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AS_Orientacao_Objetos.Controllers
{
    [Route("api/genre/")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository genreRepo;
        private readonly IMapper mapper;

        public GenreController(IGenreRepository _genreRepo, IMapper _mapper)
        {
            genreRepo = _genreRepo;
            mapper = _mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = mapper.Map<List<GenreDTO>>(await genreRepo.GetAll());
            return Ok(genres);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = mapper.Map<GenreDTO>(await genreRepo.GetById(id));
            return Ok(genre);
        }

        [HttpPost]
        public IActionResult Save([FromBody]GenreViewModel nGenre)
        {
            var genreEnt = mapper.Map<Genre>(nGenre);
            if(genreRepo.Save(genreEnt))
            {
                return Ok(mapper.Map<GenreDTO>(genreEnt));
            }else{      //Already exists
                return Conflict(new {
                    sttsCode = 409,
                    message = "Conflito ao salvar"
                });
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id, [FromBody]GenreViewModel uGenre)
        {
            var genreEnt = mapper.Map<Genre>(uGenre);
            genreEnt.Id = id;
            if(genreRepo.Update(genreEnt))
            {
                return Ok(mapper.Map<GenreDTO>(genreEnt));
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
            if(genreRepo.Delete(id))
            {
                return Ok(new {
                    sttsCode = 200,
                    message = "Gênero removido com sucesso"
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