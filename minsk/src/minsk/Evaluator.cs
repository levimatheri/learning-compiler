using System;

namespace minsk;

public class Evaluator
{
    private readonly ExpressionSyntax _root;

    public Evaluator(ExpressionSyntax root)
    {
        _root = root;
    }

    public int Evaluate()
    {

        return EvaluateExpression(_root);
    }

    private int EvaluateExpression(ExpressionSyntax node)
    {
        if (node is NumberExpressionSyntax n) return (int)n.NumberToken.Value;

        if (node is BinaryExpressionSyntax b)
        {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            if (b.OperatorToken.Kind == SyntaxKind.PlusToken) return left + right;
            if (b.OperatorToken.Kind == SyntaxKind.MinusToken) return left - right;
            if (b.OperatorToken.Kind == SyntaxKind.StarToken) return left * right;
            if (b.OperatorToken.Kind == SyntaxKind.SlashToken) return left / right;
            throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
        }

        if (node is ParenthesizedExpressionSyntax p) return EvaluateExpression(p.Expression);

        throw new Exception($"Unexpected node {node.Kind}");
    }
}
