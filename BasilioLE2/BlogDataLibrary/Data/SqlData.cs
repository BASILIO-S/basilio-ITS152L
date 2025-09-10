using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BlogDataLibrary.Data
{
    public class SqlData : ISqlData
    {
        private ISqlDataAccess _db;
        private const string connectionStringName = "SqlDB";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        public UserModel Authenticate(string username, string password)
        {
            UserModel result = _db.LoadData<UserModel, dynamic>("dbo.spUsers_Authenticate",
                                                                new { username, password },
                                                                connectionStringName,
                                                                true).FirstOrDefault();
            return result;
        }

        public void Register(string userName, string firstName, string lastName, string password)
        {
            _db.SaveData(
                "spUsers_Register",
                new { userName, firstName, lastName, password },
                connectionStringName,
                true);
        }

        public void AddPost(PostModel post)
        {
            _db.SaveData("spPosts_Insert", new { post.UserId, post.Title, post.Body, post.DateCreated },
                connectionStringName,
                true);
        }

        public List<ListPostModel> ListPosts()
        {
            return _db.LoadData<ListPostModel, dynamic>("dbo.spPosts_List", new { },
                connectionStringName, true).ToList();
        }

        public ListPostModel ShowPostDetails(int id)
        {
            return _db.LoadData<ListPostModel, dynamic>("dbo.spPosts_Details", new { id },
                connectionStringName, true).FirstOrDefault();
        }

    }
}
