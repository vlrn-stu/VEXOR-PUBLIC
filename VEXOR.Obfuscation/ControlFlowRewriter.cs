using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VEXOR.Obfuscation
{
    /// <summary>
    /// Rewriter class for control flow obfuscation by inverting conditions and adding extra brackets.
    /// </summary>
    public class ControlFlowRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitIfStatement(IfStatementSyntax node)
        {
            var trueStatement = node.Statement;
            var falseStatement = node.Else?.Statement ?? SyntaxFactory.Block();

            var condition = node.Condition;

            // Add extra parentheses around the condition
            var parenthesizedCondition = SyntaxFactory.ParenthesizedExpression(condition);

            // Invert the condition using logical NOT
            var notCondition = SyntaxFactory.PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, parenthesizedCondition);

            // Swap the true and false branches
            var obfuscatedIf = SyntaxFactory.IfStatement(notCondition, falseStatement)
                .WithElse(SyntaxFactory.ElseClause(trueStatement));

            return base.VisitIfStatement(obfuscatedIf);
        }
    }
}