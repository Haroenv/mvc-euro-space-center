using EuroSpaceCenter.Models;
using System;
using System.Web.UI;

namespace EuroSpaceCenter.Views.Account {
    public partial class Register : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void submit_Click(object sender, EventArgs e) {
            if (Page.IsValid) {
                var u = new user();
                u.name = name.Text;
                u.email = email.Value.ToString();
                u.password = password.Text;
                if (users.create(u)) {
                    submit.Text = "goed gememed";
                } else {
                    submit.Text = "slecht gememed";
                }
            }
        }
    }
}