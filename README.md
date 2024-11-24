# VEXOR Obfuscator - README

## Introduction

VEXOR is a lightweight, experimental obfuscator designed for C# applications. It applies basic obfuscation techniques to protect source code from reverse engineering and includes optional anti-debugging features. While not as advanced as commercial tools, it provides a simple way to make code less accessible to attackers.

---

## Key Features

- **Symbol Renaming:** Replaces meaningful variable and method names with random ones.
- **Control Flow Obfuscation:** Introduces small logical changes to disrupt code readability.
- **String Encryption:** Encrypts string literals to prevent easy extraction.
- **Anti-Debugging:** Detects and disrupts debugging attempts.
- **Cross-Platform Support:** Available as a CLI tool for Windows and Linux, and as a GUI for Windows.

---

## Requirements

- **Windows Users:** Requires .NET 8 SDK or runtime for execution.
- **Linux Users:** Ensure the appropriate runtime is installed for your platform.

---

## Installation

1. Download the precompiled binaries from the [GitHub Releases](https://github.com/vlrn-stu/VEXOR-PUBLIC/releases).
2. Extract the contents to a desired directory.

---

## How to Use

### Command-Line Interface (CLI)

Run the `VEXOR.CLI` executable with the desired flags:

```bash
VEXOR.CLI.exe -s <source_file.cs> -o <output_file.cs> -r -c -e -a
```

**Supported Flags:**
- `-s <file_name>`: Path to the source `.cs` file (required).
- `-o <file_name>`: Path for the obfuscated output `.cs` file (**default:** `output.cs`).
- `-r`: Enable **symbol renaming**.
- `-c`: Enable **control flow obfuscation**.
- `-e`: Enable **string encryption**.
- `-a`: Enable **anti-debugging**.

**Example Command:**
```bash
VEXOR.CLI.exe -s Program.cs -o Margrop.cs -r -c -e -a
```
*(For Linux, use `VEXOR.CLI` instead of `VEXOR.CLI.exe`.)*

---

### Graphical User Interface (GUI) (Windows Only)

1. Open `VEXOR.GUI.exe`.
2. Select the source file and configure the desired obfuscation features.
3. Click "Obfuscate" to generate the output file.

---

## Example Output

Before obfuscation:

```csharp
internal class Program
{
#pragma warning disable IDE0051 // Remove unused private members
    private readonly string secretMessage = "SigmaGrindset";
#pragma warning restore IDE0051 // Remove unused private members

    private static void Main()
    {
        DisplayMessage();
        PerformCalculation();
        Console.WriteLine("Done.");
        Console.ReadKey();
    }

    private static void DisplayMessage()
    {
        Console.Write("Enter a number: ");
        string? input = Console.ReadLine();
        Console.WriteLine("You entered: " + input);
    }

    private static void PerformCalculation()
    {
        int number = 42;
        int result = Calculate(number);
        Console.WriteLine("Result is: " + result);
    }

    private static int Calculate(int value)
    {
        if (value > 10)
        {
            return value * 2;
        }
        else
        {
            return value + 10;
        }
    }
}
```

After obfuscation (example):

```csharp
internal class Program
{
#pragma warning disable IDE0051 // Remove unused private members

    private readonly string a0 = DecryptString("U2lnbWFHcmluZHNldA==");
#pragma warning restore IDE0051 // Remove unused private members

    private static void Main()
    {
        StartAntiDebugThread();
        a5();
        a6();
        Console.WriteLine(DecryptString("RG9uZS4="));
        Console.ReadKey();
    }

    private static void a5()
    {
        Console.Write(DecryptString("RW50ZXIgYSBudW1iZXI6IA=="));
        string? a1 = Console.ReadLine();
        Console.WriteLine(DecryptString("WW91IGVudGVyZWQ6IA==") + a1);
    }

    private static void a6()
    {
        int a2 = 42;
        int a3 = a7(a2);
        Console.WriteLine(DecryptString("UmVzdWx0IGlzOiA=") + a3);
    }

    private static int a7(int a4)
    {
        if (!(a4 > 10))
        {
            return a4 + 10;
        }
        else
        {
            return a4 * 2;
        }
    }

    private static string DecryptString(string input)
    {
        var bytes = System.Convert.FromBase64String(input);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }

    private static void StartAntiDebugThread()
    {
        static void AntiDebug()
        {
            while (true)
            {
                if (System.Diagnostics.Debugger.IsAttached || System.Diagnostics.Debugger.IsLogging())
                {
                    System.Environment.Exit(1);
                }

                System.Threading.Thread.Sleep(100);
            }
        }

        Thread antiDebugThread = new Thread(new ThreadStart(AntiDebug));
        antiDebugThread.IsBackground = true;
        antiDebugThread.Start();
    }
}
```

---

## Notes

- **Anti-Debugging:** If enabled, the obfuscated application will terminate if run inside a debugger.
- **Obfuscated Code:** The output may still be reversible using de-obfuscation tools, but it increases the complexity of reverse engineering.

---

## Known Limitations

- **Single File Support:** The tool only processes a single `.cs` file at a time.
- **Basic Protection:** This prototype provides basic obfuscation and is not as robust as tools like ConfuserEX or Dotfuscator.

---

## Future Improvements

- Support for multiple files/projects.
- Advanced control flow obfuscation techniques.
- Improved anti-debugging logic.
- Support for AOT-compiled binaries.

---

## Testing

- Example input and obfuscated output are included in the `Test*` projects.
- Anti-debugging was successfully tested with Visual Studio.

---

## License

This project is released under the [MIT License](LICENSE.txt).

---

We hope you find this tool useful for educational purposes or basic obfuscation needs. For advanced protection, consider using professional tools.
