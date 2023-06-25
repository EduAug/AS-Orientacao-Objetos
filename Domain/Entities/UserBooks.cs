using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.Entities
{
    public class UserBooks
    {
        //UserBooks é uma tabela "retirada", que controla
        //Quem tirou qual(is) livro(s) e quando.
        public int UserId { get; set; }
        public int BookId { get; set; }
        //Por não existir "Id da transação" um usuário
        //não poderá tornar a alugar um livro que já tenha
        //alugado no passado.
        //Caso isso venha a acontecer, será editada a
        //relação e mudado o prazo limite e apagada a
        //data de retorno.
        public User User { get; set; }
        public Book Book { get; set; }

        public DateTime RentalDate { get; set; }
        public DateTime ReturnLimitDate { get; set; }
        public DateTime? ReturnedOn { get; set; }
    }
}