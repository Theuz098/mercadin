using Microsoft.Data.Sqlite;


namespace MERCADIN
{
    public static class DatabaseSetup
    {
        public static string ConnectionString = "Data Source=database.db;";

        public static void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                string sql = @"
                    CREATE TABLE IF NOT EXISTS Products (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Price REAL NOT NULL,
                        Stock INTEGER NOT NULL
                    );";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
