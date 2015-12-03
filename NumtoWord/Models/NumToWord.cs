using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumtoWord.Models
{
    public class NumToWord
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15, ErrorMessage ="max length: 15")]
        public string number { get; set; }
        public string asword { get; set; }
    }
}
