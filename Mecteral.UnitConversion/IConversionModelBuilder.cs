using System.Collections.Generic;

namespace Mecteral.UnitConversion
{
    public interface IConversionModelBuilder
    {
        IConversionExpression BuildFrom(IEnumerable<IConversionToken> tokens);
    }
}