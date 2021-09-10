using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;


namespace WebApplication1.Models
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (UserMasterRepository _repo = new UserMasterRepository())
            {
                System.Diagnostics.Debug.WriteLine("User Name:" + context.UserName.ToString());
                System.Diagnostics.Debug.WriteLine("Password:" + context.Password.ToString());
                var user = _repo.ValidateUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Provided email and password is incorrect");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                string[] roles = user.roles.Split(',');
                foreach(string role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                identity.AddClaim(new Claim(ClaimTypes.Email, user.email));

                context.Validated(identity);
            }
        }
    }
}