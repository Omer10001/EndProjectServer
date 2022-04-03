﻿using Microsoft.AspNetCore.Http;
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
                //HttpContext.Session.SetObject("theUser", user);
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
            {//need to verify if the user is the same using DTO
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
        [Route("AddTag")]
        [HttpPost]
        public void AddTag([FromBody] Tag tag)
        {
            try
            {
                context.CreateTag(tag);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return;
            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return;
            }
        }
        [Route("AddGame")]
        [HttpPost]
        public void AddGame([FromBody] Topic game)
        {
            try
            {
                context.CreateGame(game);
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
        [Route("CreatePost")]
        [HttpPost]
        public void AddGame([FromBody] Post p)
        {
            try
            {
                context.CreatePost(p);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return;
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return;
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
    }


}
