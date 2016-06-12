using Simple.Data;
using System;
using System.Collections.Generic;
using WebApiUserManagement.Models;

namespace WebApiUserManagement.Repository
{
    public interface IRepository<T>
    {
        IList<T> GetAll(string sortColumn, bool sortAscending);
        T GetById(int id);
    }

    public interface IUserRepository : IRepository<User>
    {
    }

    public class UserRepository : IUserRepository
    {
        public IList<User> GetAll(string sortColumn, bool sortAscending = true)
        {
            var db = this.OpenConnection();
            var users = GetUsers(db, db.Users.All());

            if (sortAscending)
            {
                users = users.OrderBy(db["Users"][sortColumn]);
            }
            else
            {
                users = users.OrderByDescending(db["Users"][sortColumn]);
            }

            return users.ToList<User>();
        }

        public User GetById(int id)
        {
            var db = this.OpenConnection();

            return GetUsers(db, db.Users.FindAllByUserId(id)).FirstOrDefault();
        }

        private dynamic GetUsers(dynamic db, dynamic source)
        {
            return source
                .With(db.Users.Brands.As("Brand"))
                .With(db.Users.UserRoles.Roles);
        }
    }

    internal static class IRepositoryExtensions
    {
        public static dynamic OpenConnection<T>(this IRepository<T> repository)
        {
            return Database.OpenNamedConnection("WebApiUsers");
        }
    }
}