using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourAssignment.Models
{
    public class CommentCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Enter a minimum of two characters to comment.")]
        [MaxLength(350, ErrorMessage = "Chill out with the typing, no one is going to read all of this....")]
        public string Text { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
