using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HAHDotNetCore.PizzaApi.Models;

[Table("Tbl_Pizza")]
public class PizzaModel
{
    [Key]
    public int PizzaId { get; set; }
    public string PizzaName { get; set; }
    public decimal Price { get; set; }
}

