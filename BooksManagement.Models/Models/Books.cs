using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BooksManagement.Models
{
    public class BooksObj
    {
        [Key]
        [Column("ID")]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? Year { get; set; }
        [StringLength(50)]
        public string Author { get; set; }
    }
}