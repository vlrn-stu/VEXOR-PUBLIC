using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VEXOR.Obfuscation
{
    /// <summary>
    /// Rewriter class for renaming identifiers, including variables, methods, parameters, classes, namespaces, and fields.
    /// </summary>
    public class RenameRewriter : CSharpSyntaxRewriter
    {
        public int Counter => _counter;
        private readonly Dictionary<string, string> _uniqueNames;
        private readonly List<string> _methodNames;
        private int _counter;

        public RenameRewriter(Dictionary<string, string> uniqueNames, ref int counter, ref List<string> methodNames)
        {
            _uniqueNames = uniqueNames;
            _counter = counter;
            _methodNames = methodNames;
        }

        /// <summary>
        /// Renames namespace declarations.
        /// </summary>
        public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            var identifier = node.Name.ToString();
            if (!_uniqueNames.TryGetValue(identifier, out string? value))
            {
                var newName = GenerateName();
                value = newName;
                _uniqueNames[identifier] = value;
            }

            var newNameSyntax = SyntaxFactory.ParseName(value);
            var newNode = node.WithName(newNameSyntax);

            // Continue visiting child nodes
            return base.VisitNamespaceDeclaration(newNode);
        }

        /// <summary>
        /// Renames class declarations.
        /// </summary>
        public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var identifier = node.Identifier.Text;

            // Avoid renaming the 'Main' method
            if (identifier == "Program")
                return base.VisitClassDeclaration(node);

            if (!_uniqueNames.TryGetValue(identifier, out string? value))
            {
                var newName = GenerateName();
                value = newName;
                _uniqueNames[identifier] = value;
            }

            var newIdentifier = SyntaxFactory.Identifier(value);
            var newNode = node.WithIdentifier(newIdentifier);

            // Continue visiting child nodes
            return base.VisitClassDeclaration(newNode);
        }

        /// <summary>
        /// Renames method declarations.
        /// </summary>
        public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var identifier = node.Identifier.Text;

            // Avoid renaming the 'Main' method
            if (identifier == "Main" && node.Modifiers.Any(SyntaxKind.StaticKeyword))
                return base.VisitMethodDeclaration(node);

            _methodNames.Add(identifier);

            // Continue visiting child nodes
            return base.VisitMethodDeclaration(node);
        }

        /// <summary>
        /// Renames variable declarations.
        /// </summary>
        public override SyntaxNode? VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            var identifier = node.Identifier.Text;

            if (!_uniqueNames.TryGetValue(identifier, out string? value))
            {
                var newName = GenerateName();
                value = newName;
                _uniqueNames[identifier] = value;
            }

            var newIdentifier = SyntaxFactory.Identifier(value);
            var newNode = node.WithIdentifier(newIdentifier);

            // Continue visiting child nodes
            return base.VisitVariableDeclarator(newNode);
        }

        /// <summary>
        /// Renames parameter declarations.
        /// </summary>
        public override SyntaxNode? VisitParameter(ParameterSyntax node)
        {
            var identifier = node.Identifier.Text;

            if (!_uniqueNames.TryGetValue(identifier, out string? value))
            {
                var newName = GenerateName();
                value = newName;
                _uniqueNames[identifier] = value;
            }

            var newIdentifier = SyntaxFactory.Identifier(value);
            var newNode = node.WithIdentifier(newIdentifier);

            // Continue visiting child nodes
            return base.VisitParameter(newNode);
        }

        /// <summary>
        /// Renames property declarations.
        /// </summary>
        public override SyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var identifier = node.Identifier.Text;

            if (!_uniqueNames.TryGetValue(identifier, out string? value))
            {
                var newName = GenerateName();
                value = newName;
                _uniqueNames[identifier] = value;
            }

            var newIdentifier = SyntaxFactory.Identifier(value);
            var newNode = node.WithIdentifier(newIdentifier);

            // Continue visiting child nodes
            return base.VisitPropertyDeclaration(newNode);
        }

        /// <summary>
        /// Renames identifiers in invocation expressions (method calls).
        /// </summary>
        public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var newExpression = (ExpressionSyntax)Visit(node.Expression);
            var newArgumentList = (ArgumentListSyntax)Visit(node.ArgumentList);

            var newNode = node.WithExpression(newExpression)
                              .WithArgumentList(newArgumentList);

            return newNode;
        }

        /// <summary>
        /// Renames identifiers in member access expressions (e.g., obj.Method()).
        /// </summary>
        public override SyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var newExpression = (ExpressionSyntax)Visit(node.Expression);
            var newName = (SimpleNameSyntax)Visit(node.Name);

            var newNode = node.WithExpression(newExpression)
                              .WithName(newName);

            return newNode;
        }

        /// <summary>
        /// Replaces all identifier references in the code with their renamed values.
        /// </summary>
        public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
        {
            var identifier = node.Identifier.Text;

            if (_uniqueNames.TryGetValue(identifier, out var newName))
            {
                var newIdentifier = SyntaxFactory.IdentifierName(newName).WithTriviaFrom(node);
                return newIdentifier;
            }

            return base.VisitIdentifierName(node);
        }

        /// <summary>
        /// Renames object creation expressions (e.g., 'new ClassName()').
        /// </summary>
        public override SyntaxNode? VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            var newType = (TypeSyntax)Visit(node.Type);
            var newArgumentList = (ArgumentListSyntax?)Visit(node.ArgumentList);

            var newNode = node.WithType(newType)
                              .WithArgumentList(newArgumentList);

            return base.VisitObjectCreationExpression(newNode);
        }

        /// <summary>
        /// Generates a unique name for renaming.
        /// </summary>
        private string GenerateName()
        {
            // Generates names like a0, a1, a2, etc.
            return "a" + _counter++;
        }
    }
}