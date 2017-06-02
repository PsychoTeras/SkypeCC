using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using SkypeCC.Entities;

namespace SkypeCC.DAL
{
    class MessagesDAL : BaseDAL
    {
        public MessagesDAL(string dataBaseFile) 
            : base(dataBaseFile)
        {
        }

        public Messages GetMessages(User user)
        {
            Messages messages = new Messages();

            using (SQLiteCommand cmd = CreateCommand())
            {
                cmd.CommandText = string.Format(@"
    SELECT
         id
        ,author
        ,timestamp
        ,body_xml
    FROM Messages
    WHERE chatname = '{0}' OR dialog_partner = '{0}'
    ORDER BY timestamp
", user.Nick);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    FilUserMessagesFromReader(reader, user, messages);
                }
            }

            return messages;
        }

        private void FilUserMessagesFromReader(SQLiteDataReader reader, User user, Messages messages)
        {
            while (reader.Read())
            {
                int id = GetInt32(reader, "id");
                string author = GetString(reader, "author");
                int timestamp = GetInt32(reader, "timestamp");
                string bodyXml = GetString(reader, "body_xml");
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();

                messages.Add(new Message
                (
                    id,
                    author != user.Nick,
                    dtDateTime,
                    bodyXml
                ));
            }
        }

        public void Delete(IEnumerable<Message> messages)
        {
            if (!messages.Any()) return;

            StringBuilder sb = new StringBuilder();
            foreach (Message message in messages)
            {
                sb.AppendFormat("{0},", message.Id);
            }

            using (SQLiteCommand cmd = CreateCommand())
            {
                cmd.CommandText = string.Format(@"
    DELETE
    FROM Messages
    WHERE id IN ({0})
", sb.ToString(0, sb.Length - 1));
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAll(User user)
        {
            if (user == null) return;

            using (SQLiteCommand cmd = CreateCommand())
            {
                cmd.CommandText = string.Format(@"
    DELETE
    FROM Messages
    WHERE chatname = '{0}' OR dialog_partner = '{0}'
", user.Nick);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditMessage(int messageId, string newText)
        {
            using (SQLiteCommand cmd = CreateCommand())
            {
                cmd.CommandText = string.Format(@"
    UPDATE Messages
    SET body_xml = '{0}'
    WHERE id = {1}
", newText, messageId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
