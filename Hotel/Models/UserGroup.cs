using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models
{
  public class UserGroup
    {
      [Key]
      public int UserGroupId { get; set; }
      public string UserGroupName { get; set; }
    }
}
