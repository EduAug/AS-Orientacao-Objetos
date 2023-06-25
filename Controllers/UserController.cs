using AutoMapper;
using AS_Orientacao_Objetos.Domain.DTOs;
using AS_Orientacao_Objetos.Domain.ViewModels;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AS_Orientacao_Objetos.Controllers
{
    [Route("api/user/")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;
        
        public UserController(IUserRepository _userRepo, IMapper _mapper)
        {
            userRepo = _userRepo;
            mapper = _mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            //Retorna a lista completa, com direito aos "tipos objeto"
            var users = await userRepo.GetAll();
            //Faz a conversão para o DTO, eliminando os tipos que causam "loop reference"
            var usersRet = mapper.Map<List<UserDTO>>(users);
            //Procurando em cada membro da lista seu "donated book" faltando e atribuindo o valor vindo do entity ao invés do entityDTO
            foreach (var usersDonated in usersRet)
            {
                if(usersDonated.DonatedBooks == null)
                //se o atributo for nulo
                {//procura pelo usuário em específico
                    var user = users.FirstOrDefault(u=>u.Id==usersDonated.Id);
                    //e atribui uma lista de livros, passada pelo BookDTO para converter
                    usersDonated.DonatedBooks = mapper.Map<List<BookDTO>>(user.DonatedBooks);
                }
            }
            return Ok(usersRet);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await userRepo.GetById(id);
            if(user != null){
                var userRet = mapper.Map<UserDTO>(user);
                //Não é das melhores práticas fazer tratamento na
                //controller, mas é o jeito pra "linkar"
                if(userRet.DonatedBooks == null)
                    userRet.DonatedBooks = mapper.Map<List<BookDTO>>(user.DonatedBooks);
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Save([FromBody]UserViewModel nUser)
        {
            var userEnt = mapper.Map<User>(nUser);
            if(userRepo.Save(userEnt))
            {
                return Ok(mapper.Map<UserDTO>(userEnt));
            }else{      //Already exists
                return Conflict(new {
                    sttsCode = 409,
                    message = "Conflito ao salvar"
                });
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id, [FromBody]UserViewModel uUser)
        {
            var user = userRepo.GetById(id).Result;
            //Atribuido .Result uma vez que Update não é uma task
            //assíncrona, o que garante que só atribui após achar
            if(user != null){
                var userEnt = mapper.Map<User>(uUser);
                userEnt.Id = id;
                if(userRepo.Update(userEnt))
                {
                    var userMpd = mapper.Map<UserDTO>(userEnt);
                    if(userMpd.DonatedBooks == null)
                        userMpd.DonatedBooks = mapper.Map<List<BookDTO>>(user.DonatedBooks);
                    return Ok(userMpd);
                }else{
                    return BadRequest(new {
                        sttsCode = 400,
                        message = "Id não encontrada"
                    });
                    //Repetido pois deve haver um retorno
                }
            }else{
                return BadRequest(new {
                    sttsCode = 400,
                    message = "Id não encontrado"
                });
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if(userRepo.Delete(id))
            {
                return Ok(new {
                    sttsCode = 200,
                    message = "Usuário removido com sucesso"
                });
            }else{
                return BadRequest(new {
                    sttsCode = 400,
                    message = "Id não encontrada"
                });
            }
        }
        //----------------------------------------------
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var usersName = await userRepo.GetByName(name);
            var userByName = mapper.Map<List<UserDTO>>(usersName);
            foreach (var users in userByName)
            {
                if(users.DonatedBooks == null)
                {
                    var namedUser = usersName.FirstOrDefault(u=>u.Id==users.Id);
                    users.DonatedBooks = mapper.Map<List<BookDTO>>(namedUser.DonatedBooks);
                }
            }
            return Ok(userByName);
        }
    }
}