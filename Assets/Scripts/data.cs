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
                            fields.GetField(out price, "price", "-1");
                            fields.GetField(out name, "name", "NONAME");
                            Debug.Log("Found : " + name + " = " + float.Parse(price));
                        });
                    });
                }
                dataLoaded = true;
            });
        }, delegate (string list) { //"name" will be equal to the name of the missing field.  In this case, "hits"
            Debug.LogWarning("no data found");
        });
    }

    //access data (and print it)
    void accessData(JSONObject obj)
    {
        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];
                    Debug.Log(key);
                    accessData(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                foreach (JSONObject j in obj.list)
                {
                    accessData(j);
                }
                break;
            case JSONObject.Type.STRING:
                //Debug.Log(obj.resources.resource.name);
                break;
            case JSONObject.Type.NUMBER:
                //Debug.Log(obj.resources.resource.price);
                break;

        }
    }
}