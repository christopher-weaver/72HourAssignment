using _72HourAssignment.Data;
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

        public bool CreateReply(ReplyCreate model)
        {
            var entity =
                    new Reply()
                    {
                        OwnerId = _authorId,
                        Title = model.Title,
                        Content = model.Content,
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
                    .Where(e => e.OwnerId == _authorId)
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
                    .Replies
                    .Single(e => e.ReplyId == id && e.CommentId == _commentId);
            return
                new ReplyDetail
                {
                    ReplyId = entity.ReplyId,
                    Title = entity.Title,
                    Content = entity.Content,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
        }
    }
}
