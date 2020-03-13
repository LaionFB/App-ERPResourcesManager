using System;
using SQLite;

namespace Notes.Models
{
    public class AuthToken
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Token { get; set; }
    }
}