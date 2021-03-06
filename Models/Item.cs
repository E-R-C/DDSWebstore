using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDSWebstore.Models
{
  public class Item
  {
    [Key]
    public int ID {get; set;}
    public string Name {get; set;}
    public string Description{get; set;}
    public string Location{get; set;}

    public string FID {get; set;}
    
    [DataType(DataType.Currency)]
    public decimal Price{get; set;}

    // comma-separated string of 1+ tags
    public string Tags {get; set;}

    

    public virtual IList<DDSWebstore.Models.Image> Images { get; set; }

  }
}
