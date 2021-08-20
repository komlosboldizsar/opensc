using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    internal class DynamicTextSubstituteBuilder
    {

        public DynamicTextSubstituteBuilder(string formula)
        {
            this.formula = formula;
        }

        private string formula;

        List<IDynamicTextFunctionSubstitute> substitutes = new List<IDynamicTextFunctionSubstitute>();

        public IReadOnlyList<IDynamicTextFunctionSubstitute> Substitutes
        {
            get => substitutes;
        }

        public List<IDynamicTextFunctionSubstitute> Build()
        {
            if (formula == null)
                return substitutes;
            substitutes.Clear();
            processFormula();
            return substitutes;
        }

        int currentPosition;
        TokenType state;

        private void processFormula()
        {
            currentPosition = 0;
            state = TokenType.Literal;
            while (currentPosition <= formula.Length)
                nextChar();
        }

        bool escaped = false;

        private void nextChar()
        {

            char currentChr = (currentPosition < formula.Length) ? formula[currentPosition] : (char)0;
            bool setEscapedFlag = false;

            switch (state)
            {

                case TokenType.Literal:
                    if ((currentChr == '%') && !escaped)
                    {
                        state = TokenType.FunctionStart;
                        literalEnd();
                    }
                    else if ((currentChr == '\\') && !escaped)
                    {
                        setEscapedFlag = true;
                    }
                    else if (currentChr == 0)
                    {
                        literalEnd();
                    }
                    else
                    {
                        currentLiteral += currentChr;
                    }
                    break;

                case TokenType.FunctionStart:
                    if (!isAlphabetic(currentChr))
                        syntaxError();
                    state = TokenType.FunctionName;
                    functionStart();
                    functionName += currentChr;
                    break;

                case TokenType.FunctionName:
                    if (isAlphanumericOrUnderscore(currentChr))
                    {
                        functionName += currentChr;
                    }
                    else if (currentChr == '(')
                    {
                        state = TokenType.FunctionArgumentListStart;
                    }
                    else
                    {
                        syntaxError();
                    }
                    break;

                case TokenType.FunctionArgumentListStart:
                    currentArgumentValue = "";
                    if (isNumeric(currentChr))
                    {
                        state = TokenType.FunctionNumberArgumentWhole;
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == '"')
                    {
                        state = TokenType.FunctionStringArgumentStart;
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == ')')
                    {
                        state = TokenType.FunctionArgumentListEnd;
                    }
                    else
                    {
                        syntaxError();
                    }
                    break;

                case TokenType.FunctionArgumentSeparator:
                    currentArgumentValue = "";
                    if (isNumeric(currentChr))
                    {
                        state = TokenType.FunctionNumberArgumentWhole;
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == '"')
                    {
                        state = TokenType.FunctionStringArgumentStart;
                    }
                    else
                    {
                        syntaxError();
                    }
                    break;

                case TokenType.FunctionStringArgumentStart:
                    if (currentChr == '"')
                    {
                        state = TokenType.FunctionStringArgumentEnd;
                    }
                    else if (currentChr == 0)
                    {
                        syntaxError();
                    }
                    else
                    {
                        state = TokenType.FunctionStringArgumentContent;
                        if (currentChr == '\\')
                            setEscapedFlag = true;
                        else
                            currentArgumentValue += currentChr;
                    }
                    break;

                case TokenType.FunctionStringArgumentContent:
                    if ((currentChr == '"') && !escaped)
                    {
                        state = TokenType.FunctionStringArgumentEnd;
                    }
                    else if ((currentChr == '\\') && !escaped)
                    {
                        setEscapedFlag = true;
                    }
                    else if (currentChr == 0)
                    {
                        syntaxError();
                    }
                    else
                    {
                        currentArgumentValue += currentChr;
                    }
                    break;

                case TokenType.FunctionStringArgumentEnd:
                    if (currentChr == ',')
                    {
                        addStringArgument();
                        state = TokenType.FunctionArgumentSeparator;
                    }
                    else if (currentChr == ')')
                    {
                        addStringArgument();
                        state = TokenType.FunctionArgumentListEnd;
                    }
                    else
                    {
                        syntaxError();
                    }
                    break;

                case TokenType.FunctionNumberArgumentWhole:
                    if (isNumeric(currentChr))
                    {
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == '.')
                    {
                        currentArgumentValue += ".";
                        state = TokenType.FunctionNumberArgumentDecimalPoint;
                    }
                    else if (currentChr == ',')
                    {
                        addIntArgument();
                        state = TokenType.FunctionArgumentSeparator;
                    }
                    else if (currentChr == ')')
                    {
                        addIntArgument();
                        state = TokenType.FunctionArgumentListEnd;
                    }
                    else
                    {
                        syntaxError();
                    }
                    break;

                case TokenType.FunctionNumberArgumentDecimalPoint:
                    if (isNumeric(currentChr))
                    {
                        state = TokenType.FunctionNumberArgumentFractional;
                        currentArgumentValue += currentChr;
                    }
                    else
                    {
                        syntaxError();
                    }
                    break;

                case TokenType.FunctionNumberArgumentFractional:
                    if (isNumeric(currentChr))
                    {
                        currentArgumentValue += currentChr;
                    }
                    else if (currentChr == ',')
                    {
                        addFloatArgument();
                        state = TokenType.FunctionArgumentSeparator;
                    }
                    else if (currentChr == ')')
                    {
                        addFloatArgument();
                        state = TokenType.FunctionArgumentListEnd;
                    }
                    else
                    {
                        syntaxError();
                    }
                    break;

                case TokenType.FunctionEnd:
                    if (currentChr == '%')
                    {
                        state = TokenType.FunctionStart;
                    }
                    else
                    {
                        state = TokenType.Literal;
                        currentLiteral += currentChr;
                    }
                    functionEnd();
                    break;

                case TokenType.FunctionArgumentListEnd:
                    if (currentChr != '%')
                        syntaxError();
                    state = TokenType.FunctionEnd;
                    break;

            }

            currentPosition++;
            escaped = setEscapedFlag;

        }

        private string functionName = "";
        private string currentLiteral = "";
        private string currentArgumentValue = "";

        private List<IArgument> collectedArguments = new List<IArgument>();

        private void addStringArgument()
        {
            collectedArguments.Add(new StringArgument(currentArgumentValue));
            currentArgumentValue = "";
        }

        private void addIntArgument()
        {
            collectedArguments.Add(new IntArgument(currentArgumentValue));
            currentArgumentValue = "";
        }

        private void addFloatArgument()
        {
            collectedArguments.Add(new FloatArgument(currentArgumentValue));
            currentArgumentValue = "";
        }

        private void clearArguments()
        {
            collectedArguments.Clear();
        }

        private void literalEnd()
        {
            substitutes.Add(new StringSubstitute(currentLiteral));
            currentLiteral = "";
        }

        private void functionStart()
        {
            clearArguments();
            functionName = "";
        }

        private void functionEnd()
        {

            IDynamicTextFunction function = DynamicTextFunctionRegister.Instance.GetFunction(functionName);
            if (function == null)
            {
                string substituteText = string.Format("%{0}:E(un)%", functionName);
                substitutes.Add(new StringSubstitute(substituteText));
                return;
            }

            try
            {
                object[] args = convertArguments(function);
                substitutes.Add(function.GetSubstitute(args));
            }
            catch(ArgumentCountMismatchException)
            {
                string substituteText = string.Format("%{0}:E(ac)%", functionName);
                substitutes.Add(new StringSubstitute(substituteText));
                return;
            }
            catch (ArgumentTypeMismatchException)
            {
                string substituteText = string.Format("%{0}:E(at)%", functionName);
                substitutes.Add(new StringSubstitute(substituteText));
                return;
            }

        }

        private object[] convertArguments(IDynamicTextFunction forFunction)
        {
            if (forFunction.ArgumentCount != collectedArguments.Count)
                throw new ArgumentCountMismatchException();
            object[] convertedArguments = new object[forFunction.ArgumentCount];
            int i = 0;
            foreach (IDynamicTextFunctionArgument argument in forFunction.Arguments)
                convertedArguments[i] = convertArgument(collectedArguments[i++], argument.KeyType);
            return convertedArguments;
        }

        private object convertArgument(IArgument collectedArgument, DynamicTextFunctionArgumentType requiredType)
        {

            if (requiredType == DynamicTextFunctionArgumentType.String)
                return collectedArgument.ObjValue.ToString();

            if (requiredType == DynamicTextFunctionArgumentType.Float)
            {
                if (!((collectedArgument is FloatArgument) || (collectedArgument is IntArgument)))
                    throw new ArgumentTypeMismatchException();
                return (float)Convert.ToDouble(collectedArgument.ObjValue);
            }

            if (requiredType == DynamicTextFunctionArgumentType.Integer)
            {
                if (!(collectedArgument is IntArgument))
                    throw new ArgumentTypeMismatchException();
                return Convert.ToInt32(collectedArgument.ObjValue);
            }

            return null;

        }

        private void syntaxError(string message = "")
        {
            throw new FormulaSyntaxErrorException(currentPosition, message);
        }

        private interface IArgument
        {
            object ObjValue { get; }
        }

        private class StringArgument: IArgument
        {
            public object ObjValue
            {
                get => Value;
            }

            public string Value { get; private set; }

            public StringArgument(string str)
            {
                Value = str;
            }

        }

        private class IntArgument: IArgument
        {

            public object ObjValue
            {
                get => Value;
            }

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

            public object ObjValue
            {
                get => Value;
            }

            public float Value { get; private set; }

            public FloatArgument(string numberStr)
            {
                if (!float.TryParse(numberStr, out float v))
                    throw new ArgumentException();
                Value = v;
            }

        }

        private static bool isAlphabetic(char c)
        {
            return ((c >= 'a') && (c <= 'z')) | ((c >= 'A') && (c <= 'Z'));
        }

        private static bool isAlphanumeric(char c)
        {
            return ((c >= 'a') && (c <= 'z')) | ((c >= 'A') && (c <= 'Z')) | ((c >= '0') && (c <= '9'));
        }

        private static bool isNumeric(char c)
        {
            return ((c >= '0') && (c <= '9'));
        }

        private static bool isAlphanumericOrUnderscore(char c)
        {
            return ((c >= 'a') && (c <= 'z')) | ((c >= 'A') && (c <= 'Z')) | ((c >= '0') && (c <= '9')) | (c == '_');
        }

        private enum TokenType
        {
            Literal,
            FunctionStart,
            FunctionName,
            FunctionArgumentListStart,
            FunctionArgumentSeparator,
            FunctionStringArgumentStart,
            FunctionStringArgumentContent,
            FunctionStringArgumentEnd,
            FunctionNumberArgumentWhole,
            FunctionNumberArgumentDecimalPoint,
            FunctionNumberArgumentFractional,
            FunctionArgumentListEnd,
            FunctionEnd
        }

        public class FormulaSyntaxErrorException : Exception
        {

            public int Position { get; private set; } = -1;

            public FormulaSyntaxErrorException()
            { }

            public FormulaSyntaxErrorException(string message) :
                base(message)
            { }

            public FormulaSyntaxErrorException(int position, string message = "") :
                base(string.Format("Syntax error at character {0}.{1}{2}", position, ((message != "") ? " " : ""), message))
            {
                Position = position;
            }

            public FormulaSyntaxErrorException(string message, Exception innerException) :
                base(message, innerException)
            { }

        }

        private class ArgumentCountMismatchException : Exception
        {
            public ArgumentCountMismatchException()
            { }
        }

        private class ArgumentTypeMismatchException : Exception
        {
            public ArgumentTypeMismatchException()
            { }
        }

        internal class StringSubstitute : IDynamicTextFunctionSubstitute
        {

            public StringSubstitute(string text)
            {
                CurrentValue = text;
            }

            public void Init(object[] argumentObjects)
            { }

            public string CurrentValue { get; private set; }

            public event DynamicTextFunctionSubstituteValueChanged ValueChanged
            {
                add { }
                remove { }
            }

        }

    }

}
