using System.Text.RegularExpressions;
public class Tag
{
    public string TagString { get; set; }
    public Regex Pattern { get; set; }

    public string PatternWithBlockNumber { get; set; }

    public bool ToRemove { get; set; } = true;

    public string Type { get; set; } = "markdown";

    public Tag(string tagString, Regex pattern)
    {
        TagString = tagString;
        Pattern = pattern;
    }

    public Tag(string tagString, string pattern)
    {
        TagString = tagString;
        Pattern = new Regex(pattern, RegexOptions.Compiled);
    }

    public Tag(string tagString, string pattern, bool toRemove)
    {
        TagString = tagString;
        Pattern = new Regex(pattern, RegexOptions.Compiled);
        ToRemove = toRemove;
    }

    public Tag(string tagString, Regex pattern, bool toRemove)
    {
        TagString = tagString;
        Pattern = pattern;
        ToRemove = toRemove;
    }

    public Tag(string tagString, string pattern, string patternWithBlockNumber)
    {
        TagString = tagString;
        Pattern = new Regex(pattern, RegexOptions.Compiled);
        PatternWithBlockNumber = patternWithBlockNumber;
    }
    

    public Tag(string tagString, Regex pattern, string patternWithBlockNumber)
    {
        TagString = tagString;
        Pattern = pattern;
        PatternWithBlockNumber = patternWithBlockNumber;
    }

    public Tag(string tagString, string pattern, string patternWithBlockNumber, bool toRemove)
    {
        TagString = tagString;
        Pattern = new Regex(pattern, RegexOptions.Compiled);
        PatternWithBlockNumber = patternWithBlockNumber;
        ToRemove = toRemove;
    }

    public Tag(string tagString, Regex pattern, string patternWithBlockNumber, bool toRemove)
    {
        TagString = tagString;
        Pattern = pattern;
        PatternWithBlockNumber = patternWithBlockNumber;
        ToRemove = toRemove;
    }

    public Tag(string tagString, string pattern, string patternWithBlockNumber, bool toRemove, string type)
    {
        TagString = tagString;
        Pattern = new Regex(pattern, RegexOptions.Compiled);
        PatternWithBlockNumber = patternWithBlockNumber;
        ToRemove = toRemove;
        Type = type;
    }

    public Tag(string tagString, Regex pattern, string patternWithBlockNumber, bool toRemove, string type)
    {
        TagString = tagString;
        Pattern = pattern;
        PatternWithBlockNumber = patternWithBlockNumber;
        ToRemove = toRemove;
        Type = type;
    }

    // TagCleanUp for one line string
    public string TagCleanUp(string line)
    {
        // q: what does Pattern.Replace do?
        // a: it's a method from the Regex class, it replaces all matches of the pattern with the second argument
        if (ToRemove)
        {
            return Regex.Replace(line, Pattern.ToString(), "");
        }
        return line;
    }

    public List<string> TagCleanUp(List<string> lines)
    {
        return lines.Select(line =>
        {
            if (ToRemove)
            {
                return Regex.Replace(line, Pattern.ToString(), "");
            }
            return line;
        }).ToList();
    }

}
