using System.Collections.Generic;

public class Philatelist
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string ContactDetails { get; set; }
    public List<Stamp> RareStamps { get; set; }

    public Philatelist(string name, string country, string contactDetails)
    {
        Name = name;
        Country = country;
        ContactDetails = contactDetails;
        RareStamps = new List<Stamp>();
    }

    public void AddRareStamp(Stamp stamp)
    {
        RareStamps.Add(stamp);
    }

    public override string ToString()
    {
        return $"Name: {Name}, Country: {Country}, Contact: {ContactDetails}, Number of Stamps: {RareStamps.Count}";
    }
}
