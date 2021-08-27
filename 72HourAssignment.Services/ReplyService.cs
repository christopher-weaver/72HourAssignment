using _72HourAssignment.Data;
using _72HourAssignment.Data.Data;
using _72HourAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourAssignment.Services
{
    public class ReplyService
    {
        private readonly Guid _authorId;

        public ReplyService(Guid authorId)
        {
            _authorId = authorId;
        }

        public bool CreateReply(ReplyCreate reply)
        {
            var entity =
                    new Reply()
                    {
                        AuthorId = _authorId,
                        CommentId = reply.CommentId,
                        Text = reply.Text,
                        CreatedUtc = DateTimeOffset.Now,
                    };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Replies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        
        public IEnumerable<ReplyListItem> GetReplies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Replies
                        .Where(e => e.AuthorId == _authorId)
                        .Select(
                            e =>
                                new ReplyListItem
                                {
                                    ReplyId = e.ReplyId,
                                    CommentId = e.CommentId,
                                    CreatedUtc = e.CreatedUtc,
                                }
                                );
                return query.ToArray();
            }
        }

        public ReplyDetails GetReplyByCommentId(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Replies
                        .Single(e => e.ReplyId == commentId && e.AuthorId == _authorId);
                return new ReplyDetails
                {
                    ReplyId = entity.ReplyId,
                    Text = entity.Text,
                    CommentId = entity.CommentId,
                    CreatedUtc = entity.CreatedUtc,
                   };
            }
        }
    }
}
