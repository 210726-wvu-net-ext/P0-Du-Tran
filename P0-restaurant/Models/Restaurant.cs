
namespace Models;
public class Restaurant
{
    public Restaurant(){}

    public Restaurant(string name)
    {
        this.Name = name;
    }

    public Restaurant(int id, string name, string location, string contact ) : this(name)
    {
        this.Id = id;  
        this.Location = location;
        this.Contact = contact; 
    }
    public int Id {get; set;}
    public string Name {get; set;}
    public string Location {get; set;}
    public string Contact {get; set;}
}
