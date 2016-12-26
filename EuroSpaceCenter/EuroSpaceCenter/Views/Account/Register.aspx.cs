using EuroSpaceCenter.Models;
using System;
using System.Web.UI;

namespace EuroSpaceCenter.Views.Account {
    public partial class Register : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void submit_Click(object sender, EventArgs e) {
            if (Page.IsValid) {
                var u = new user() {
                    name = name.Text,
                    email = email.Value.ToString(),
                    password = password.Text
                };
                if (users.create(u)) {
                    output.Text = "Check your email!";
                } else {
                    output.Text = "Something didn't work, sorry 😞";
                }
            }
        }
    }
}