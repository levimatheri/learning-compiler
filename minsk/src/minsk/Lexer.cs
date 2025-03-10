public class Lexer
{
    private readonly string _text;
    private int _position;
    public Lexer(string text)
    {
        _text = text;
    }

    private char Current
    {
        get
        {
            if (_position >= _text.Length) return '\0';
            return _text[_position];
        }
    }

    private void Next()
    {
        _position++;
    }

    public SyntaxToken NextToken()
    {
        // <numbers>
        // +  - * / ( )
        // <whitespace>

        if (_position >= _text.Length)
        {
            return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
        }

        if (char.IsDigit(Current))
        {
            var start = _position;
            while (char.IsDigit(Current)) 
                Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            if (!int.TryParse(text, out var value))
            {
                
            }
            return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
        }
        
        if (char.IsWhiteSpace(Current))
        {
            var start = _position;
            while (char.IsWhiteSpace(Current)) 
                Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
        }

        return Current switch
        {
            '+' => new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null),
            '-' => new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null),
            '*' => new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null),
            '/' => new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null),
            '(' => new SyntaxToken(SyntaxKind.OpenParenToken, _position++, "(", null),
            ')' => new SyntaxToken(SyntaxKind.CloseParenToken, _position++, ")", null),
            _ => new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null)
        };
    }
}