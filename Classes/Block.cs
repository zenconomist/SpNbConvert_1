
public class Block
{
        public IBlockType BlockType { get; set; }
        public int OpeningLine { get; set; }
        public int ClosingLine { get; set; }
        public List<string> Lines { get; set; }

        public string Name { get; set; }
}
