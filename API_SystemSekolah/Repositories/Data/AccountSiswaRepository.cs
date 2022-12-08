using System;
using API_SystemSekolah.Context;
using API_SystemSekolah.Models;
using API.Handlers;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace API_SystemSekolah.Repositories.Data
{
	public class AccountSiswaRepository
	{
        private MyContext context;
        public AccountSiswaRepository(MyContext context)
        {
            this.context = context;
        }

        //public int ChangePassword(string email, string password, string newPassword)
        //{
        //    var data = context.Siswas
        //        .SingleOrDefault(x => x.Email.Equals(email));
        //    if (data != null)
        //    {
        //        var result = Hashing.ValidatePassword(password, data.Password);
        //        if (result)
        //        {
        //            data.Password = Hashing.HashPassword(newPassword);
        //            context.Entry(data).State = EntityState.Modified;

        //            var resultPassword = context.SaveChanges();
        //            if (resultPassword > 0)
        //            {
        //                return 1;
        //            }
        //        }
        //        return 2;
        //    }
        //    return 3;
        //}

        public int ChangePassword(string email, string password, string newPassword)
        {
            var data = context.Siswas
                .SingleOrDefault(x => x.Email.Equals(email));
            if (data != null)
            {
                var result = Hashing.ValidatePassword(password, data.Password);
                if (result)
                {
                    data.Password = Hashing.HashPassword(newPassword);
                    context.Entry(data).State = EntityState.Modified;

                    var resultPassword = context.SaveChanges();
                    if (resultPassword > 0)
                    {
                        return 1;
                    }
                }
                return 2;
            }
            return 3;
        }

        public int ForgotPassword(string email, string newPassword)
        {
            var data = context.Siswas
                .SingleOrDefault(x => x.Email.Equals(email));
            if (data != null)
            {
                data.Password = Hashing.HashPassword(newPassword);
                context.Entry(data).State = EntityState.Modified;

                var result = context.SaveChanges();
                if (result > 0)
                {
                    return 1;
                }
            }
            return 2;
        }

        public Siswa Login(string email, string password)
        {
            var data = context.Siswas.FirstOrDefault(option =>
            option.Email.Equals(email));
            if (data != null && Hashing.ValidatePassword(password, data.Password))
            {
                return data;
            }
            return null;
        }
    }
}

