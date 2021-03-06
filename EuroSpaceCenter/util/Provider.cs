﻿using EuroSpaceCenter.Models;
using System;
using System.Web.Security;

namespace EuroSpaceCenter.util {
    public class Provider : RoleProvider {

        public override bool IsUserInRole(string username, string roleName) {
            user u = user.Get(username);
            if (roleName == "admin") {
                return u.admin;
            }
            return false;
        }

        public override string[] GetRolesForUser(string username) {
            user u = user.Get(username);
            if (u.admin) {
                return new string[] { "admin" };
            }
            return new string[0];
        }

        public override string ApplicationName {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName) {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles() {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName) {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName) {
            throw new NotImplementedException();
        }
    }
}