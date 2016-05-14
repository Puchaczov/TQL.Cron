﻿using Cron.Core.Tokens;
using System;
using System.Collections.Generic;

namespace Cron.Core.Syntax
{
    public abstract class LexerBase<TToken>: ILexer<TToken>
    {
        protected TToken currentToken;
        protected TToken lastToken;

        protected readonly Dictionary<char, char> endLines = new Dictionary<char, char>();

        protected readonly string input;
        protected int pos;

        protected LexerBase(string input, TToken defaultToken)
        {
            if (input == null || input == string.Empty)
            {
                throw new ArgumentException(nameof(input));
            }
            this.input = input.Trim();

            pos = 0;
            currentToken = defaultToken;
            endLines.Add('\r', '\n');
        }

        public abstract TToken NextToken();

        protected TToken AssignTokenOfType(Func<TToken> instantiate)
        {
            if (instantiate == null)
            {
                throw new ArgumentNullException(nameof(instantiate));
            }

            lastToken = currentToken;
            currentToken = instantiate();
            return currentToken;
        }
    }
}