using System.Collections.Generic;

namespace Mecteral.UnitConversion
{
    public interface IConversionTokenizer
    {
        IEnumerable<IConversionToken> Tokens { get; }
        void Tokenize(string input, string abbreviation);
    }
}