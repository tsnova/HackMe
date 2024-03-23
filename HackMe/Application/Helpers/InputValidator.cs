namespace HackMe.Application.Helpers
{
    public static class InputValidator
    {
        private static readonly IReadOnlyCollection<string> BlackListedKeywords = new List<string>
        {
            "drop", 
            "delete",
            "sys.sysobjects",
            "sys.tables",
            "INFORMATION_SCHEMA",
            "INFORMATION_SCHEMA.SCHEMATA",
            "INFORMATION_SCHEMA.TABLES",
            "INFORMATION_SCHEMA.COLUMNS"
        };

        public static bool IsValid(string input)
        {
            return !BlackListedKeywords.Contains(input);
        }
    }
}
