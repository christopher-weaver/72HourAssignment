using _72HourAssignment.Data;
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

        public bool CreateReply(CreateReply reply)
        {
            var entity =
                    new Reply()
                    {
                        ReplyId = _replyid,
                        Text = reply.Text,
                        CreatedUtc = DateTimeOffset.Now,
                    };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Reply.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }

    public Inumerable<ReplyListItem> GetReplies()
    {
        using (var ctx = new ApplicationDbContext())
        {
            var query =
                ctx
                    .Reply
                    .Where(e => e.ReplyId == _authorId)
                    .Select(
                        e =>
                            new ReplyListItem
                            {
                                ReplyId = e.ReplyId,
                                Title = e.Title,
                                CreatedUtc - e.CreatedUtc,
                            }
                            );
            return query.ToArray();
        }
    }

    public GetReplyByCommentId(int commentId)
    {
        using (var ctx = new ApplicationDbContext())
        {
            var entity =
                ctx
                    .Reply
                    .Single(e => e.ReplyId == replyId && e.CommentId == _commentId);
            return
                new ReplyDetail
                {
                    ReplyId = entity.ReplyId,
                    Text = entity.Text,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
        }
    }
}
