using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DDSWebstore.Models
{
  public class Category
  {
    [Key]
    public int ID {get; set;}
    public string ItemID {get; set;}
    public string Tag {get; set;}
  }
}
