using VEXOR.Obfuscation;

string? inputFile = null;
string outputFile = "output.cs";
bool renameSymbols = false;
bool controlFlow = false;
bool encryptStrings = false;
bool antiDebug = false;

// Parse command-line arguments
for (int i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "-s":
            if (i + 1 < args.Length)
            {
                inputFile = args[++i];
            }
            else
            {
                Console.WriteLine("Error: Missing argument for -s");
                return;
            }
            break;

        case "-o":
            if (i + 1 < args.Length)
            {
                outputFile = args[++i];
            }
            else
            {
                Console.WriteLine("Error: Missing argument for -o");
                return;
            }
            break;

        case "-r":
            renameSymbols = true;
            break;

        case "-c":
            controlFlow = true;
            break;

        case "-e":
            encryptStrings = true;
            break;

        case "-a":
            antiDebug = true;
            break;

        default:
            Console.WriteLine($"Unknown option: {args[i]}");
            return;
    }
}

if (inputFile == null)
{
    Console.WriteLine("Error: Input file is required. Use -s <file_name> to specify the source file.");
    return;
}

try
{
    string sourceCode = File.ReadAllText(inputFile);

    Obfuscator obfuscator = new(sourceCode);
    string obfuscatedCode = obfuscator.Obfuscate(renameSymbols, controlFlow, encryptStrings, antiDebug);

    File.WriteAllText(outputFile, obfuscatedCode);

    Console.WriteLine($"Obfuscation complete. Output written to {outputFile}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}