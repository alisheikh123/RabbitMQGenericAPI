using System.ComponentModel.DataAnnotations;

namespace Producor_Web_API.Model
{
    public enum TableEnum
    {
        users,
        products,
        orders
    }
    public enum ActionEnum
    {
        add,
        delete,
        update,
        getAll,
        read,
        getById,
        getId

    }
    public class RequestParameter
    {
        [Required]
        public string Table { get; set; }
        [Required]
        public string Action { get; set; }
        [Required]
        public string? Model { get; set; }

    }
}
