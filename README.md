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

The Library was built with extension in mind
##If you use IOC:

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

####Metrical Lenght     ####Metrical Mass       ####Metrical Volume     ####Metrical Area
|Unit| Abbreviation|    |Unit| Abbreviation|    |Unit| Abbreviation|    |Unit| Abbreviation|
|----|---|---|---|---|  |----|---|---|---|---|  |----|---|---|---|---|  |----|---|---|---|---|
| Millimeters | "mm"|   | Milligram | "mg"|     | Milliliters | "ml"|   | Squaremillimeters | "qmm"|
| Centimeters |"cm"|    | Gram |"g"|            | Centiliters |"cl"|    | Squarecentimeters |"qcm"|
| Meters |"m"|          | Kilogram |"kg"|       | Liters |"l"|          | Sqauremeters |"qm"|
|  Kilometers  | "km"  ||  Ton  | "t"  |        |  Hectoliters|"hl"|    |  Squarekilometers  | "qkm"  |
         |||||||||                                                               |  Hectas  | "ha"  |

####Imperial Length
|Unit| Abbreviation|
|----|---|---|---|---|
| Though | "th"|
| Inch |"in"|
| Foot |"ft"|
|  Yard  | "yd"  |
|  Chain  | "ch"  |
| Furlong |"fur"|
| Mile |"mI"|
|  League  | "lea"  |
|  Fathom  | "ftm"  |

####Imperial Area
|Unit| Abbreviation|
|----|---|---|---|---|
| Squarefoot | "sft"|
| Perch |"perch"|
| Rood |"rood"|
|  Acre  | "acre"  |

####Imperial Volume
|Unit| Abbreviation|
|----|---|---|---|---|
| FluidOunce | "floz"|
| Gill |"gi"|
| Pint |"pt"|
|  Quart  | "qt"  |
|  Gallon  | "gal"  |

####Imperial Mass
|Unit| Abbreviation|
|----|---|---|---|---|
| Grain | "gr"|
| Drachm |"dr"|
| Ounce |"oz"|
|  Pound  | "lb"  |
|  Stone  | "st"  |
|  HundredWeight  | "cwt"  |
|  ImperialTon  | "it"  |
