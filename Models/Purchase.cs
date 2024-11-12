using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEV1_2024_Assignment.Models;
    public class Purchase
    {
        public int Id{get;set;}
        public int ProductId{get;set;}
        public Product Product{get;set;}
        public string AppUserId{get;set;}
        public AppUser AppUser{get;set;}
    }
