﻿using System;
using System.Collections.Generic;
using RSS_Reader.Model;

namespace RSS_Reader.Controller
{
    class RegistrationAndLoginManager
    {
        private readonly FileManager _profileFileManager = new FileManager();

        public List<string> GetUserNameList()
        {
            return _profileFileManager.GetUserNameList();
        }

        public User RegisterUser(string userName)
        {
            if (NameIsBusy(userName)) throw new ArgumentException();
            var user = GetUser(userName,Status.Registered,Status.Anonym.ToString());
            _profileFileManager.SaveUserProfile(user);
            return user;
        }

        public User LogIn(string userName)
        {
            return GetUser(userName, Status.Registered);
        }

        public User GetAnonymUser()
        {
            return GetUser(Status.Anonym.ToString(), Status.Anonym);
        }

        private User GetUser(string userName, Status status, string fileName = null)
        {
            return new User(userName, status, _profileFileManager.GetUserProfile(fileName ?? userName));
        }

        private bool NameIsBusy(string name)
        {
            return _profileFileManager.GetUserNameList().Exists(x => x.Equals(name));
        }
    }
}