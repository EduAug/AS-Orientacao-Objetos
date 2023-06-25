using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AS_Orientacao_Objetos.Domain.ViewModels
{
    public class GenreViewModel
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage ="Campo {0} precisa conter entre {2} e {1} caracteres", MinimumLength = 4)]
        public string Name { get; set; }
    }
}