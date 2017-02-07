namespace Mecteral.UnitConversion
{
    public interface IConversionFacade
    {
        string ConvertUnits(string input, bool toMetric);
        string ConvertUnits(string input, string abbreviation, bool toMetric);
    }
}