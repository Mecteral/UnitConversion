# UnitConversion

The Library is an easy to use system for conversions between the metrical and imperial systems.

The Library supports basic operations ( + , - , * , / ).

The Library supports mixed input: ``12m + 13ft`` and throws an exception if the systems can't be calculated ``12l + 13m``.

##Usage:
```c#
            ConversionFacade.Convert(input, abbreviation, toMetric)
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

##The Library was built with extension in mind.

The Library contains 3 key elements which can be extended for further functionality:

###ConversionTokenizer

```c#
            ConversionTokenizer.Tokenize(input, abbreviation);
```
Tokenize takes in 2 string:
input => the input like: `12m+13km`
abbreviation => the unitAbbreviation `"km"`

If abbreviation is set to null every number need its own abbreviation.

The Tokenizer splits the string into parts and creates `ConversionTokens` out of them, which are stored in an IEnumerable. This IEnumerable is used to build an InMemoryModel with `ConversionModelBuilder` for further calculation.

To extend the functionality you simply have to create new ConversionToken classes which inherit from `IConversionToken` and use `AConversionToken` for creation. Then you have to adjust the class `Unitabbreviations` and `ConversionFactors` and the Dictionary in `AConversionToken`.

####Example:

#####Lets assume metric length is not supported yet :
Lets create the class `MetricLengthToken`:
```c#
    public class MetricLengthToken : AConversionTokens, IConversionToken
    {
        public MetricLengthToken(string asText, string arg) : base(asText, arg) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
```
now we have to adjust our visitorpattern in `IConversionTokenVisitor`:
```c#
        void Visit(MetricLengthToken metricLengthToken);
```

now we adjust the `UnitAbbreviations`:
######in this step you can adjust even more abbreviations to extend the functionality.

```c#
        public const string Millimeters = "mm";
        public const string Centimeters = "cm";
        public const string Meters = "m";
        public const string Kilometers = "km";
```


now we adjust  the `ConversionFactors` where needed:

```c#
        public const decimal MetricDivisionOneThousand = 1.0E-3M;
        public const decimal MetricDivisionOneHundred = 1.0E-2M;
        public const decimal MultiplicationByOne = 1;
        public const decimal MetricMultiplicationOneHundred = 1.0E2M;
        public const decimal MetricMultiplicationOneThousand = 1.0E3M;
```

now we adjust the Dictionary in `AConversionToken`:

```c#
            {UnitAbbreviations.Millimeters, ConversionFactors.MetricDivisionOneThousand},
            {UnitAbbreviations.Centimeters, ConversionFactors.MetricDivisionOneHundred},
            {UnitAbbreviations.Meters, ConversionFactors.MultiplicationByOne},
            {UnitAbbreviations.Kilometers, ConversionFactors.MetricMultiplicationOneThousand},
```

now we adjust the `ConversionTokenizer` so it creates the newly defined Token:

```c#
            else if (UnitAbbreviations.MetricLengths.Any(input.Contains) || mAbbreviation != null && !input.Any(char.IsLetter) && UnitAbbreviations.MetricLengths.Contains(mAbbreviation))
                mTempTokens.Add(new MetricLengthToken(input, mAbbreviation));
```

now we have a new token in our IEnumerable, which we pass to our ModelBuilder:

###ConversionModelBuilder

```c#
            ConversionModelBuilder.BuildFrom(IEnumerable<IConversionToken> tokens)
```
The ModelBuilder takes in the IEnumerable which we created in our Tokenizer and creates an InMemoryModel out of it, which we use to calculate our result.


First we have to define a new `ConversionExpression` for our length example:

```c#
    public class ImperialLengthExpression : AnConversionExpression
    {
        public override decimal Value { get; set; }
        public override void Accept(IConversionExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"{Value}";
        public override void ReplaceChild(IConversionExpression oldChild, IConversionExpression newChild)
        {
            throw new InvalidOperationException();
        }
    }
```

The `ImperialLengthExpression` inherits from `AnConversionExpression` and holds:
Value => which is the calculated value of our `MetricLengthToken`.
Visitor => which is used to visit the expression later on in the process.
ReplaceChild => is only used with operators, as those hold a child `left` and a child `right`.

here we adjust our visitorpattern again for the expressions in `IConversionExpressionVisitor`:
```c#
            void Visit(MetricLengthExpression metricLengthExpression);
```

now we adjust the `ConversionModelBuilder`:

```c#
        public void Visit(MetricLengthToken metricLengthToken)
        {
            mCurrent = new MetricLengthExpression {Value = metricLengthToken.Value};
            if (mCurrentOperation != null)
                FillOperation();
        }
```
This process is always the same for new expressions which are no operators.

Now we have a working model of our expressions, which we use to calculate and convert our input.
We do this in the class `UnitConverter`:

###UnitConverter

```c#
            UnitConverter.Convert(expression, toMetric);
```
The `UnitConverter` takes in our expression that we just created and a bool value, which defines if the convert into the metric or the imperial system.

#####In UnitConverter we have to adjust the methods:

`CalculateIfNoConversionIsNeeded`,
`CreateReplacementIfBothSidesOfTheOperationNeedToBeConvertedForImperial`,
`CreateReplacementIfBothSidesOfTheOperationNeedToBeConvertedForMetric`,
`CreateReplacement`

In those methods we simply follow the already given structure.

Now our expression is calculated and reduced to one single expression which holds the value we want to output.

If you want to make the created value better readable for the user you have to adjust the `ReadableOutputCreator`:


```c#
            ReadableOutputCreator.MakeReadable(expression)
```
It takes in the expression and return us a easy to read string.
