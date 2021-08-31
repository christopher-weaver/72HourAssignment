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
                                    ReplyId = e.Id,
                                    CommentId = e.CommentId,
                                    CreatedUtc = e.CreatedUtc,
                                }
                                );
                return query.ToArray();
            }
        }

        // since we can reply more than once to the same comment  we should return all the replies to a single comment
        public IEnumerable<ReplyDetails> GetReplyByCommentId(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entities =
                    ctx
                        .Replies 
                        .Where(e => e.Id == commentId && e.AuthorId == _authorId); // we want more than one right? 

                return entities.Select(e => new ReplyDetails
                {
                    ReplyId = e.Id,
                    Text = e.Text,
                    CommentId = e.CommentId,
                    CreatedUtc = e.CreatedUtc,
                }).ToList();
            }
        }
    }
}
