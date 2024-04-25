using System.Text.RegularExpressions;
using Npgsql;

namespace surveyapi.SqlQueryService
{
    public class TableCreationService
    {
        private readonly string _connectionString;
        private readonly string _scriptsDirectory;

        public TableCreationService(string connectionString, string scriptsDirectory)
        {
            _connectionString = connectionString;
            _scriptsDirectory = scriptsDirectory;
        }

        public void CreateTablesFromScripts()
        {
            var scriptFiles = Directory.GetFiles(_scriptsDirectory, "*.sql");

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            foreach (var scriptFile in scriptFiles)
            {
                var script = File.ReadAllText(scriptFile);
                var tableName = ExtractTableName(script);

                if (!TableExists(connection, tableName))
                {
                    using var command = new NpgsqlCommand(script, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Table created from script: {scriptFile}");
                }
                else
                {
                    Console.WriteLine($"Table already exists: {tableName}");
                }
            }
        }
        private string ExtractTableName(string sqlScript)
        {
            var regex = new Regex(@"CREATE\s+TABLE\s+""?(\w+)""?\s*\(");
            var match = regex.Match(sqlScript);
            return match.Success ? match.Groups[1].Value : null;
        }

        private bool TableExists(NpgsqlConnection connection, string tableName)
        {
            string query = $"SELECT COUNT(*) FROM information_schema.tables WHERE table_name = '{tableName}'";
            using (var command = new NpgsqlCommand(query, connection))
            {
                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
