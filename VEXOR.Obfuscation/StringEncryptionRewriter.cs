using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VEXOR.Obfuscation
{
    /// <summary>
    /// Rewriter class for encrypting string literals.
    /// </summary>
    public class StringEncryptionRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            if (node.IsKind(SyntaxKind.StringLiteralExpression))
            {
                string originalString = node.Token.ValueText;
                // Encrypt the string using Base64 encoding
                string encryptedString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(originalString));

                // Replace the string literal with a call to the decryption method
                var methodInvocation = SyntaxFactory.ParseExpression($"DecryptString(\"{encryptedString}\")");
                return methodInvocation;
            }

            return base.VisitLiteralExpression(node);
        }
    }
}