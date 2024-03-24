using System.Linq;

namespace HackMe.Application.Helpers
{
    public static class InputHelper
    {
        private static readonly IReadOnlyCollection<string> BlackListedKeywords = new List<string>
        {
            "drop", "delete", "alter", "TRUNCATE", "MERGE", "sleep",
            "grant", "exec","DENY", "REVOKE",
            "sys.sysobjects", "sys.tables", "syscolumns", "sysobjects", 
            "INFORMATION_SCHEMA", "INFORMATION_SCHEMA.SCHEMATA", "INFORMATION_SCHEMA.TABLES", "INFORMATION_SCHEMA.COLUMNS",
            "BACKUP", "RESTORE", "EXECUTE IMMEDIATE", "CREATE", "OPENROWSET", "sp_executesql",
            "xp_", "xp_cmdshell", "xp_regwrite", "xp_regdeletekey", "xp_fileexist"
        };

        public static bool IsAllowedSqlInjection(string input)
        {
            return IsSqlInjection(input) && !ContainsHarmfulKeywords(input);
        }

        private static bool ContainsHarmfulKeywords(string input)
        {
            foreach (string keyword in BlackListedKeywords)
            {
                if (input.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsSqlInjection(string input)
        {
            string[] sqlInjectionKeywords = { "'", ";", "--", "/*", "*/", "xp_", "exec ", "select ", "union ", "insert ", "update ", "delete ", "drop ", "truncate " };

            foreach (string keyword in sqlInjectionKeywords)
            {
                if (input.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
