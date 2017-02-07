using System;

namespace Mecteral.UnitConversion
{
    /// <summary>
    /// 
    /// </summary>
    public class ConversionFacade : IConversionFacade
    {
        readonly IReadableOutputCreator mReadableOutputCreator;
        readonly IUnitConverter mUnitConverter;
        readonly IConversionTokenizer mConversionTokenizer;
        readonly IConversionModelBuilder mConversionModelBuilder;
        /// <summary>
        /// Constructor for IOC
        /// </summary>
        /// <param name="readOutputCreator"></param>
        /// <param name="unitConverter"></param>
        /// <param name="conversionTokenizer"></param>
        /// <param name="conversionModelBuilder"></param>
        public ConversionFacade(IReadableOutputCreator readOutputCreator, IUnitConverter unitConverter, IConversionTokenizer conversionTokenizer, IConversionModelBuilder conversionModelBuilder)
        {
            mReadableOutputCreator = readOutputCreator;
            mUnitConverter = unitConverter;
            mConversionTokenizer = conversionTokenizer;
            mConversionModelBuilder = conversionModelBuilder;
        }
        /// <summary>
        /// IOC needed! Takes string as input and bool if it should be converted to the metric system
        /// </summary>
        /// <param name="input"></param>
        /// <param name="toMetric"></param>
        /// <returns></returns>
        public string ConvertUnits(string input, bool toMetric)
        {
            mConversionTokenizer.Tokenize(input, null);
            var converted = UseUnitConverter(CreateConversionInMemoryModel(mConversionTokenizer), toMetric);
            return mReadableOutputCreator.MakeReadable(converted);
        }
        /// <summary>
        /// IOC needed! Takes string as input, unit as string for defaultunit and bool if it should be converted to the metric system
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abbreviation"></param>
        /// <param name="toMetric"></param>
        /// <returns></returns>
        public string ConvertUnits(string input, string abbreviation, bool toMetric)
        {
            mConversionTokenizer.Tokenize(input, abbreviation);
            var converted = UseUnitConverter(CreateConversionInMemoryModel(mConversionTokenizer), toMetric);
            return mReadableOutputCreator.MakeReadable(converted);
        }
        /// <summary>
        /// Takes string as input and bool if it should be converted to the metric system
        /// </summary>
        /// <param name="input"></param>
        /// <param name="toMetric"></param>
        /// <returns></returns>
        public static string Convert(string input, bool toMetric)
        {
            Func<bool, IConverters> conversionFactory = b => b ? (IConverters)new ImperialToMetricConverter() : new MetricToImperialConverter();
            var tokenizer = new ConversionTokenizer();
            tokenizer.Tokenize(input, null);
            var modelBuilder = new ConversionModelBuilder();
            var model = modelBuilder.BuildFrom(tokenizer.Tokens);
            var converter = new UnitConverter(conversionFactory);
            var converted = converter.Convert(model, toMetric);
            var readableOutputCreator = new ReadableOutputCreator();
            return readableOutputCreator.MakeReadable(converted);
        }
        /// <summary>
        /// Takes string as input, unit as string for defaultunit and bool if it should be converted to the metric system
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abbreviation"></param>
        /// <param name="toMetric"></param>
        /// <returns></returns>
        public static string Convert(string input, string abbreviation, bool toMetric)
        {
            Func<bool, IConverters> conversionFactory = b => b ? (IConverters)new ImperialToMetricConverter() : new MetricToImperialConverter();
            var tokenizer = new ConversionTokenizer();
            tokenizer.Tokenize(input, abbreviation);
            var modelBuilder = new ConversionModelBuilder();
            var model = modelBuilder.BuildFrom(tokenizer.Tokens);
            var converter = new UnitConverter(conversionFactory);
            var converted = converter.Convert(model, toMetric);
            var readableOutputCreator = new ReadableOutputCreator();
            return readableOutputCreator.MakeReadable(converted);
        }

        IConversionExpression CreateConversionInMemoryModel(IConversionTokenizer token) => mConversionModelBuilder.BuildFrom(token.Tokens);

        IConversionExpressionWithValue UseUnitConverter(IConversionExpression expression, bool toMetric) => mUnitConverter.Convert(expression, toMetric);
    }
}
