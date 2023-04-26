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
        if (Tags.ContainsKey("Comment"))
        {
            string pattern = $@"\s*--\s*{Tags["Comment"].TagString}_{blockNumber}";
            if (Regex.IsMatch(line, pattern))
            {
                line = "--" + line; // Comment the line
            }
        }
        return line;
    };

    public BlockModifierDelegate UnComment => (string line, int blockNumber) =>
    {
        if (Tags.ContainsKey("UnComment"))
        {
            string pattern = $@"\s*--\s*{Tags["UnComment"].TagString}_{blockNumber}";
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
}
