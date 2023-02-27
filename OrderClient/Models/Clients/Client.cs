using System.ComponentModel.DataAnnotations;

namespace OrderClient.Models.Clients
{
    public class Client
    {
        public uint ID { get; set; }

        [Required]
        [Range(0, uint.MaxValue, ErrorMessage = "OrderAmount must be a positive integer")]
        public uint OrderAmount { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNum { get; set; }
        public DateTime DateAdd { get; set; }
    }
}
