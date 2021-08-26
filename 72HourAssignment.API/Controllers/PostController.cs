using _72HourAssignment.Models.Models;
using _72HourAssignment.Services.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _72HourAssignment.API.Controllers
{
    [Authorize]
    public class PostController : ApiController
    {
        // POST(Create) a Post(required)
        [HttpPost]
        public IHttpActionResult Post(PostCreate post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postService = CreatePostService();

            if (!postService.CreatePost(post))
            {
                return InternalServerError();
            }

            return Ok($"The following post has been created: {post.Title}");
        }

        // GET All Posts(required)
        [HttpGet]
        public IHttpActionResult Get()
        {
            PostService postService = CreatePostService();
            var posts = postService.GetPosts();
            return Ok(posts);
        }

        // GET Posts By Author Id
        // + Any Author Id
        [Route("api/Post/{authorId}")]
        public IHttpActionResult Get([FromUri]string authorId)
        {
            if (!Guid.TryParse(authorId, out Guid authorGuid))
            {
                return BadRequest("Incorrect author ID provided.");
            }

            PostService postService = CreatePostService();
            var posts = postService.GetPostsByAuthorId(authorGuid);
            return Ok(posts);
        }
        // + Current user
        [Route("api/Post/GetAllFromCurrentUser")]
        public IHttpActionResult GetAllFromCurrentUser()
        {
            PostService postService = CreatePostService();
            var posts = postService.GetPostsFromCurrentUser();
            return Ok(posts);
        }

        // PUT(Update) a Post
        [HttpPut]
        public IHttpActionResult Put(PostEdit post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
      
            var service = CreatePostService();

            if (!service.UpdatePost(post))
            {
                return InternalServerError();
            }
            
            return Ok();
        }

        // DELETE a Post
        [HttpDelete]
        [Route("api/Post/{postId}")]
        public IHttpActionResult Delete([FromUri] int postId)
        {
            var service = CreatePostService();

            if (!service.DeletePost(postId))
            {
                return InternalServerError();
            }
            
            return Ok();
        }


        // Helper Methods
        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var postService = new PostService(userId);
            return postService;
        }
    }
}
