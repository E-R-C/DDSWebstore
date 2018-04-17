using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DDSWebstore.Models
{
  public class Image
  {
    [Key]
    public int ID {get; set;}
    public int ItemID {get; set;}
    public string ImageURL {get; set;}

    public Item Item{get; set;}
  }
}
