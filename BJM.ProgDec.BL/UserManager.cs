using BJM.ProgDec.BL.Models;
using BJM.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BJM.ProgDec.BL
{
    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base("Could not login with those credentials. IP Address saved.")
        {

        }
        public LoginFailureException(string message) : base(message)
        {

        }
    }
    public static class UserManager
    {
        public static string GetHash(string password)
        {
            using(var hasher = SHA1.Create())
            {
                var hashbytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }
        public static int DeleteAll() 
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    dc.tblUsers.RemoveRange(dc.tblUsers.ToList());
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Insert(User user, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblUser entity = new tblUser();
                    entity.Id = dc.tblUsers.Any() ? dc.tblUsers.Max(s => s.Id) + 1 : 1;
                    entity.FirstName = user.FirstName;
                    entity.LastName = user.LastName;
                    entity.UserId = user.UserId;
                    entity.Password = GetHash(user.Password);

                    user.Id = entity.Id;

                    dc.tblUsers.Add(entity);
                    results = dc.SaveChanges();
                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool Login(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.UserId))
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using(ProgDecEntities dc = new ProgDecEntities())
                        {
                            tblUser tbluser = dc.tblUsers.FirstOrDefault(u => u.UserId == user.UserId);
                            if (tbluser != null)
                            {
                                if(tbluser.Password == GetHash(user.Password))
                                {
                                    user.Id = tbluser.Id;
                                    user.FirstName = tbluser.FirstName;
                                    user.LastName = tbluser.LastName;
                                    return true;
                                }
                                else
                                {
                                    throw new LoginFailureException();
                                }
                            }
                            else
                            {
                                throw new LoginFailureException("UserId was not found.");
                            }
                        }
                    }
                    else
                    {
                        throw new LoginFailureException("Password was not set");
                    }
                }
                else
                {
                    throw new LoginFailureException("UserId was not set");
                }
            }
            catch (LoginFailureException)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void Seed() 
        {
            using (ProgDecEntities dc = new ProgDecEntities())
            {
                if (!dc.tblUsers.Any())
                {
                    User user = new User()
                    {
                        UserId = "bment",
                        FirstName = "Ben",
                        LastName = "Mentink",
                        Password = "123Dev"
                    };
                    Insert(user);
                    user = new User()
                    {
                        UserId = "bfoote",
                        FirstName = "Brian",
                        LastName = "Foote",
                        Password = "maple"
                    };
                    Insert(user);
                }
            }
        }
    }
}
