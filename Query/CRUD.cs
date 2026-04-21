namespace QuizCompetition.Query
{
    public static class CRUD
    {
        public static string Get(string tableName)
        {
            string query = "select * from  " + tableName;
            return query;
        }
        public static string Insert(string tableName, string key, string value)
        {
            string query = "INSERT into " + tableName + " ( " + key + " ) values ( " + value + " )";
            return query;
        }
        public static string Update(string tableName, string Key, string value)
        {
            string query = "UPDATE " + tableName + " SET " + Key + " WHERE " + value;
            return query;
        }
        public static string Select(string tableName, string key, string value)
        {
            string query = "Select * from " + tableName + " Where " + key + " = " + value;
            return query;
        }
        public static string Delete(string tableName, string key)
        {
            string query = $"DELETE FROM {tableName} WHERE {key}";
            return query;
        }
    }
}
