using _72HourAssignment.Data;
using _72HourAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourAssignment.Services
{
   public class CommentService
    {
        private readonly Guid _userId;
        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity = new Comment()
            {
                AuthorId = _userId,
                Text = model.Text,
                PostId = model.PostId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CommentList> GetComments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Comments
                    .Where(e => e.AuthorId == _userId)
                    .Select(e => new CommentList
                    {
                        CommentId = e.Id,
                        Text = e.Text,
                        CreatedUtc = e.CreatedUtc
                    });
                return query.ToArray();
            }
        }

        // this should be get comments by post id, which means we need to search for the post and then return it's comments, not search for comments
        public IEnumerable<CommentList> GetCommentByPostId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                // this will break if there are no matching elements, use SingleOrDefault instead please and check for null
                var entity =
                    ctx
                    .Posts
                    .SingleOrDefault(e => e.ID == id && e.AuthorId == _userId);

                return entity?.Comments?.Select(c => new CommentList { CommentId = c.Id, Text = c.Text, CreatedUtc = c.CreatedUtc }).ToList() ?? Enumerable.Empty<CommentList>();
            }
        }

        public CommentProperties GetCommentByAuthorId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.Id == id && e.AuthorId == _userId);
                return new CommentProperties
                {
                    AuthorId = entity.AuthorId,
                    Text = entity.Text,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
            }
        }

        public bool UpdateComment(CommentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.Id == model.CommentId && e.AuthorId == _userId);

                entity.Text = model.Text;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteComment(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.Id == commentId && e.AuthorId == _userId);
                ctx.Comments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
