namespace Mecteral.UnitConversion
{
    public class ImperialMassToken : AConversionTokens, IConversionToken
    {
        public ImperialMassToken(string asText, string arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
