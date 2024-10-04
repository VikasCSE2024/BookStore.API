using System.ComponentModel.DataAnnotations; 

namespace BookStore.API.Models

{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Title field sahi se bharo")]

        [NoSpecialCharactersOrNumbers]
 
        public string Title { get; set; }
        [NoSpecialCharactersOrNumbers]
        public string Description { get; set; }
    }
}
