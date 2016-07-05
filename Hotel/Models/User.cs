using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
   public class User
    {
       [Key]
        public int UserId { get; set; }
        public int UserGroupId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
    }
}
