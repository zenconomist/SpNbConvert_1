using System.Text.RegularExpressions;

// testing the analyzer: I want to first only print out the blocks that are in the file:
// 1. I need to get the blocks from the file
// 2. I need to print out the blocks from the file
// using System.Text.RegularExpressions;

string inputFilePath = "TestSp2.sql";
// string outputFilePath = "TestSp2.ipynb";

// Tags
var tagMap = new Dictionary<string, Tag>
{
    {
        "SignedComment",
        new Tag("-- BlocCom:", new Regex(@"(?:.*)\s*--\s*BlocCom:"))
    },
    {
        // SignedComment without the beginning --
        "SignedComment2",
        new Tag("BlocCom:", new Regex(@"(?:.*)\s*BlocCom:"))
    },
    {
        "NewCellBegin",
        new Tag("-- CodeBlocBegin_", new Regex(@"\s*--\s*CodeBlocBegin_\d+"))
    },
    {
        "NewCellEnd",
        new Tag("-- CodeBlocEnd_", new Regex(@"\s*--\s*CodeBlocEnd_\d+"), @"^\s*--\s*CodeBlocEnd_")
    },
    {
        "DemoWhere",
        new Tag("-- DemoWhere:", new Regex(@"\s*--\s*DemoWhere:"))
    },
    {
        "NewBlockToComment",
        new Tag("-- BlocToComment_", new Regex(@"(?:.*)\s*--\s*BlocToComment_\d+"), @"\s*--\s*BlocToComment_")
    },
    {
        "NewBlockToUnComment",
        new Tag("-- BlocToUnComment_", new Regex(@"(?:.*)\s*--\s*BlocToUnComment_\d+"), @"\s*--\s*BlocToUnComment_")
    },
    {
        "RemoveDemoWhere",
        new Tag("-- RemoveDemoWhere:", new Regex(@"\s*--\s*RemoveDemoWhere:"))
    },
    {
        "RemoveLine", 
        new Tag("-- RemoveLine_Block_", new Regex(@"(?:.*)\s*--\s*RemoveLine_Block_\d+"), @"\s*--\s*RemoveLine_Block_") 
    }

};

var blockMod = new BlockModifier(tagMap);

// BlockModifier
var blockToComment = new BlockModifierDelegate(blockMod.Comment);
var blockToUnComment = new BlockModifierDelegate(blockMod.UnComment);
var removeDemoWhere = new BlockModifierDelegate(blockMod.RemoveDemoWhere);
var removeLine = new BlockModifierDelegate(blockMod.RemoveLine);



var blockTypes = new List<IBlockType>
{
    new MarkdownBlockType(tagMap["SignedComment"].Pattern, tagMap),
    // new MarkdownBlockType(tagMap["SignedComment2"].Pattern, tagMap),
    new CodeBlockType(tagMap["NewCellBegin"].Pattern, tagMap) 
        { ModifierFunctions = 
            { 
                blockToComment
                , blockToUnComment
                , removeDemoWhere 
                , removeLine
            } 
    }
    // add Tags

    ,
    // You can define more block types here
};



var analyzer = new NbAnalyzer(blockTypes);

var blocks = analyzer.NbAnalyze(inputFilePath);

foreach (var block in blocks)
{
    Console.WriteLine($"Block type: {block.Name}");
    Console.WriteLine($"Block lines: {block.Lines.Count}");
    Console.WriteLine($"Block opening line: {block.OpeningLine}");
    Console.WriteLine($"Block closing line: {block.ClosingLine}");
    
    // print out every line of the block
    foreach (var line in block.Lines)
    {
        Console.WriteLine(line);
    }

    Console.WriteLine();
}


// Instantiate and use the NotebookBuilder
var notebookBuilder = new NotebookBuilder();
notebookBuilder.BuildNotebook(inputFilePath, blocks);

