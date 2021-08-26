using _72HourAssignment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _72HourAssignment.API.Controllers
{
    [Authorize]
    public class ReplyController : ApiController
    {
        // POST(Create) a Reply to a Comment using a Foreign Key relationship (required)
        private ReplyService CreateReplyService()
        {
        var userId = Guid.Parse(User.Identity.GetAuthorId());
            var replyService = new ReplyService(replyId);
            return replyService;
        }

        // GET Replies By Comment Id(required)

        public IHttpActionResult Get(int commentId)
        {
            ReplyService replyService = CreateReplyService();
            var reply = replyService.GetReplyByCommentId(commentId);
            return Ok(reply);
        }






        // GET Reply By Author Id


        // PUT(Update) a Reply


        // DELETE a Reply

    }
}
