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

####Metrical Lenght & Mass    
|Unit| Abbreviation|    |Unit| Abbreviation|    
|----|---|---|---|---|  |----|---|---|---|---|  
| Millimeters | "mm"|   | Milligram | "mg"|     
| Centimeters |"cm"|    | Gram |"g"|            
| Meters |"m"|          | Kilogram |"kg"|      
|  Kilometers  | "km"  ||  Ton  | "t"  |    

####Metrical Volume & Area
|Unit| Abbreviation|    |Unit| Abbreviation|
|----|---|---|---|---|  |----|---|---|---|---|
| Milliliters | "ml"|   | Squaremillimeters | "qmm"|
| Centiliters |"cl"|    | Squarecentimeters |"qcm"|
| Liters |"l"|          | Sqauremeters |"qm"|
|  Hectoliters|"hl"|    |  Squarekilometers  | "qkm"  |

####Imperial Length & Area
|Unit| Abbreviation|    |Unit| Abbreviation|
|----|---|---|---|---|  |----|---|---|---|---|
| Though | "th"|        | Squarefoot | "sft"|
| Inch |"in"|           | Perch |"perch"|
| Foot |"ft"|           | Rood |"rood"|
|  Yard  | "yd"  |      |  Acre  | "acre"  |
|  Chain  | "ch"  |     
| Furlong |"fur"|       
| Mile |"mI"|           
|  League  | "lea"  |   
|  Fathom  | "ftm"  |   

####Imperial Mass & Volume
|Unit| Abbreviation|    |Unit| Abbreviation|  
|----|---|---|---|---|  |----|---|---|---|---|  
| Grain | "gr"|         | FluidOunce | "floz"|  
| Drachm |"dr"|         | Gill |"gi"|      
| Ounce |"oz"|          | Pint |"pt"|     
|  Pound  | "lb"  |     |  Quart  | "qt"  |    
|  Stone  | "st"  |     |  Gallon  | "gal"  |  
|HundredWeight|"cwt"|   
|  ImperialTon  | "it"| 
