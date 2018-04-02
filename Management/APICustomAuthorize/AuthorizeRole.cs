using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Management.APICustomAuthorize
{
    public class AuthorizeRole : AuthorizeAttribute
    {
        public AuthorizeRole(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}