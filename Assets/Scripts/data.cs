using UnityEngine;
using System.Collections;

public class data : MonoBehaviour
{
    public Material positiveChange;
    public Material negativeChange;

    private bool dataLoaded;
    private WWW currencyWWW;

    public void Awake()
    {
        // Starting in 2 seconds.
        // data will be called every 4 seconds
        InvokeRepeating("dataGet", 2, 4);
        
    }

    public void dataGet()
    { 
    dataLoaded = false;
        string currencyURL = "http://finance.yahoo.com/webservice/v1/symbols/allcurrencies/quote?format=json";
    currencyWWW = new WWW(currencyURL);
    }

    public void Update()
    {


  

        if (dataLoaded || !currencyWWW.isDone) return;

        // let's look at the results
        JSONObject j = new JSONObject(currencyWWW.text);

        j.GetField("list", delegate (JSONObject list) {
            list.GetField("resources", delegate (JSONObject resources) {
                foreach (JSONObject entry in resources.list)
                {
                    entry.GetField("resource", delegate (JSONObject resource) {
                        resource.GetField("fields", delegate (JSONObject fields) {
                            string name;
                            string price;
                            string volume;
                            fields.GetField(out price, "price", "-1");
                            fields.GetField(out name, "name", "NONAME");
                            fields.GetField(out volume, "volume", "NOVOLUME");
                            Debug.Log("Found : " + name + " = " + float.Parse(price) + " at " + float.Parse(volume) + " sold");
                        });
                    });
                }
                dataLoaded = true;
            });
        }, delegate (string list) { //"name" will be equal to the name of the missing field.  In this case, "hits"
            Debug.LogWarning("no data found");
        });
    }


}