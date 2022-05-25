using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EndProjectServerBL.Models
{


    public partial class EndProjectDBContext : DbContext
    {
        public User Login(string email, string pswd)
        {
            try
            {
                User user = this.Users
                   .Where(u => u.Email == email && u.Password == pswd).FirstOrDefault();

                     return user;
                 }
                 catch(Exception e)
                 {
                      throw new Exception("Could not Login. error retreiving Data", e);
                 }
               
            }
        public List<Post> GetPostsByDate()
        {
            try
            {
                List<Post> posts = (List<Post>)this.Posts.Include(x => x.Comments).Include(x => x.Topic).Include(x => x.User).OrderByDescending(x => x.TimeCreated).ToList();
    
                
                return posts;
            }
            catch (Exception e)
            {
                throw new Exception("error retreiving Data", e);
            }
        }
        public List<LikesInPost> GetLikesInPost()
        {
            try
            {
                List<LikesInPost> likesInPosts = this.LikesInPosts.ToList();


                return likesInPosts;
            }
            catch (Exception e)
            {
                throw new Exception("error retreiving Data", e);
            }
        }
        public List<LikesInComment> GetLikesInComment()
        {
            try
            {
                List<LikesInComment> likesInComments = this.LikesInComments.ToList();


                return likesInComments;
            }
            catch (Exception e)
            {
                throw new Exception("error retreiving Data", e);
            }
        }
        public void UpdateLikePost(Post p)
        {
            try
            {
                this.Posts.Update(p);

               
                
                   
                
                this.SaveChanges();
            }
            catch(Exception e)
            {

            }
        }
        public void UpdateLikeComment(Comment c)
        {
            try
            {
                this.Comments.Update(c);





                this.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }
        public void AddLikeInPost(LikesInPost l)
        {
            this.LikesInPosts.Add(l);
            this.SaveChanges();
        }
            
        public void CreateUser(User user)
        {

            this.Users.Add(user);
            this.SaveChanges();
        }
        public void ChangePassword(string newPassword, User user)
        {
            this.Users.Where(x => x.Email == user.Email).FirstOrDefault().Password = newPassword;
            this.SaveChanges();
        }
       
        public void CreateGame(Topic t)
        {
            this.Topics.Add(t);
            this.SaveChanges();
        }
        public void AddComment(Comment c)
        {
            this.Comments.Add(c);
            this.SaveChanges();
        }
        public void AddReview(Review r)
        {
            this.Reviews.Add(r);
            this.SaveChanges();
        }
        public void CreatePost(Post p)
        {
            this.Posts.Add(p);
            this.SaveChanges();
        }
        public List<User> GetUsers()
        {
            try
            {
                List<User> users = (List<User>)this.Users.OrderBy(x => x.Id);

                return users;
            }
            catch (Exception e)
            {
                throw new Exception("error retreiving Data", e);
            }
        }
        public List<Topic> GetTopics()
        {
            try
            {
                List<Topic> topics = (List<Topic>)this.Topics.Include(x=>x.Reviews).OrderBy(x => x.Name).ToList();

                return topics;
            }
            catch (Exception e)
            {
                throw new Exception("error retreiving Data", e);
            }
        }
        public List<Review> GetReviews()
        {
            try
            {
                List<Review> reviews = (List<Review>)this.Reviews.Include(x => x.User).OrderBy(x => x.TimeCreated).ToList();

                return reviews;
            }
            catch (Exception e)
            {
                throw new Exception("error retreiving Data", e);
            }
        }
        public List<Comment> GetComments()
        {
            try
            {
                List<Comment> comments = (List<Comment>)this.Comments.Include(x=>x.User).OrderBy(x => x.TimeCreated).ToList();

                return comments;
            }
            catch (Exception e)
            {
                throw new Exception("error retreiving Data", e);
            }
        }
        public bool UpdateUser(User u)
        {
            try
            {
                this.Users.Update(u);
                this.SaveChanges();
                return true;
            }
            catch( Exception e)
            {
                return false;
            }
        }
    }
    
}
