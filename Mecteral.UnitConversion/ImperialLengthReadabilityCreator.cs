﻿using System.Collections.Generic;

namespace Mecteral.UnitConversion
{
    public class ImperialLengthReadabilityCreator : AnImperialReadabilityCreator
    {
        public string MakeReadable(IConversionExpressionWithValue expression)
        {
            IList<decimal> conversionFactors = new List<decimal>
            {
                ConversionFactors.FathomToFeet,
                ConversionFactors.LeagueToFeet,
                ConversionFactors.MileToFeet,
                ConversionFactors.FurlongToFeet,
                ConversionFactors.ChainToFeet,
                ConversionFactors.YardToFeet,
                ConversionFactors.MultiplicationByOne,
                ConversionFactors.InchToFeet,
                ConversionFactors.ThouToFeet
            };
            IList<string> abbreviations = new List<string>
            {
                UnitAbbreviations.Fathom,
                UnitAbbreviations.League,
                UnitAbbreviations.Mile,
                UnitAbbreviations.Furlong,
                UnitAbbreviations.Chain,
                UnitAbbreviations.Yard,
                UnitAbbreviations.Foot,
                UnitAbbreviations.Inch,
                UnitAbbreviations.Though
            };
            return CreateFrom(expression.Value, UnitAbbreviations.Foot, conversionFactors, abbreviations);
        }
    }
}