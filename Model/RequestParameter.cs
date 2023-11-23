using System.ComponentModel.DataAnnotations;

namespace Producor_Web_API.Model
{
    public enum TableEnum
    {
        Users,
        Products,
        Orders
    }
    public enum ActionEnum
    {
        Add,
        Delete,
        Update,
        GetAll,
        Read,
        GetById,
        GetId

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
