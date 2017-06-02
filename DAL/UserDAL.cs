using System.Data.SQLite;
using SkypeCC.Entities;

namespace SkypeCC.DAL
{
    class UserDAL : BaseDAL
    {
        public UserDAL(string dataBaseFile) 
            : base(dataBaseFile)
        {
        }

        public Users GetUsers()
        {
            Users users = new Users();

            using (SQLiteCommand cmd = CreateCommand())
            {
                cmd.CommandText = @"
    SELECT
         id
        ,skypename
        ,fullname
    FROM Contacts
    ORDER BY fullname
";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    FillUsersFromReader(reader, users);
                }
            }

            return users;
        }

        private void FillUsersFromReader(SQLiteDataReader reader, Users users)
        {
            while (reader.Read())
            {
                string nick = GetString(reader, "skypename");
                if (!string.IsNullOrEmpty(nick))
                {
                    int id = GetInt32(reader, "id");
                    string name = GetString(reader, "fullname");
                    if (string.IsNullOrEmpty(name)) name = nick;
                    users.Add(new User(id, nick, name));
                }
            }
        }
    }
}
