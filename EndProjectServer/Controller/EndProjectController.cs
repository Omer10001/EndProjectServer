using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            User user = context.Login(Email,Password);

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
    }

}
