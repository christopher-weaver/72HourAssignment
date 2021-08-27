using _72HourAssignment.Data;
using _72HourAssignment.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourAssignment.Services.Services
{
    public class PostService
    {
        private readonly Guid _authorId;

        public PostService(Guid authorId)
        {
            _authorId = authorId;
        }

        public bool CreatePost(PostCreate model)
        {
            var newNote =
                new Post()
                {
                    AuthorId = _authorId,
                    Title = model.Title,
                    Text = model.Text
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(newNote);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PostDisplay> GetPosts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Posts
                               .Select(p => new PostDisplay
                               {
                                   Id = p.ID,
                                   AuthorId = p.AuthorId,
                                   Title = p.Title,
                                   Text = p.Text,
                                   Comments = p.Comments
                               });

                return query.ToArray();
            }
        }

        public IEnumerable<PostDisplay> GetPostsFromCurrentUser()
        {
            return GetPostsByAuthorId(_authorId);
        }

        public IEnumerable<PostDisplay> GetPostsByAuthorId(Guid authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Posts
                               .Where(p => p.AuthorId == authorId)
                               .Select(p => new PostDisplay
                                            {
                                                Id = p.ID,
                                                AuthorId = p.AuthorId,
                                                Title = p.Title,
                                                Text = p.Text,
                                                Comments = p.Comments
                                            });

                return query.ToArray();
            }
        }

        public bool UpdatePost(PostEdit editedPost)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var postToEdit =
                    ctx
                        .Posts
                        .Single(p => p.ID == editedPost.Id && p.AuthorId == _authorId);

                if (postToEdit == null)
                {
                    return false;
                }

                postToEdit.Title = editedPost.Title;
                postToEdit.Text = editedPost.Text;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePost(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var postToDelete =
                    ctx
                        .Posts
                        .Single(p => p.ID == postId && p.AuthorId == _authorId);

                if (postToDelete == null)
                {
                    return false;
                }

                ctx.Posts.Remove(postToDelete);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
