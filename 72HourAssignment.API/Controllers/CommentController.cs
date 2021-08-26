using _72HourAssignment.Models;
using _72HourAssignment.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;

namespace _72HourAssignment.API.Controllers
{
    public class CommentController : ApiController
    {
        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var commentService = new CommentService(userId);
            return commentService;
        }

        public IHttpActionResult Get()
        {
            CommentService CommentService = CreateCommentService();
            var comments = CommentService.GetComments();
            return Ok(comments);
        }
        // POST(Create) a Comment on a Post using a Foreign Key relationship (required)
        public IHttpActionResult Post(CommentCreate comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCommentService();

            if (!service.CreateComment(comment))
                return InternalServerError();

            return Ok();
        }

        // GET Comments By Post Id(required)
        public IHttpActionResult Get(int id)
        {
            CommentService commentService = CreateCommentService();
            var comment = commentService.GetCommentByPostId(id);
            return Ok(comment);
        }

        // GET Comments By Author Id
        public IHttpActionResult GetAuthorId(int authorId)
        {
            CommentService commentService = CreateCommentService();
            var comment = commentService.GetCommentByAuthorId(authorId);
            return Ok(comment);
        }

        // PUT(Update) a Comment
        public IHttpActionResult Put(CommentEdit comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCommentService();

            if (!service.UpdateComment(comment))
                return InternalServerError();

            return Ok();
        }

        // DELETE a Comment
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCommentService();
            if (!service.DeleteComment(id))
                return InternalServerError();

            return Ok();
        }
    }
}
