using System;

namespace DDSWebstore.Models
{
  public class Item
  {
    public int ID {get; set;}
    public string name {get; set;}
    public string description{get; set;}
    public string location{get; set;}
    public string status{get; set;}
    public bool available{get; set;}
    public int orderID{get; set;}
    public float price{get; set;}
  }
}
