using AutoMapper;
using AS_Orientacao_Objetos.Domain.DTOs;
using AS_Orientacao_Objetos.Domain.ViewModels;
using AS_Orientacao_Objetos.Domain.Entities;
using AS_Orientacao_Objetos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AS_Orientacao_Objetos.Controllers
{
    [Route("/api/rental/")]
    public class UserBooksController : ControllerBase
    {
        private readonly IUserBooksRepository<UserBooks> usboRepo;
        private readonly IMapper mapper;
        
        public UserBooksController(IUserBooksRepository<UserBooks> _usboRepo, IMapper _mapper)
        {
            usboRepo = _usboRepo;
            mapper = _mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var UBs = mapper.Map<List<UserBooksDTO>>(await usboRepo.GetAll());
            return Ok(UBs);
        }

        [HttpGet("{idBook:int}/{idUser:int}")]
        public async Task<IActionResult> GetById(int idBook, int idUser)
        {
            var UBRelation = mapper.Map<UserBooksDTO>(await usboRepo.GetById(idBook, idUser));
            return Ok(UBRelation);
        }

        [HttpPost]
        public IActionResult Save([FromBody]UserBooksViewModel nRental)
        {
            var usBookEnt = mapper.Map<UserBooks>(nRental);
            if(usboRepo.Save(usBookEnt))
            {
                return Ok(mapper.Map<UserBooksDTO>(usBookEnt));
            }else{
                return Conflict(new {
                    sttsCode = 409,
                    message = "Conflito ao salvar"
                });
            }
        }

        [HttpPut("{idBook:int}/{idUser:int}")]
        public IActionResult Update([FromRoute]int idBook, [FromRoute]int idUser, [FromBody]UserBooksViewModel uUB)
        {
            var UBEnt = mapper.Map<UserBooks>(uUB);
            if(usboRepo.Update(UBEnt))
            {
                var UBMod = mapper.Map<UserBooksDTO>(UBEnt);
                var bkpUB = usboRepo.GetById(idBook,idUser).Result;
                    
                UBMod.UserRented = bkpUB.User.Name;
                UBMod.BookRented = bkpUB.Book.Title;
                return Ok(UBMod);
            }else{
                return BadRequest(new {
                    sttsCode = 400,
                    message = "Id não encontrada"
                });
            }
        }

        [HttpDelete("{idBook:int}/{idUser:int}")]
        public IActionResult Delete(int idBook, int idUser)
        {
            if(usboRepo.Delete(idBook, idUser))
            {
                return Ok(new {
                    sttsCode = 200,
                    message = "Relação de empréstimo removida com sucesso"
                });
            }else{
                return BadRequest(new {
                    sttsCode = 400,
                    message = "Esse usuário não locou esse livro"
                });
            }
        }
        //-------------------------------
        [HttpGet("usersRenting")]
        public async Task<IActionResult> GetUsersRenting()
        {
            var UsersAndBooksRelated = await usboRepo.GetUsersRentingBooks();
            var UsersRentingBooks = mapper.Map<List<UserBooksDTO>>(UsersAndBooksRelated);
            foreach (var usRent in UsersRentingBooks)
            {
                var thisUBrelation = usboRepo.GetById(usRent.BookId,usRent.UserId).Result;
                usRent.BookRented = thisUBrelation.Book.Title;
                usRent.UserRented = thisUBrelation.User.Name;
            }
            return Ok(UsersRentingBooks);
        }
        [HttpPut("returnBook/{idBook:int}/{idUser:int}")]
        public IActionResult ReturnBook(int idBook, int idUser)
        {
            var checkForReturn = GetById(idBook,idUser).Result;

            if(checkForReturn!=null){
                usboRepo.ReturnBook(idBook,idUser);
                return Ok(new {
                    sttsCode = 200,
                    message = "Livro devolvido com sucesso"
                });
            }else{
                return BadRequest(new {
                    sttsCode = 400,
                    message = "Esse usuário não alugou esse livro, ou já o devolveu"
                });
            }
        }
    }
}