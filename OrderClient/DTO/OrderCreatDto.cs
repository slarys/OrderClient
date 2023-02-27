using System.ComponentModel.DataAnnotations;
using OrderClient.Models.Orders;

namespace OrderClient.DTO;

public class OrderCreatDto
{
   
    [Display(Name = "Order Date")]
    [Required(ErrorMessage = "Please enter an order date")]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Client ID")]
    [Required(ErrorMessage = "Please enter a client ID")]
    public uint ClientID { get; set; }

    [Display(Name = "Description")]
    [Required(ErrorMessage = "Please enter a description")]
    public string Description { get; set; }

    [Display(Name = "Order Price")]
    [Required(ErrorMessage = "Please enter an order price")]
    public float OrderPrice { get; set; }

    [Display(Name = "Close Date")]
    public DateTime? CloseDate { get; set; }
}