﻿using Cron.Parser.Exceptions;
using Cron.Parser.Tokens;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cron.Parser
{
    public class Lexer
    {
        private string input;
        private int pos;
        private Token lastToken;
        private Token currentToken;

        public int Position
        {
            get
            {
                return pos;
            }
        }

        public Token Last
        {
            get
            {
                return lastToken.Clone();
            }
        }

        public Lexer(string input)
        {
            if(input == null || input == string.Empty)
            {
                throw new ArgumentException(nameof(input));
            }
            this.input = input.Trim();
            this.pos = 0;
            this.currentToken = new NoneToken(new TextSpan(0, 0));
        }

        public Token NextToken()
        {
            if(pos > input.Count() - 1)
            {
                AssignTokenOfType(() => new EndOfFileToken(new TextSpan(input.Count() - 1, 0)));
                return currentToken;
            }

            char currentChar = input[pos];

            if(IsDigit(currentChar))
            {
                return ConsumeInterger();
            }

            if(IsLetter(currentChar))
            {
                int tmpStartPos = Position;
                var letters = ConsumeLetters();
                int tmpStopPos = Position;
                switch(letters.Value)
                {
                    case "W":
                        return AssignTokenOfType(() => new WToken(new TextSpan(tmpStartPos, tmpStopPos - tmpStartPos)));
                    case "L":
                        return AssignTokenOfType(() => new LToken(new TextSpan(tmpStartPos, tmpStopPos - tmpStartPos)));
                    case "LW":
                        return AssignTokenOfType(() => new LWToken(new TextSpan(tmpStartPos, tmpStopPos - tmpStartPos)));
                    default:
                        return letters;
                }
            }
            
            var lastPos = pos;
            pos += 1;
            switch(currentChar)
            {
                case '#':
                    return AssignTokenOfType(() => new HashToken(new TextSpan(lastPos, 1)));
                case ' ':
                    return AssignTokenOfType(() => new WhiteSpaceToken(new TextSpan(lastPos, 1)));
                case '?':
                    return AssignTokenOfType(() => new QuestionMarkToken(new TextSpan(lastPos, 1)));
                case ',':
                    return AssignTokenOfType(() => new CommaToken(new TextSpan(lastPos, 1)));
                case '*':
                    return AssignTokenOfType(() => new StarToken(new TextSpan(lastPos, 1)));
                case '-':
                    return AssignTokenOfType(() => new RangeToken(new TextSpan(lastPos, 1)));
                case '/':
                    return AssignTokenOfType(() => new IncrementByToken(new TextSpan(lastPos, 1)));
            }

            throw new UnknownTokenException(pos, currentChar);
        }

        private Token AssignTokenOfType(Func<Token> instantiate)
        {
            lastToken = currentToken;
            currentToken = instantiate();
            return currentToken;
        }

        private NameToken ConsumeLetters()
        {
            int startPos = pos;
            int cnt = input.Count();
            while (cnt > pos && IsLetter(input[pos]))
            {
                ++pos;
            }

            return AssignTokenOfType(() => new NameToken(input.Substring(startPos, pos - startPos), new TextSpan(startPos, pos - startPos))) as NameToken;
        }

        private bool IsLetter(char currentChar)
        {
            if(Regex.IsMatch(currentChar.ToString(), "[a-zA-Z]+"))
            {
                return true;
            }
            return false;
        }

        private Token ConsumeInterger()
        {
            int startPos = pos;
            int cnt = input.Count();
            while (cnt > pos && IsDigit(input[pos]))
            {
                ++pos;
            }

            return AssignTokenOfType(() => new IntegerToken(input.Substring(startPos, pos - startPos), new TextSpan(startPos, pos - startPos)));
        }

        public bool IsDigit(char letter)
        {
            if(letter >= '0' && letter <= '9')
            {
                return true;
            }
            return false;
        }
    }
}