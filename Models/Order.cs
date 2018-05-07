using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DDSWebstore.Models
{
    public class Order {
        public int ID {get; set;}
        public string Name {get;set;}
        public string StreetAddress {get; set;}
        public string City{get; set;}
        public string State{get; set;}
        public int Zipcode{get; set;}
        
        [DataType(DataType.Currency)]
         public float Price{get; set;}
    
        

    }
}