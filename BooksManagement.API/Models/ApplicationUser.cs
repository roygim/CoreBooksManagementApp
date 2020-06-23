using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BooksManagement.API.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Column(TypeName = "nvarchar(250)")]
        public string City { get; set; }
    }
}
