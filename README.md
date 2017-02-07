# UnitConversion

The UnitConverter is a easy to use system for conversions between the metrical and imperial systems.

The Unitconverter supports basic Operations ( + , - , * , / ).
The UnitConverter support mixed input: " 12m + 13ft " and throws an exception if the systems cant be calculated (" 12l + 13m ").

Usage:

The Unitconverter can be used with or without IOC.

the string is the input which needs to be formatted like => 12m+12km+23ft
"number + abbreviation"

The bool defines if the system is converted into the metrical or the imperial system

True = metrical, False = imperial

abbreviation defines the standard used unit, so you dont have to type the unit every time anew. => abbreviation ="m" => 12+12= 24m

Without IOC:

            ConversionFacade.Convert(input, toMetric)
Or:

            ConversionFacade.Convert(input, abbreviation, toMetric)

With IOC:

Registration:

            builder.RegisterType<ConversionTokenizer>().As<IConversionTokenizer>();
            builder.RegisterType<ConversionModelBuilder>().As<IConversionModelBuilder>();
            builder.RegisterType<ReadableOutputCreator>().As<IReadableOutputCreator>();
            builder.RegisterType<ConversionFacade>().As<IConversionFacade>();
            builder.RegisterType<UnitConverter>()
                .As<IUnitConverter>()
                .WithParameter((parameter, context) => parameter.ParameterType == typeof(Func<bool, IConverters>),
                    (parameter, context) =>
                    {
                        var cc = context.Resolve<IComponentContext>();
                        Func<bool, IConverters> result =
                            toMetric =>
                                toMetric
                                    ? (IConverters)cc.Resolve<IImperialToMetricConverter>()
                                    : cc.Resolve<IMetricToImperialConverter>();
                        return result;
                    });
            builder.RegisterType<ImperialToMetricConverter>().As<IImperialToMetricConverter>();
            builder.RegisterType<MetricToImperialConverter>().As<IMetricToImperialConverter>();
            
usage Example:
            
        UsageClass
        {
            readonly Func<IConversionFacade> mConversionFactory;
            
            public UseConversion(string input, bool toMetric)
            {
                        var conversion = mConversionFactory();
                        conversion.ConvertUnits(input, toMetric);
            }
            
            public Constructor(Func<IConversionFacade> conversionFactory)
            {
                        mConversionFactory = conversionFactory;
            }
        }
Or:         

        UsageClass
        {
            readonly Func<IConversionFacade> mConversionFactory;
            
            public UseConversion(string input, string abbreviation, bool toMetric)
            {
                        var conversion = mConversionFactory();
                        conversion.ConvertUnits(input, abbreviation, toMetric);
            }
            
            public Constructor(Func<IConversionFacade> conversionFactory)
            {
                        mConversionFactory = conversionFactory;
            }
        }
        
UnitAbbreviations:

Millimeters = "mm";
Centimeters = "cm";
Meters = "m";
Kilometers = "km";

Milligram = "mg";
Gram = "g";
Kilogram = "kg";
Ton = "t";

Milliliters = "ml";
Centiliters = "cl";
Liters = "l";
Hectoliters = "hl";

Squaremillimeters = "qmm";
Squarecentimeters = "qcm";
Sqauremeters = "qm";
Squarekilometers = "qkm";
Hectas = "ha";

Though = "th";
Inch = "in";
Foot = "ft";
Yard = "yd";
Chain = "ch";
Furlong = "fur";
Mile = "mI";
League = "lea";
Fathom = "ftm";

Squarefoot = "sft";
Perch = "perch";
Rood = "rood";
Acre = "acre";

FluidOunce = "floz";
Gill = "gi";
Pint = "pt";
Quart = "qt";
Gallon = "gal";

Grain = "gr";
Drachm = "dr";
Ounce = "oz";
Pound = "lb";
Stone = "st";
HundredWeight = "cwt";
ImperialTon = "it";
