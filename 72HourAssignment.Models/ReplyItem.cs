using _72HourAssignment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourAssignment.Models
{
    public class CreateReply
    {
        [Key]
        public int ReplyId { get; set; }

        [ForeignKey(nameof(Comment))]
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
    }
}
