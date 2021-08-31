using _72HourAssignment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourAssignment.Models.Models
{
    public class PostDisplay
    {
        public int Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public virtual List<CommentList> Comments { get; set; } // don't pass entities out of your endpoints. 
    }
}
