﻿using minsk;

public class Parser
{
    private readonly SyntaxToken[] _tokens;
    private List<string> _diagnostics = [];
    private int _position;
    public Parser(string text)
    {
        var tokens = new List<SyntaxToken>();
        var lexer = new Lexer(text);
        SyntaxToken token;
        do
        {
            token = lexer.NextToken();
            if (token.Kind != SyntaxKind.WhitespaceToken &&
                token.Kind != SyntaxKind.BadToken)
            {
                tokens.Add(token);
            }
        } while (token.Kind != SyntaxKind.EndOfFileToken);

        _tokens = [.. tokens];
        _diagnostics.AddRange(lexer.Diagnostics);
    }

    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;
        return index >= _tokens.Length ? _tokens[^1] : _tokens[index];
    }

    private SyntaxToken NextToken()
    {
        var current = Current;
        _position++;
        return current;
    }
    private SyntaxToken Current => Peek(0);

    public SyntaxToken Match(SyntaxKind kind)
    {
        if (Current.Kind == kind) 
            return NextToken();
        
        _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}>, expected <{kind}>");
        return new SyntaxToken(kind, Current.Position, null, null);
    }
    
    public SyntaxTree Parse()
    {
        var expression = ParseTerm();
        var endOfFileToken = Match(SyntaxKind.EndOfFileToken);
        return new SyntaxTree(_diagnostics, expression, endOfFileToken);
    }

    public IEnumerable<string> Diagnostics => _diagnostics;

    private ExpressionSyntax ParseTerm()
    {
        var left = ParseFactor();

        while (Current.Kind == SyntaxKind.PlusToken ||
               Current.Kind == SyntaxKind.MinusToken)
        {
            var operatorToken = NextToken();
            var right = ParseFactor();
            left = new BinaryExpressionSyntax(left, operatorToken, right);
        }

        return left;
    }

    private ExpressionSyntax ParseFactor()
    {
        var left = ParsePrimaryExpression();

        while (Current.Kind == SyntaxKind.StarToken ||
               Current.Kind == SyntaxKind.SlashToken)
        {
            var operatorToken = NextToken();
            var right = ParsePrimaryExpression();
            left = new BinaryExpressionSyntax(left, operatorToken, right);
        }

        return left;
    }

    private ExpressionSyntax ParseExpression()
    {
        return ParseTerm();
    }

    private ExpressionSyntax ParsePrimaryExpression()
    {
        if (Current.Kind == SyntaxKind.OpenParenToken)
        {
            var left = NextToken();
            var expression = ParseExpression();
            var right = Match(SyntaxKind.CloseParenToken);
            return new ParenthesizedExpressionSyntax(left, expression, right);
        }
        
        var numberToken = Match(SyntaxKind.NumberToken);
        return new NumberExpressionSyntax(numberToken);
    }
}