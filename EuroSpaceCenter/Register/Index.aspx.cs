﻿using EuroSpaceCenter.Models;
using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace EuroSpaceCenter.Register {
    public partial class Index : Page {
        protected void submit_Click(object sender, EventArgs e) {
            if (Page.IsValid) {
                var u = new user() {
                    name = name.Text,
                    email = email.Value.ToString(),
                    password = password.Text,
                    admin = false
                };
                try {
                    user.Create(u);

                    if (Request.QueryString["plan"] != null) {
                        int id = 0;
                        if (int.TryParse(Request.QueryString["plan"], out id)) {
                            parkplan.Invite(users_id: u.id, parkplan_id: id);
                        }
                    }

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