using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    public class MacroCodeTokenizer
    {

        public MacroCodeTokenizer(string formula)
        {
            this.formula = formula;
        }

        private string formula;

        public string CommandCode { get; private set; }

        public List<Token> Process()
        {
            if (formula == null)
                return tokens;
            tokens.Clear();
            processFormula();
            return tokens;
        }

        private void processFormula()
        {
            CommandCode = "";
            currentPosition = 0;
            currentTokenStart = 0;
            state = State.StartingWhiteSpaces;
            while (currentPosition <= formula.Length)
                nextChar();
        }

        #region State machine
        int currentPosition;
        State state;

        bool escaped = false;

        private void nextChar()
        {

            char currentChr = (currentPosition < formula.Length) ? formula[currentPosition] : (char)0;
            bool setEscapedFlag = false;

            switch (state)
            {

                case State.StartingWhiteSpaces:
                    if (isCommandCodeAllowed(currentChr))
                    {
                        state = State.CommandCode;
                        CommandCode += currentChr;
                    }
                    else if (currentChr == 0)
                    {
                        state = State.Empty;
                    }
                    else if (!isWhitespace(currentChr))
                    {
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandCode:
                    if (isCommandCodeAllowed(currentChr))
                    {
                        CommandCode += currentChr;
                    }
                    else if (currentChr == '(')
                    {
                        state = State.CommandArgumentListStart;
                        addToken(TokenType.CommandCode, false);
                    }
                    else if (isWhitespace(currentChr))
                    {
                        state = State.CommandCodeWhiteSpacesAfter;
                        addToken(TokenType.CommandCode, false);
                    }
                    else if (currentChr == 0)
                    {
                        addToken(TokenType.CommandCode, false);
                        incomplete();
                    }
                    else
                    { 
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandCodeWhiteSpacesAfter:
                    if (currentChr == '(')
                    {
                        state = State.CommandArgumentListStart;
                    }
                    else if (currentChr == 0)
                    {
                        incomplete();
                    }
                    else if (!isWhitespace(currentChr))
                    {
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandArgumentListStart:
                case State.CommandArgumentListStartWhiteSpacesAfter:
                    if (isNumeric(currentChr))
                    {
                        state = State.CommandNumberArgumentWhole;
                        currentTokenStart = currentPosition;
                        currentArgumentValue = "";
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == '"')
                    {
                        state = State.CommandStringArgumentStart;
                        currentTokenStart = currentPosition;
                        currentArgumentValue = "";
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == ')')
                    {
                        state = State.CommandArgumentListEnd;
                    }
                    else if (isWhitespace(currentChr))
                    {
                        state = State.CommandArgumentListStartWhiteSpacesAfter;
                    }
                    else if (currentChr == 0)
                    {
                        incomplete();
                    }
                    else
                    {
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandArgumentSeparator:
                    currentArgumentValue = "";
                    if (isNumeric(currentChr))
                    {
                        state = State.CommandNumberArgumentWhole;
                        currentTokenStart = currentPosition;
                        currentArgumentValue = "";
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == '"')
                    {
                        state = State.CommandStringArgumentStart;
                        currentTokenStart = currentPosition;
                        currentArgumentValue = "";
                    }
                    else if (isWhitespace(currentChr))
                    {
                        state = State.CommandArgumentSeparatorWhitespacesAfter;
                    }
                    else if (currentChr == 0)
                    {
                        incomplete();
                    }
                    else
                    {
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandArgumentSeparatorWhitespacesBefore:
                    if (currentChr == ',')
                    {
                        state = State.CommandArgumentSeparator;
                    }
                    else if (currentChr == 0)
                    {
                        incomplete();
                    }
                    else if (!isWhitespace(currentChr))
                    {
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandArgumentSeparatorWhitespacesAfter:
                    if (isNumeric(currentChr))
                    {
                        state = State.CommandNumberArgumentWhole;
                        currentTokenStart = currentPosition;
                        currentArgumentValue = "";
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == '"')
                    {
                        state = State.CommandStringArgumentStart;
                        currentTokenStart = currentPosition;
                        currentArgumentValue = "";
                    }
                    else if (currentChr == 0)
                    {
                        incomplete();
                    }
                    else if (!isWhitespace(currentChr))
                    { 
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandStringArgumentStart:
                    if (currentChr == '"')
                    {
                        state = State.CommandStringArgumentEnd;
                    }
                    else if (currentChr == 0)
                    {
                        addToken(TokenType.StringArgument, false);
                        incomplete();
                    }
                    else
                    {
                        state = State.CommandStringArgumentContent;
                        if (currentChr == '\\')
                            setEscapedFlag = true;
                        else
                            currentArgumentValue += currentChr;
                    }
                    break;

                case State.CommandStringArgumentContent:
                    if ((currentChr == '"') && !escaped)
                    {
                        state = State.CommandStringArgumentEnd;
                        addToken(TokenType.StringArgument, true);
                    }
                    else if ((currentChr == '\\') && !escaped)
                    {
                        setEscapedFlag = true;
                    }
                    else if (currentChr == 0)
                    {
                        addToken(TokenType.StringArgument, false);
                        incomplete();
                    }
                    else
                    {
                        currentArgumentValue += currentChr;
                    }
                    break;

                case State.CommandStringArgumentEnd:
                    if (currentChr == ',')
                    {
                        addStringArgument();
                        addToken(TokenType.StringArgument, false);
                        state = State.CommandArgumentSeparator;
                    }
                    else if (isWhitespace(currentChr))
                    {
                        addStringArgument();
                        addToken(TokenType.StringArgument, false);
                        state = State.CommandArgumentSeparatorWhitespacesBefore;
                    }
                    else if (currentChr == ')')
                    {
                        addStringArgument();
                        addToken(TokenType.StringArgument, false);
                        state = State.CommandArgumentListEnd;
                    }
                    else
                    {
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandNumberArgumentWhole:
                    if (isNumeric(currentChr))
                    {
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == '.')
                    {
                        currentArgumentValue += ".";
                        state = State.CommandNumberArgumentDecimalPoint;
                    }
                    else if (currentChr == ',')
                    {
                        addIntArgument();
                        addToken(TokenType.IntArgument, false);
                        state = State.CommandArgumentSeparator;
                    }
                    else if (isWhitespace(currentChr))
                    {
                        addIntArgument();
                        addToken(TokenType.IntArgument, false);
                        state = State.CommandArgumentSeparatorWhitespacesBefore;
                    }
                    else if (currentChr == ')')
                    {
                        addIntArgument();
                        addToken(TokenType.IntArgument, false);
                        state = State.CommandArgumentListEnd;
                    }
                    else if (currentChr == 0)
                    {
                        addToken(TokenType.IntArgument, false);
                        incomplete();
                    }
                    else
                    {
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandNumberArgumentDecimalPoint:
                    if (isNumeric(currentChr))
                    {
                        state = State.CommandNumberArgumentFractional;
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == 0)
                    {
                        addToken(TokenType.FloatArgument, false);
                        incomplete();
                    }
                    else
                    {
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandNumberArgumentFractional:
                    if (isNumeric(currentChr))
                    {
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == ',')
                    {
                        addFloatArgument();
                        addToken(TokenType.FloatArgument, false);
                        state = State.CommandArgumentSeparator;
                    }
                    else if (isWhitespace(currentChr))
                    {
                        addFloatArgument();
                        addToken(TokenType.FloatArgument, false);
                        state = State.CommandArgumentSeparatorWhitespacesBefore;
                    }
                    else if (currentChr == ')')
                    {
                        addFloatArgument();
                        addToken(TokenType.FloatArgument, false);
                        state = State.CommandArgumentListEnd;
                    }
                    else if (isWhitespace(currentChr))
                    {
                        state = State.CommandArgumentSeparatorWhitespacesBefore;
                    }
                    else if (currentChr == 0)
                    {
                        addToken(TokenType.FloatArgument, false);
                        incomplete();
                    }
                    else
                    {
                        fatalSyntaxError();
                    }
                    break;

                case State.CommandArgumentListEnd:
                    if (isWhitespace(currentChr))
                    {
                        state = State.EndingWhiteSpaces;
                    }
                    else
                    {
                        fatalSyntaxError();
                    }
                    break;

                case State.EndingWhiteSpaces:
                    if (!isWhitespace(currentChr))
                        fatalSyntaxError();
                    break;

                case State.SyntaxError:
                case State.EndButIncomplete:
                case State.Empty:
                default:
                    break;

            }

            Console.WriteLine("{0}: {1}", currentChr, state);

            currentPosition++;
            escaped = setEscapedFlag;

        }

        private static bool isAlphabetic(char c)
            => (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')));

        private static bool isAlphanumeric(char c)
            => (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || ((c >= '0') && (c <= '9')));

        private static bool isNumeric(char c)
            => (((c >= '0') && (c <= '9')));

        private static bool isAlphanumericOrUnderscore(char c)
            => (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || ((c >= '0') && (c <= '9')) || (c == '_'));

        private static bool isCommandCodeAllowed(char c)
            => (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || ((c >= '0') && (c <= '9')) || (c == '_') || (c == '.'));

        private static bool isWhitespace(char c)
            => ((c == ' ') || (c == '\t'));
        private enum State
        {
            Literal,
            StartingWhiteSpaces,
            EndingWhiteSpaces,
            CommandCode,
            CommandCodeWhiteSpacesAfter,
            CommandArgumentListStart,
            CommandArgumentListStartWhiteSpacesAfter,
            CommandArgumentSeparator,
            CommandArgumentSeparatorWhitespacesBefore,
            CommandArgumentSeparatorWhitespacesAfter,
            CommandStringArgumentStart,
            CommandStringArgumentContent,
            CommandStringArgumentEnd,
            CommandNumberArgumentWhole,
            CommandNumberArgumentDecimalPoint,
            CommandNumberArgumentFractional,
            CommandArgumentListEnd,
            SyntaxError,
            EndButIncomplete,
            Empty
        }
        #endregion

        #region Arguments
        private List<IArgument> arguments = new List<IArgument>();

        public IReadOnlyList<IArgument> Arguments
            => arguments.AsReadOnly();

        private string currentArgumentValue = "";

        private void addStringArgument()
        {
            arguments.Add(new StringArgument(currentArgumentValue));
            currentArgumentValue = "";
        }

        private void addIntArgument()
        {
            arguments.Add(new IntArgument(currentArgumentValue));
            currentArgumentValue = "";
        }

        private void addFloatArgument()
        {
            arguments.Add(new FloatArgument(currentArgumentValue));
            currentArgumentValue = "";
        }

        private void clearArguments()
        {
            arguments.Clear();
        }

        public interface IArgument
        {
            string StrValue { get; }
        }

        private class StringArgument : IArgument
        {
            public string StrValue => Value.ToString();
            public string Value { get; private set; }
            public StringArgument(string str)
            {
                Value = str;
            }
        }

        private class IntArgument : IArgument
        {
            public string StrValue => Value.ToString();
            public int Value { get; private set; }
            public IntArgument(string numberStr)
            {
                if (!int.TryParse(numberStr, out int v))
                    throw new ArgumentException();
                Value = v;
            }
        }

        private class FloatArgument : IArgument
        {

            private static readonly NumberStyles FLOAT_PARSE_STYLE = NumberStyles.AllowDecimalPoint;

            private static readonly IFormatProvider FLOAT_PARSE_FORMAT = CultureInfo.CreateSpecificCulture("en-US");
            public string StrValue => Value.ToString();
            public float Value { get; private set; }
            public FloatArgument(string numberStr)
            {
                if (!float.TryParse(numberStr, FLOAT_PARSE_STYLE, FLOAT_PARSE_FORMAT, out float v))
                    throw new ArgumentException();
                Value = v;
            }
        }
        #endregion

        #region Tokens
        private List<Token> tokens = new List<Token>();

        public IReadOnlyList<Token> Tokens
            => tokens.AsReadOnly();


        int currentTokenStart;
        private void addToken(TokenType type, bool containsCurrent)
        {
            int currentTokenEnd = containsCurrent ? (currentPosition + 1) : currentPosition;
            string newTokenContent = formula.Substring(currentTokenStart, currentTokenEnd - currentTokenStart);
            Token newToken = new Token(type, newTokenContent, currentTokenStart, currentTokenEnd);
            tokens.Add(newToken);
        }

        public enum TokenType
        {
            CommandCode,
            StringArgument,
            IntArgument,
            FloatArgument
        }

        public class Token
        {

            public TokenType Type { get; private set; }

            public string Content { get; private set; }

            public int StartPosition { get; private set; }

            public int EndPosition { get; private set; }

            public int Length => EndPosition - StartPosition;

            internal Token(TokenType type, string content, int startPosition, int endPosition)
            {
                Type = type;
                Content = content;
                StartPosition = startPosition;
                EndPosition = endPosition;
            }

        }
        #endregion

        #region Formula state
        public bool IsComplete
            => ((state == State.CommandArgumentListEnd) || (state == State.EndingWhiteSpaces));

        public bool IsEmpty
            => (state == State.Empty);
        public bool HasSyntaxError { get; private set; } = false;

        public int SyntaxErrorPosition { get; private set; }

        private void fatalSyntaxError()
        {
            state = State.SyntaxError;
            HasSyntaxError = true;
            SyntaxErrorPosition = currentPosition;
        }

        private void incomplete()
        {
            state = State.EndButIncomplete;
        }
        #endregion


    }

}
