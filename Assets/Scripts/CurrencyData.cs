using UnityEngine;
using System.Collections;

public class CurrencyData : MonoBehaviour {

    public string theName;
    public string price;
    public string volume;

    // Use this for initialization
   public void Initialise(string name, string price, string volume)
    {
        this.theName = name;
        this.price = price;
        this.volume = volume;
    }
	
    public string GetName() { return theName; }
    public string GetPrice() { return price; }
    public string Getvolume() { return volume; }
}
