using System.ComponentModel.DataAnnotations;

namespace Producor_Web_API.Model
{
    public enum TableEnum
    {
        Users =1,
        Products = 2,
        Orders=3
    }
    public enum ActionEnum
    {
        Add = 1,
        Delete = 2,
        Update = 3,
        GetAll = 4,
        Read =5 ,
        GetById = 6,
        GetId= 7

    }
    public class RequestParameter
    {
        [Required]
        public TableEnum Table { get; set; }
        [Required]
        public ActionEnum Action { get; set; }
        [Required]
        public string? Model { get; set; }

    }
}
