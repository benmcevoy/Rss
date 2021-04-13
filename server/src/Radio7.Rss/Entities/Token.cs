// From the PCLContrib project http://pclcontrib.codeplex.com/
// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------

namespace Radio7.Rss.Entities
{
    internal struct Token
    {
        public int StartIndex;
        public int EndIndex;
        public string Text;
        public TokenType Type;
    }
}
