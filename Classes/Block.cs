
public class Block
{
        public IBlockType BlockType { get; set; }
        public int OpeningLine { get; set; }
        public int ClosingLine { get; set; }
        public List<string> Lines { get; set; }

        public string Name { get; set; } = "";

        public int? BlockNumber { get; set; }

        // constructor with blockType, blockLines, and blockNumber
        public Block(IBlockType blockType, List<string> lines, int? blockNumber)
        {
            BlockType = blockType;
            Lines = lines;
            BlockNumber = blockNumber;
            Name = BlockType.Name;
        }

        // constructor for simple blocks
        public Block(IBlockType blockType, int openingLine, int closingLine, List<string> lines)
        {
            BlockType = blockType;
            OpeningLine = openingLine;
            ClosingLine = closingLine;
            Lines = lines;
        }

        // constructor for blocks with a name
        public Block(IBlockType blockType, int openingLine, int closingLine, List<string> lines, string name)
        {
            BlockType = blockType;
            OpeningLine = openingLine;
            ClosingLine = closingLine;
            Lines = lines;
            Name = name;
        }

        // constructor for blocks with a number
        public Block(IBlockType blockType, int openingLine, int closingLine, List<string> lines, int blockNumber)
        {
            BlockType = blockType;
            OpeningLine = openingLine;
            ClosingLine = closingLine;
            Lines = lines;
            BlockNumber = blockNumber;
        }

        // constructor for blocks with a name and a number
        public Block(IBlockType blockType, int openingLine, int closingLine, List<string> lines, string name, int blockNumber)
        {
            BlockType = blockType;
            OpeningLine = openingLine;
            ClosingLine = closingLine;
            Lines = lines;
            Name = name;
            BlockNumber = blockNumber;
        }

}
