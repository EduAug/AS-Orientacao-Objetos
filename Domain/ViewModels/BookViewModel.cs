using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS_Orientacao_Objetos.Domain.ViewModels
{
    public class BookViewModel
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int PageTotal { get; set; }
        public DateTime PublishDate { get; set; }
        public bool isRented { get; set; }
        public int GenreId { get; set; }
        public int DonatorId { get; set; }
        // public GenreViewModel Genre { get; set; }
        // public UserViewModel DonatedBy { get; set; }
    }
}