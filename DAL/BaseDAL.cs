using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SkypeCC.DAL
{
    abstract class BaseDAL : IDisposable
    {
        protected string DataBaseFile { get; }
        protected SQLiteConnection Connection { get; }

        protected BaseDAL(string dataBaseFile)
        {
            if (!File.Exists(DataBaseFile = dataBaseFile))
            {
                throw new Exception("Couln't find Skype database in \n " + DataBaseFile);
            }

            Connection = new SQLiteConnection("data source=" + DataBaseFile);

            try
            {
                Connection.Open();
            }
            catch
            {
                throw new Exception("Possible Skype is running. Can't continue...");
            }
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }

        public SQLiteCommand CreateCommand()
        {
            return Connection?.CreateCommand();
        }

        protected string GetString(IDataReader reader, string propertyName)
        {
            string result = null;
            if (reader != null && reader[propertyName] != DBNull.Value)
                result = reader[propertyName].ToString();
            return result;
        }

        protected int GetInt32(IDataReader reader, string propertyName)
        {
            int result = 0;
            if (reader != null && reader[propertyName] != DBNull.Value)
                result = Convert.ToInt32(reader[propertyName]);
            return result;
        }
    }
}
