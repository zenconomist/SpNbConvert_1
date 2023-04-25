using System.Text.RegularExpressions;
public class Tag
{
    public string TagString { get; set; }
    public Regex Pattern { get; set; }

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

    // TagCleanUp for one line string
    public string TagCleanUp(string line)
    {
        // q: what does Pattern.Replace do?
        // a: it's a method from the Regex class, it replaces all matches of the pattern with the second argument
        return Pattern.Replace(line, "");
    }

    public List<string> TagCleanUp(List<string> lines)
    {
        return lines.Select(line => Pattern.Replace(line, "")).ToList();
    }
}
