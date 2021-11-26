using TypedEnum;

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

