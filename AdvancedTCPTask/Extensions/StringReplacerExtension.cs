namespace AdvancedTCPTask.Extensions
{
    public static class StringReplacerExtension
    {
        public static string RemoveLineFeeds(this string input)
        {
            var result = input.Replace("\n", "").Replace("\r", "");
            return result;
        }
    }
}
