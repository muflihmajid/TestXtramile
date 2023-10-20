using System;
using System.Collections.Generic;
using SceletonAPI.Domain.Infrastructures;

namespace SceletonAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Name { set; get; }
        public string Company { set; get; }
        public string Key {set;get;}
        public bool Verivy {set;get;}
        public DateTime ExpiredDate {set; get;}
    }
}
