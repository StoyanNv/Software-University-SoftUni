using System;

public class WeeklyEntry : IComparable<WeeklyEntry>
{
    public WeeklyEntry(string weekDay, string notes)
    {
        this.WeekDay = (WeekDay)Enum.Parse(typeof(WeekDay), weekDay);
        this.Notes = notes;
    }
    public WeekDay WeekDay { get; }
    public string Notes { get; }

    public int CompareTo(WeeklyEntry other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }
        if (ReferenceEquals(null, other))
        {
            return 1;
        }
        var weekDayComparison = this.WeekDay.CompareTo(other.WeekDay);
        if (weekDayComparison != 0)
        {
            return weekDayComparison;
        }
        return string.Compare(this.Notes, other.Notes, StringComparison.Ordinal);
    }

    public override string ToString()
    {
        return $"{this.WeekDay} - {this.Notes}";
    }
}