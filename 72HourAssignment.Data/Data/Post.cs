using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourAssignment.Data
{
    public class Post
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [ForeignKey(nameof(Comments))]
        public List<int> CommentIds { get; set; }
        public virtual List<Comment> Comments { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

    }
}
