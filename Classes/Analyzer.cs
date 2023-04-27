using System.Text.RegularExpressions;
class NbAnalyzer
{
    private List<IBlockType> _blockTypes;
    public int CurrentBlockNumber { get; private set; }
    public NbAnalyzer(List<IBlockType> blockTypes)
    {
        _blockTypes = blockTypes;
    }

    public List<Block> NbAnalyze(string inputFilePath)
    {
        var blocks = new List<Block>();
        var lines = File.ReadAllLines(inputFilePath);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            foreach (var blockType in _blockTypes)
            {

                // Console.WriteLine($"Checking line {i} for block type: {blockType.Name}, line: {line}.");

                Match match = blockType.Pattern.Match(line);
                
                // Console.WriteLine($"Matched: {match.Success}, match value: {match.Value}."); // Add this line to see what the match value is

                if (match.Success)
                {
                    // Console.WriteLine($"Found block type: {blockType.Name} on line {i}.");

                    int? blockNumber = null;

                    // Add this check to prevent parsing an empty string
                    if (match.Groups.Count > 1 && !string.IsNullOrEmpty(match.Groups[1].Value))
                    {
                        blockNumber = int.Parse(match.Groups[1].Value);
                    }

                    int closingLine = FindClosingLine(lines, blockType, match, i);

                    var blockLines = blockType.ProcessBlock(lines, i, closingLine, blockNumber.GetValueOrDefault());

                    blocks.Add(new Block(blockType, blockLines, blockNumber));
                }
            }
        }

        return blocks;
    }
    public int FindClosingLine(string[] lines, IBlockType blockType, Match openingPatternMatch, int openingLine)
    {
        Regex closingPattern = blockType.GetClosingPattern(openingPatternMatch);

        for (int i = openingLine + 1; i < lines.Length; i++)
        {
            if (closingPattern != null)
            {
                Match match = closingPattern.Match(lines[i]);
                if (match.Success)
                {
                    return i;
                }
            }
            // else
            // {
            //     Match match = blockType.Pattern.Match(lines[i]);
            //     if (match.Success)
            //     {
            //         return i - 1;
            //     }
            // }
        }

        return -1;
    }



}
