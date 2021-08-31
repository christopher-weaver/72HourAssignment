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
    public class ReplyCreate
    {
        [Key]
        public int ReplyId { get; set; } // don't need this on a create model - pks

        [Required]
        //[ForeignKey(nameof(Comment))] // no need for foreign key annotations here - pks
        public int CommentId { get; set; } // unless you can have a reply without a comment best to make this Required - pks
        //public virtual Comment Comment { get; set; } // don't use entities in your models - pks

        [Required]
        public string Text { get; set; }

        // don't need this because we can populate this from the built in ASP.NET identity logic - pks
        //[Required] - pks
        //public Guid AuthorId { get; set; } - pks
    }
}
