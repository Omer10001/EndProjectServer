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
                List<Post> posts = (List<Post>)this.Posts.Include(x => x.Comments).Include(x => x.Topic).Include(x => x.TagsInPosts).Include(x => x.User).OrderBy(x => x.TimeCreated);

                return posts;
            }
            catch (Exception e)
            {
                throw new Exception("error retreiving Data", e);
            }
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
        public void CreateTag(Tag t)
        {
            this.Tags.Add(t);
            this.SaveChanges();
        }
    }
    
}
