using System;
using BCrypt.Net;

namespace DatabaseManagement
{
    public class PasswordHelper
    {
        public static string HashPassword(string simplePassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(simplePassword);
        }
        public static bool VerifyPassword(string simplePassword, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(simplePassword, hashPassword);
        }
    }
}