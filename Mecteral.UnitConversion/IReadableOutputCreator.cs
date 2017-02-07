namespace Mecteral.UnitConversion
{
    public interface IReadableOutputCreator
    {
        string MakeReadable(IConversionExpressionWithValue expression);
    }
}