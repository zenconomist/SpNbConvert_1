using Newtonsoft.Json;
public class NotebookBuilder
{
    public void BuildNotebook(string inputFilePath, List<Block> builtBlocks)
    {
        // Create the IPYNB file structure
        dynamic notebook = new
        {
            cells = new List<dynamic>(),
            metadata = new
            {
                kernelspec = new
                {
                    display_name = "SQL",
                    language = "sql",
                    name = "mssql"
                },
                language_info = new
                {
                    codemirror_mode = "sql",
                    file_extension = ".sql",
                    mimetype = "text/x-sql",
                    name = "sql"
                }
            },
            nbformat = 4,
            nbformat_minor = 5
        };

        foreach (var block in builtBlocks)
        {
            dynamic cell = new
            {
                cell_type = block.Name.ToLower(),
                metadata = new { },
                source = block.Lines
            };

            notebook.cells.Add(cell);
        }

        // Serialize the notebook to JSON
        string json = JsonConvert.SerializeObject(notebook);

        // Write the JSON to a file with the same name as the input file, but with .ipynb extension
        string outputFilePath = Path.ChangeExtension(inputFilePath, ".ipynb");
        // Delete the output file if it already exists
        if (File.Exists(outputFilePath))
        {
            File.Delete(outputFilePath);
        }
        File.WriteAllText(outputFilePath, json);
    }
}
