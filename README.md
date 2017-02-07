# UnitConversion

The Library is an easy to use system for conversions between the metrical and imperial systems.

The Library supports basic operations ( + , - , * , / ).

The Library supports mixed input: ``12m + 13ft`` and throws an exception if the systems can't be calculated ``12l + 13m``.

##Usage:
```c#
            `ConversionFacade.Convert(input, abbreviation, toMetric)`
```
string is the input which needs to be formatted like => `12m+12km+23ft`
"number + abbreviation"

bool defines if the system is converted into the metrical or the imperial system => true = metrical, false = imperial

abbreviation defines the standard used unit => abbreviation ="m" => 12+12+12cm= 24,12m

```c#
            ConversionFacade.Convert(input, toMetric)
```
####Or:
```c#
            ConversionFacade.Convert(input, abbreviation, toMetric)
```
The Library was built with
##With IOC:

###Registration:
```c#
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
``` 
###usage Example:
```c#
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
```
####Or:         
```c#
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
```   
##UnitAbbreviations:

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
