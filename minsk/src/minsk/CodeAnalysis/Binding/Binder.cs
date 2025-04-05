﻿using minsk.CodeAnalysis.Syntax;

namespace minsk.CodeAnalysis.Binding;

internal sealed class Binder
{
    private readonly List<string> _diagnostics = new List<string>();

    public IEnumerable<string> Diagnostics => _diagnostics;

    public BoundExpression BindExpression(ExpressionSyntax syntax)
    {
        switch (syntax.Kind)
        {
            case SyntaxKind.LiteralExpression:
                return BindLiteralExpression((LiteralExpressionSyntax)syntax);
            case SyntaxKind.UnaryExpression:
                return BindUnaryExpression((UnaryExpressionSyntax)syntax);
            case SyntaxKind.BinaryExpression:
                return BindBinaryExpression((BinaryExpressionSyntax)syntax);
            default:
                throw new Exception($"Unexpected syntax {syntax.Kind}");
        }
    }

    private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax)
    {
        var value = syntax.LiteralToken.Value as int? ?? 0;
        return new BoundLiteralExpression(value);
    }

    private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
    {
        var boundLeft = BindExpression(syntax.Left);
        var boundRight = BindExpression(syntax.Right);
        var boundOperatorKind = BindBinaryOperatorKind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);

        if (boundOperatorKind == null)
        {
            _diagnostics.Add($"Binary operator {syntax.OperatorToken.Kind} is not defined for types {boundLeft.Type} and {boundRight.Type}");
            return boundLeft;
        }

        return new BoundBinaryExpression(boundLeft, boundOperatorKind.Value, boundRight);
    }

    private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
    {
        var boundOperand = BindExpression(syntax.Operand);
        var boundOperatorKind = BindUnaryOperatorKind(syntax.OperatorToken.Kind, boundOperand.Type);

        if (boundOperatorKind == null)
        {
            _diagnostics.Add($"Unary operator {syntax.OperatorToken.Kind} is not defined for type {boundOperand.Type}");
            return boundOperand;
        }

        return new BoundUnaryExpression(boundOperatorKind.Value, boundOperand);
    }

    private BoundUnaryOperatorKind? BindUnaryOperatorKind(SyntaxKind kind, Type operandType)
    {
        if (operandType != typeof(int)) return null;

        return kind switch
        {
            SyntaxKind.PlusToken => BoundUnaryOperatorKind.Identity,
            SyntaxKind.MinusToken => BoundUnaryOperatorKind.Negation,
            _ => throw new Exception($"Unexpected unary operator {kind}")
        };
    }
     
    private BoundBinaryOperatorKind? BindBinaryOperatorKind(SyntaxKind kind, Type leftType, Type rightType)
    {
        if (leftType != typeof(int) || rightType != typeof(int))
            return null;

        return kind switch
        {
            SyntaxKind.PlusToken => BoundBinaryOperatorKind.Addition,
            SyntaxKind.MinusToken => BoundBinaryOperatorKind.Subtraction,
            SyntaxKind.StarToken => BoundBinaryOperatorKind.Multiplication,
            SyntaxKind.SlashToken => BoundBinaryOperatorKind.Division,
            _ => throw new Exception($"Unexpected binary operator {kind}")
        };
    }
}