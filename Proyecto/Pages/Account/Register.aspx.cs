using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Models;

public partial class Pages_Account_Register : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {

        var userStore = new UserStore<IdentityUser>();

        userStore.Context.Database.Connection.ConnectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["GarageConnectionString"].ConnectionString;
        var manager = new UserManager<IdentityUser>(userStore);

        var user = new IdentityUser { UserName = txtUserName.Text };

        if (txtPassword.Text == txtConfirmPassword.Text)
        {
            try
            {
                IdentityResult result = manager.Create(user, txtPassword.Text);
                if (result.Succeeded)
                {
                    UserDetail userDetail = new UserDetail
                    {
                        Address = txtAddress.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Guid = user.Id,
                        PostalCode = Convert.ToInt32(txtPostalCode.Text)
                    };

                    UserDetailModel model = new UserDetailModel();
                    model.InsertUserDetail(userDetail);

                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                    Response.Redirect("~/Index.aspx");
                }
                else
                {
                    litStatusMessage.Text = result.Errors.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                litStatusMessage.Text = ex.ToString();
            }
        }
        else
        {
            litStatusMessage.Text = "Las Contraseñas deben coincidir!";
        }
    }
}