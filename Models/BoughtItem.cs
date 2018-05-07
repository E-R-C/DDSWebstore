using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DDSWebstore.Models
{
  public class BoughtItem
  {
    [Key]
    public int ID {get; set;}
    public string Name {get; set;}
    public string Description{get; set;}
    public string Location{get; set;}
    public int OrderID{get; set;}
    
    [DataType(DataType.Currency)]
    public float Price{get; set;}

  }
}
