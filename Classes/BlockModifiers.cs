using System.Text.RegularExpressions;

class BlockModifiers {

    public static string Comment(string line, int blockNumber)
    {
        Match match = Regex.Match(line, @"--\s*NewBlockToComment_(\d+)");
        if (match.Success)
        {
            int commentBlockNumber = int.Parse(match.Groups[1].Value);

            // Uncomment the line only if it belongs to the corresponding block
            if (commentBlockNumber == blockNumber)
            {
                // Uncomment the line by removing the comment characters "--"
                return Regex.Replace(line.Substring(0, match.Index), @"^\s*--\s*|\s*--\s*$", "");
            }
        }

        return line;
    }

    public static string UnComment(string line, int blockNumber)
    {
        string pattern = $@"\s*--\s*NewBlockToUnComment_{blockNumber}";
        if (Regex.IsMatch(line, pattern))
        {
            line = Regex.Replace(line, @"^(\s*)--\s*", "$1"); // Uncomment the line
        }
        return line;
    }

}
