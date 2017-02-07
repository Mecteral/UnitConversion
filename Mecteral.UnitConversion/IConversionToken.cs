namespace Mecteral.UnitConversion
{
    public interface IConversionToken
    {
        void Accept(IConversionTokenVisitor visitor);
    }
}
