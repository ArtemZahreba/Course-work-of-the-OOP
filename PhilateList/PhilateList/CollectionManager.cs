using System.Collections.Generic;
using System.Linq;

public class CollectionManager
{
    public List<Philatelist> Philatelists { get; set; }

    public CollectionManager()
    {
        Philatelists = new List<Philatelist>();
    }

    public void AddPhilatelist(Philatelist philatelist)
    {
        Philatelists.Add(philatelist);
    }

    public void UpdatePhilatelist(string name, Philatelist updatedPhilatelist)
    {
        var philatelist = Philatelists.FirstOrDefault(p => p.Name == name);
        if (philatelist != null)
        {
            philatelist.Name = updatedPhilatelist.Name;
            philatelist.Country = updatedPhilatelist.Country;
            philatelist.ContactDetails = updatedPhilatelist.ContactDetails;
            philatelist.RareStamps = updatedPhilatelist.RareStamps;
        }
    }

    public void RemovePhilatelist(string name)
    {
        var philatelist = Philatelists.FirstOrDefault(p => p.Name == name);
        if (philatelist != null)
        {
            Philatelists.Remove(philatelist);
        }
    }

    public Philatelist GetPhilatelistByIndex(int index)
    {
        if (index >= 0 && index < Philatelists.Count)
        {
            return Philatelists[index];
        }
        return null;
    }

    public void ViewPhilatelists()
    {
        for (int i = 0; i < Philatelists.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Philatelists[i]}");
        }
    }
    public Philatelist GetPhilatelistByName(string name)
    {
        return Philatelists.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

}
