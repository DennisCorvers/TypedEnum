# TypedEnum
An Enum type that can only use the pre-defined values from the specified Enum
It can be used when the user wants to be certain that pre-specified Enum values are used, and no deviation from this set is possible in any case. Naturally, the TypedEnum does not work when using an Enum as flags.

## Example
```C#
public class Appointment
{
    private TypedEnum<WeekDays> weekday;

    // Operator overload to easily compare the TypedEnum to its underlying type
    public bool IsMidWeek
        => weekday == WeekDays.Wednesday;

    // No validation is required. A weekday will always be one of the valid 7 days.
    public Appointment(TypedEnum<WeekDays> weekday)
    {
        this.weekday = weekday;
    }

    public bool IsWorkDay()
    {
        // No false positives are returned. We know that the remaining days must be Monday -> Friday.
        // The inverse of this function can thus be used to determine is the appointment is in a weekend.
        return weekday.EnumValue switch
        {
            WeekDays.Saturday or WeekDays.Sunday => false,
            _ => true,
        };
    }

    // A valid string representation will always occur, since the enum must be one of 7 values.
    public override string ToString()
    {
        return $"This appointment is on {weekday}";
    }
}


public enum WeekDays
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}
```

## Performance
This section aims to compare the performance of TypedEnum against the built in Enum type.
Below shows the time it takes to instantiate an instance of TypedEnum vs assigning a regular Enum. Although the difference is quite large, the time it takes to instantiate a TypedEnum instance is still neglibible. Enum assignment is considered indistinguishable from an empty method because it takes a single IL operation to assign. 

|      Method |      Mean |     Error |    StdDev |    Median | Allocated |
|------------ |----------:|----------:|----------:|----------:|----------:|
| Constructor | 0.0036 ns | 0.0065 ns | 0.0054 ns | 0.0014 ns |         - |
|  EnumAssign | 2.9652 ns | 0.0034 ns | 0.0028 ns | 2.9644 ns |         - |

### Comparison to regular Enum
Choosing a common use-case as example (such as the use in lookup tables/dictionaries), we can see that there is no performance difference between the TypedEnum and the Enum. The only extra performance cost comes when initially creating a TypedEnum instance.

|           Method |       Mean |     Error |    StdDev | Allocated |
|----------------- |-----------:|----------:|----------:|----------:|
|      HashSetEnum | 13.1786 ns | 0.0341 ns | 0.0319 ns |         - |
| HashSetTypedEnum | 13.0388 ns | 0.1278 ns | 0.1196 ns |         - |

### Improvements over System.Enum
The generic CompareTo method has been improved with the use of IL generation. This method uses the underlying enum type to compare and thus avoids boxing of the values.

The ToString performance has also been improved by caching the possible string representations of the enum. 

|          Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
|---------------- |----------:|----------:|----------:|-------:|----------:|
| NativeCompareTo |  8.950 ns | 0.0600 ns | 0.0561 ns | 0.0057 |      48 B |
|     ILCompareTo |  2.571 ns | 0.0063 ns | 0.0049 ns |      - |         - |
|  NativeToString | 21.900 ns | 0.0475 ns | 0.0397 ns | 0.0029 |      24 B |
|  CachedToString |  4.263 ns | 0.0183 ns | 0.0171 ns |      - |         - |
