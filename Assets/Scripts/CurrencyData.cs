using UnityEngine;
using System.Collections;

public class CurrencyData
{
    public GameObject gameObject;

    // Use this for initialization
   public CurrencyData(string name, string price, string volume)
    {
        this.name = name;
        this.price = price;
        this.volume = volume;
        this.gameObject = null;
    }

    public void Update(string price, string volume)
    {
        this.price = price;
        this.volume = volume;
        if ( gameObject != null )
        {
            cubeBehaviour cb = gameObject.GetComponent<cubeBehaviour>();
            if (cb != null)
            {
                cb.DataUpdated();
            }
        }
    }
	
    public string GetName() { return name; }
    public string GetPrice() { return price; }
    public string Getvolume() { return volume; }

    private string name;
    private string price;
    private string volume;

}
