using EuroSpaceCenter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EuroSpaceCenter.Register {
    public partial class Index : System.Web.UI.Page {
        protected void submit_Click(object sender, EventArgs e) {
            if (Page.IsValid) {
                var u = new user() {
                    name = name.Text,
                    email = email.Value.ToString(),
                    password = password.Text,
                    admin = false
                };
                try {
                    users.create(u);
                    output.Text = "Check your email!";
                } catch (SqlException) {
                    output.Text = "This account has already been made";
                } catch {
                    output.Text = "Something didn't work, sorry 😞";
                }
            }
        }
    }
}