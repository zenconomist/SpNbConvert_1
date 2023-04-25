using System.Text.RegularExpressions;

class BlockModifiers {

    public static string Comment(string line, int blockNumber)
    {
        string pattern = $@"\s*--\s*NewBlockToComment_{blockNumber}";
        if (Regex.IsMatch(line, pattern))
        {
            line = Regex.Replace(line, @"^(\s*)(\S.*)", "$1-- $2"); // Add comment while preserving the leading whitespaces
        }
        return line;
    }


    public static string UnComment(string line, int blockNumber)
    {
        string pattern = $@"\s*--\s*NewBlockToComment_{blockNumber}";
        if (Regex.IsMatch(line, pattern))
        {
            line = Regex.Replace(line, @"^(\s*)--\s*(\S.*)", "$1$2"); // Uncomment the line while preserving the leading whitespaces
        }
        return line;
    }


}
