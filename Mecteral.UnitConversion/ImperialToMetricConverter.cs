﻿using System.Data;

namespace Mecteral.UnitConversion
{
    public class ImperialToMetricConverter : IImperialToMetricConverter
    {
        public IConversionExpressionWithValue Convert(IConversionExpression expression)
        {
            if (expression is ImperialLengthExpression)
            {
                var tempExpression = (ImperialLengthExpression)expression;
                return new MetricLengthExpression { Value = tempExpression.Value * (decimal) 0.3048 };
            }
            else if (expression is ImperialAreaExpression)
            {
                var tempExpression = (ImperialAreaExpression)expression;
                return new MetricAreaExpression { Value = tempExpression.Value * (decimal) 0.0929 };
            }
            else if (expression is ImperialMassExpression)
            {
                var tempExpression = (ImperialMassExpression)expression;
                return new MetricMassExpression { Value = tempExpression.Value  * (decimal) 453.59237 };
            }
            else if (expression is ImperialVolumeExpression)
            {
                var tempExpression = (ImperialVolumeExpression)expression;
                return new MetricVolumeExpression { Value = tempExpression.Value * (decimal) 0.0284130625 };
            }
            else
            {
                throw new InvalidExpressionException();
            }
        }
    }
}
