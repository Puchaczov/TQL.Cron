﻿using Cron.Parser.Enums;
using Cron.Parser.Extensions;
using Cron.Parser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cron.Parser.Syntax
{
    public class LWNode : NumberNode
    {
        public LWNode(Token token)
            : base(token)
        { }

        public LWNode()
            : base(new IntegerToken("0"))
        { }

        public override IList<int> Evaluate(Segment segment)
        {
            return ListExtension.Empty();
        }
    }

    public class NumericPreceededLWNode : LWNode
    {
        public NumericPreceededLWNode(Token token)
            : base(token)
        { }
    }
}