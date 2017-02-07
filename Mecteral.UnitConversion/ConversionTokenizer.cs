using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mecteral.UnitConversion
{
    public class ConversionTokenizer : IConversionTokenizer
    {
        string mAbbreviation;
        readonly List<IConversionToken> mTempTokens = new List<IConversionToken>();
        string mInput;
        public IEnumerable<IConversionToken> Tokens { get; private set; }

        public void Tokenize(string input, string abbreviation)
        {
            mAbbreviation = abbreviation;
            mTempTokens.Clear();
            mInput = RemoveWhitespaceAndEqualSign(input);
            Tokens = FillTokens();
        }

        IEnumerable<IConversionToken> FillTokens()
        {
            string number = null;
            foreach (var c in mInput)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    AddToken(number);
                    number = null;
                    AddArithmeticToken(c);
                }
                else
                {
                    number += c;
                }
            }
            if (number != null)
                AddToken(number);
            return mTempTokens;
        }

        void AddToken(string input)
        {
            if (UnitAbbreviations.ImperialAreas.Any(input.Contains) || mAbbreviation != null && !input.Any(char.IsLetter) && UnitAbbreviations.ImperialAreas.Contains(mAbbreviation))
                mTempTokens.Add(new ImperialAreaToken(input, mAbbreviation));
            else if (UnitAbbreviations.ImperialLengths.Any(input.Contains) || mAbbreviation != null && !input.Any(char.IsLetter) && UnitAbbreviations.ImperialLengths.Contains(mAbbreviation))
                mTempTokens.Add(new ImperialLengthToken(input, mAbbreviation));
            else if (UnitAbbreviations.ImperialVolumes.Any(input.Contains) || mAbbreviation != null && !input.Any(char.IsLetter) && UnitAbbreviations.ImperialVolumes.Contains(mAbbreviation))
                mTempTokens.Add(new ImperialVolumeToken(input, mAbbreviation));
            else if (UnitAbbreviations.ImperialMasses.Any(input.Contains) || mAbbreviation != null && !input.Any(char.IsLetter) && UnitAbbreviations.ImperialMasses.Contains(mAbbreviation))
                mTempTokens.Add(new ImperialMassToken(input, mAbbreviation));
            else if (UnitAbbreviations.MetricVolumes.Any(input.Contains) || mAbbreviation != null && !input.Any(char.IsLetter) && UnitAbbreviations.MetricVolumes.Contains(mAbbreviation))
                mTempTokens.Add(new MetricVolumeToken(input, mAbbreviation));
            else if (UnitAbbreviations.MetricMasses.Any(input.Contains) || mAbbreviation != null && !input.Any(char.IsLetter) && UnitAbbreviations.MetricMasses.Contains(mAbbreviation))
                mTempTokens.Add(new MetricMassToken(input, mAbbreviation));
            else if (UnitAbbreviations.MetricAreas.Any(input.Contains) || mAbbreviation != null && !input.Any(char.IsLetter) && UnitAbbreviations.MetricAreas.Contains(mAbbreviation))
                mTempTokens.Add(new MetricAreaToken(input, mAbbreviation));
            else if (UnitAbbreviations.MetricLengths.Any(input.Contains) || mAbbreviation != null && !input.Any(char.IsLetter) && UnitAbbreviations.MetricLengths.Contains(mAbbreviation))
                mTempTokens.Add(new MetricLengthToken(input, mAbbreviation));
            else
                throw new InvalidExpressionException($"The input didnt define which system it used. Part that threw Exception: {input} ");
        }

        void AddArithmeticToken(char input)
        {
            mTempTokens.Add(new ConversionOperatorToken(input));
        }

        static string RemoveWhitespaceAndEqualSign(string input)
        {
            return input.TakeWhile(c => c != '=')
                .Where(c => !char.IsWhiteSpace(c))
                .Aggregate("", (current, c) => current + c);
        }
    }
}