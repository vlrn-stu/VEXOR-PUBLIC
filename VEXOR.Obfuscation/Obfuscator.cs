using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VEXOR.Obfuscation
{
    /// <summary>
    /// The Obfuscator class provides methods to obfuscate C# source code.
    /// It includes features like symbol renaming, control flow obfuscation,
    /// string encryption, and anti-debugging techniques.
    /// </summary>
    public class Obfuscator
    {
        private readonly string _sourceCode;
        private readonly SyntaxTree _syntaxTree;
        private static List<string> _methodNames = [];
        private static int _counter = 0;
        private SyntaxNode _root;

        /// <summary>
        /// Initializes a new instance of the Obfuscator class with the provided source code.
        /// </summary>
        /// <param name="sourceCode">The source code to be obfuscated.</param>
        public Obfuscator(string sourceCode)
        {
            _sourceCode = sourceCode;
            _syntaxTree = CSharpSyntaxTree.ParseText(_sourceCode);
            _root = _syntaxTree.GetRoot();
        }

        /// <summary>
        /// Applies the selected obfuscation techniques to the source code.
        /// </summary>
        /// <param name="renameSymbols">Enable symbol renaming.</param>
        /// <param name="controlFlow">Enable control flow obfuscation.</param>
        /// <param name="encryptStrings">Enable string encryption.</param>
        /// <param name="antiDebug">Enable anti-debugging techniques.</param>
        /// <returns>The obfuscated source code as a string.</returns>
        public string Obfuscate(bool renameSymbols, bool controlFlow, bool encryptStrings, bool antiDebug)
        {
            if (renameSymbols)
            {
                _root = RenameIdentifiers(_root);
            }

            if (encryptStrings)
            {
                _root = EncryptStrings(_root);
                _root = InjectStringDecryptor(_root);
            }

            if (controlFlow)
            {
                _root = ObfuscateControlFlow(_root);
            }

            if (antiDebug)
            {
                _root = InjectAntiDebug(_root);
            }

            string result = _root.NormalizeWhitespace().ToFullString();

            // This is to rename the methods, using the visitor method is not proper for this, also the node structure does not make it easy.
            // A simple string replace works, as the visitor finds all declared methods.
            int counter = _counter;
            foreach (var method in _methodNames)
            {
                result = result.Replace(method, $"a{counter}");
                counter++;
            }

            return result;
        }

        /// <summary>
        /// Renames identifiers (variables, methods) to non-meaningful names.
        /// </summary>
        /// <param name="root">The root syntax node.</param>
        /// <returns>The modified syntax node with renamed identifiers.</returns>
        private static SyntaxNode RenameIdentifiers(SyntaxNode root)
        {
            var identifiers = root.DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .Where(id => !(id.Parent is InvocationExpressionSyntax syntax &&
                               syntax.Expression == id));

            var uniqueNames = new Dictionary<string, string>();
            int counter = 0;
            var methodNames = new List<string>();

            var rewriter = new RenameRewriter(uniqueNames, ref counter, ref methodNames);
            var node = rewriter.Visit(root);
            _methodNames = methodNames;
            _counter = rewriter.Counter;
            return node;
        }

        /// <summary>
        /// Encrypts all string literals in the source code.
        /// </summary>
        /// <param name="root">The root syntax node.</param>
        /// <returns>The modified syntax node with encrypted strings.</returns>
        private static SyntaxNode EncryptStrings(SyntaxNode root)
        {
            var rewriter = new StringEncryptionRewriter();
            return rewriter.Visit(root);
        }

        /// <summary>
        /// Injects the string decryption method into the source code.
        /// </summary>
        /// <param name="root">The root syntax node.</param>
        /// <returns>The modified syntax node with the decryption method.</returns>
        private static SyntaxNode InjectStringDecryptor(SyntaxNode root)
        {
            // We should make this much more complex, however for our needs it is sufficient
            const string decryptorCode = @"
            private static string DecryptString(string input)
            {
                var bytes = System.Convert.FromBase64String(input);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }";

            // Parse the decryptor code into a method declaration
            var method = SyntaxFactory.ParseMemberDeclaration(decryptorCode);

            if (method != null)
            {
                method = method.WithLeadingTrivia(SyntaxFactory.Whitespace("\n\t\t"))
                               .WithTrailingTrivia(SyntaxFactory.Whitespace("\n"));

                // Find the first class declaration to inject the method into
                var classNode = root.DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .FirstOrDefault();

                if (classNode != null)
                {
                    // Add the decryption method to the class
                    var newClassNode = classNode.AddMembers(method);
                    root = root.ReplaceNode(classNode, newClassNode);
                }
            }

            return root;
        }

        /// <summary>
        /// Applies rudimentary control flow obfuscation by inverting if statements.
        /// </summary>
        /// <param name="root">The root syntax node.</param>
        /// <returns>The modified syntax node with obfuscated control flow.</returns>
        private static SyntaxNode ObfuscateControlFlow(SyntaxNode root)
        {
            var rewriter = new ControlFlowRewriter();
            return rewriter.Visit(root) ?? root; // Ensure we return a non-null SyntaxNode
        }

        /// <summary>
        /// Injects anti-debugging techniques by adding a background thread that monitors for debuggers.
        /// </summary>
        /// <param name="root">The root syntax node.</param>
        /// <returns>The modified syntax node with anti-debugging code.</returns>
        private static SyntaxNode InjectAntiDebug(SyntaxNode root)
        {
            const string antiDebugCode = @"
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
            }";

            // Parse the anti-debug code into method declarations
            var methods = SyntaxFactory.ParseMemberDeclaration(antiDebugCode);

            if (methods != null)
            {
                methods = methods.WithLeadingTrivia(SyntaxFactory.Whitespace("\n\t\t"))
                                 .WithTrailingTrivia(SyntaxFactory.Whitespace("\n"));

                // Find the first class declaration to inject the methods into
                var classNode = root.DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .FirstOrDefault();

                if (classNode != null)
                {
                    // Add the anti-debug methods to the class
                    var newClassNode = classNode.AddMembers(methods);

                    // Find the Main method to insert the anti-debug thread starter
                    var mainMethod = newClassNode.DescendantNodes()
                        .OfType<MethodDeclarationSyntax>()
                        .FirstOrDefault(m => m.Identifier.Text == "Main");

                    if (mainMethod != null && mainMethod.Body != null)
                    {
                        // Create a statement to start the anti-debug thread
                        var antiDebugInvocation = SyntaxFactory.ParseStatement("StartAntiDebugThread();\n");

                        // Insert the anti-debug invocation at the beginning of Main
                        var newBody = mainMethod.Body.WithStatements(
                            mainMethod.Body.Statements.Insert(0, antiDebugInvocation));

                        var newMainMethod = mainMethod.WithBody(newBody);
                        newClassNode = newClassNode.ReplaceNode(mainMethod, newMainMethod);
                    }

                    root = root.ReplaceNode(classNode, newClassNode);
                }
            }

            return root;
        }
    }
}