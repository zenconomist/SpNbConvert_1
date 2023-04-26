using System.Text.RegularExpressions;
public delegate string BlockModifierDelegate(string line, int blockNumber);
class BlockModifier
{
    public Dictionary<string, Tag> Tags { get; set; }

    public BlockModifier(Dictionary<string, Tag> tags)
    {
        Tags = tags;
    }

    public BlockModifierDelegate Comment => (string line, int blockNumber) =>
    {
        if (Tags.ContainsKey("NewBlockToComment"))
        {
            string pattern = $@"\s*--\s*{Tags["NewBlockToComment"].PatternWithBlockNumber}{blockNumber}";
            if (Regex.IsMatch(line, pattern))
            {
                line = Regex.Replace(line, @"^(\s*)(\S.*)", "$1-- $2"); // Add comment while preserving the leading whitespaces
            }
        }
        return line;
    };

    public BlockModifierDelegate UnComment => (string line, int blockNumber) =>
    {
        if (Tags.ContainsKey("NewBlockToUnComment"))
        {
            string pattern = $@"\s*--\s*{Tags["NewBlockToUnComment"].PatternWithBlockNumber}{blockNumber}";
            if (Regex.IsMatch(line, pattern))
            {
                line = Regex.Replace(line, @"^(\s*)--\s*(\S.*)", "$1$2"); // Uncomment the line while preserving the leading whitespaces
            }
        }
        return line;
    };

    public BlockModifierDelegate RemoveDemoWhere => (string line, int blockNumber) =>
    {
        if (Tags.ContainsKey("DemoWhere"))
        {
            string pattern = Tags["DemoWhere"].Pattern.ToString();
            if (Regex.IsMatch(line, pattern))
            {
                line = Regex.Replace(line, pattern, string.Empty);
            }
        }
        return line;
    };

    public BlockModifierDelegate RemoveLine => (string line, int blockNumber) =>
    {
        if (Tags.ContainsKey("RemoveLine"))
        {
            string pattern = $@"\s*{Tags["RemoveLine"].PatternWithBlockNumber}{blockNumber}";
            if (Regex.IsMatch(line, pattern))
            {
                line = string.Empty; // Set the line to an empty string
            }
        }
        return line;
    };


}
