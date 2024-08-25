public class Stamp
{
    public string Country { get; set; }
    public decimal FaceValue { get; set; }
    public int YearOfIssue { get; set; }
    public int Circulation { get; set; }
    public string Features { get; set; }

    public Stamp(string country, decimal faceValue, int yearOfIssue, int circulation, string features)
    {
        Country = country;
        FaceValue = faceValue;
        YearOfIssue = yearOfIssue;
        Circulation = circulation;
        Features = features;
    }

    public override string ToString()
    {
        return $"{Country} - Face Value: {FaceValue}, Year: {YearOfIssue}, Circulation: {Circulation}, Features: {Features}";
    }
}
