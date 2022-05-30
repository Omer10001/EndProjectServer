using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndProjectServer.DTO;
using EndProjectServerBL.Models;
using System.IO;

namespace EndProjectServer.Controller
{
    [Route("EndProjectAPI")]
    [ApiController]
    public class EndProjectController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        EndProjectDBContext context;
        public EndProjectController(EndProjectDBContext context)
        {
            this.context = context;
        }
        #endregion
        [Route("Login")]
        [HttpGet]
        public User Login([FromQuery] string Email, [FromQuery] string Password)
        {
            User user = context.Login(Email, Password);

            //Check user name and password
            if (user != null)
            {
                HttpContext.Session.SetObject("theUser", user);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return user;
            }
            else
            {

                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        [Route("SignUp")]
        [HttpPost]
        public void SignUp([FromBody] User user)
        {
            try
            {
                User checkEmail = context.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (checkEmail != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                    return;
                }
                context.CreateUser(user);
               
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return;
            }

        }
        [Route("Logout")]
        [HttpGet]
        public void Logout()
        {
            try
            {
                HttpContext.Session.SetObject("theUser", null);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return;
            }
        }
        [Route("ChangePassword")]
        [HttpPost]
        public void ChangePassword([FromBody] string newPassword)
        {
            try
            {
                User user = HttpContext.Session.GetObject<User>("theUser");
                context.ChangePassword(newPassword, user);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            }

        }
        [Route("MainPage")]
        [HttpGet]
        public List<PostDTO> GetPosts()
        {
            try
            {
                List<Post> posts = context.GetPostsByDate();
                List<LikesInPost> likesInPosts = context.GetLikesInPost();
                List<PostDTO> postDTOs = new List<PostDTO>();
                foreach(Post p in posts)
                {
                    LikesInPost likeInPost = likesInPosts.Where(x => x.PostId == p.Id && x.UserId == HttpContext.Session.GetObject<User>("theUser").Id).FirstOrDefault();
                    if(likeInPost ==null)
                    {
                        likeInPost = new LikesInPost { PostId = p.Id, UserId = HttpContext.Session.GetObject<User>("theUser").Id, IsDisliked =false, IsLiked = false };
                        p.LikesInPosts.Add(likeInPost);
                        context.UpdateLikePost(p);
                    }
                    postDTOs.Add(new PostDTO { Post = p, LikeInPost = likeInPost });
                }

                if (postDTOs != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return postDTOs;

                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    return null;
                }
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }
        
        [Route("AddGame")]
        [HttpPost]
        public Topic AddGame([FromBody] Topic game)
        {
            try
            {
                Topic t = context.CreateGame(game);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return t;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }
        }
        [Route("AddComment")]
        [HttpPost]
        public void AddComment([FromBody] Comment comment)
        {
            try
            {
                context.AddComment(comment);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return;
            }
        }
        [Route("AddReview")]
        [HttpPost]
        public void AddReview([FromBody] Review review)
        {
            try
            {
                context.AddReview(review);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return;
            }
        }
        [Route("GetUsers")]
        [HttpGet]
        public List<User> GetUsers()
        {
            try
            {
                if (HttpContext.Session.GetObject<User>("theUser").IsAdmin)
                {
                    List<User> users = context.GetUsers();
                    if (users != null)
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        return users;

                    }
                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                        return null;
                    }
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }

            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }
        [Route("GetTopics")]
        [HttpGet]
        public List<Topic> GetTopics()
        {
            try
            {
                List<Topic> topics = context.GetTopics();
                if (topics != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return topics;

                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    return null;
                }
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }
        [Route("GetReviews")]
        [HttpGet]
        public List<Review> GetReviews()
        {
            try
            {
                List<Review> reviews = context.GetReviews();
                if (reviews != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return reviews;

                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    return null;
                }
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }
        [Route("GetComments")]
        [HttpGet]
        public List<CommentDTO> GetComments()
        {
            try
            {
                List<Comment> comments = context.GetComments();
                List<LikesInComment> likesInComments = context.GetLikesInComment();
                List<CommentDTO> commentDTOs = new List<CommentDTO>();
                foreach (Comment c in comments)
                {
                    LikesInComment likeInComment = likesInComments.Where(x => x.CommentId == c.Id && x.UserId == HttpContext.Session.GetObject<User>("theUser").Id).FirstOrDefault();
                    if (likeInComment == null)//maybe useless
                    {
                        likeInComment = new LikesInComment { CommentId = c.Id, UserId = HttpContext.Session.GetObject<User>("theUser").Id, IsDisliked = false, IsLiked = false };
                        c.LikesInComments.Add(likeInComment);
                        context.UpdateLikeComment(c);
                    }
                    commentDTOs.Add(new CommentDTO { Comment = c, LikeInComment = likeInComment });
                }

                if (commentDTOs != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return commentDTOs;

                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    return null;
                }
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }
        [Route("CreatePost")]
        [HttpPost]
        public Post CreatePost([FromBody] Post p)
        {
            try
            {
                p = context.CreatePost(p);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return p;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }
        }
        [Route("LikePost")]
        [HttpPost]
        public bool LikePost([FromBody] PostDTO p)
        {
            try
            {
                
                context.UpdateLikePost(p.Post);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }
        }
        [Route("LikeComment")]
        [HttpPost]
        public bool LikeComment([FromBody] CommentDTO c)
        {
            try
            {

                context.UpdateLikeComment(c.Comment);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }
        }
        [Route("UpdateUser")]
        [HttpPost]
        public bool UpdateUser([FromBody] User u)
        {
            try
            {
                if (HttpContext.Session.GetObject<User>("theUser").IsAdmin != true)
                    return false;
                context.UpdateUser(u);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;

            }
            catch(Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }
        }
        [Route("DeletePost")]
        [HttpPost]
        public bool DeletePost([FromBody] Post p)
        {
            try
            {

                context.DeletePost(p);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }
        }
        [Route("UploadImage")]
        [HttpPost]

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
            {
                if (file == null)
                {
                    return BadRequest();
                }

                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    return Ok(new { length = file.Length, name = file.FileName });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }
            return Forbid();
        }
    }


}
