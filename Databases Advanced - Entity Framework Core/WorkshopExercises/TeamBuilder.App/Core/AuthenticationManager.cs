using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core
{
    class AuthenticationManager
    {
        private static User CurrentUser;
        public static void Login(User user)
        {
            if (!IsAuthenticated())
            {
                CurrentUser = user;
            }
            else
            {
                throw new ArgumentException(Constants.ErrorMessages.LogoutFirst);
            }

        }

        public static void Logout()
        {
            Authorize();
            CurrentUser = null;
        }

        public static void Authorize()
        {
            if (CurrentUser == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public static bool IsAuthenticated()
        {
            if (CurrentUser != null)
            {
                return true;
            }
            return false;
        }

        public static User GetCurrentUser()
        {
            if (CurrentUser != null)
            {
                return CurrentUser;
            }
            throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
        }
    }
}
