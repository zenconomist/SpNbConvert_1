using System.Text.RegularExpressions;
class Analyzer
{
    private List<IBlockType> _blockTypes;
    public int CurrentBlockNumber { get; private set; }
    public Analyzer(List<IBlockType> blockTypes)
    {
        _blockTypes = blockTypes;
    }

    public List<Block> Analyze(string inputFilePath)
    {
        var blocks = new List<Block>();
        var lines = File.ReadAllLines(inputFilePath);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            foreach (var blockType in _blockTypes)
            {
                Match match = blockType.Pattern.Match(line);

                if (match.Success)
                {
                    int? blockNumber = null;

                    // Add this check to prevent parsing an empty string
                    if (match.Groups.Count > 1 && !string.IsNullOrEmpty(match.Groups[1].Value))
                    {
                        blockNumber = int.Parse(match.Groups[1].Value);
                    }

                    int closingLine = FindClosingLine(lines, blockType, match, i);

                    var blockLines = blockType.ProcessBlock(lines, i, closingLine, blockNumber.GetValueOrDefault());

                    // ...
                }
            }
        }

        return blocks;
    }
    private int FindClosingLine(string[] lines, IBlockType blockType, Match openingPatternMatch, int openingLine)
    {
        Regex closingPattern = blockType.GetClosingPattern(openingPatternMatch);

        // Add this check to handle null closing patterns
        if (closingPattern == null)
        {
            return -1;
        }

        for (int i = openingLine + 1; i < lines.Length; i++)
        {
            if (closingPattern.IsMatch(lines[i]))
            {
                return i;
            }
        }

        return -1;
    }


}
