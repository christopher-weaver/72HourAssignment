using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourAssignment.Models.Models
{
    public class PostCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "Post titles must be at least 1 character long.")]
        [MaxLength(250, ErrorMessage = "Post titles can be no more than 250 characters long.")]
        public string Title { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Post content must be at least 1 character long.")]
        [MaxLength(50000, ErrorMessage = "Post content cannot exceed 50,000 characters.")]
        public string Text { get; set; }
    }
}
