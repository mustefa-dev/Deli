using System.ComponentModel.DataAnnotations;

namespace Deli.DATA.DTOs;

public class RatingOrderForm
{
    [Required]
    public double Rating { get; set; }
}