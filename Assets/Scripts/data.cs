using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class data : MonoBehaviour
{
    public int counter;
    public GameObject spawnTemplate;
    List<GameObject> cubes = new List<GameObject>();    
    // public Material positiveChange;
    // public Material negativeChange;

    private bool dataLoaded;
    private WWW currencyWWW;
    //public Dictionary<string, CurrencyData> currencydatas = new Dictionary<string, CurrencyData>();

    public void Awake()
    {
        // Starting in 2 seconds.
        // data will be called every 4 seconds
        InvokeRepeating("dataGet", 2, 1);
        counter = 0;



        
    }

    public void dataGet()
    { 
        dataLoaded = false;
        string currencyURL = "http://finance.yahoo.com/webservice/v1/symbols/allcurrencies/quote?format=json";
        currencyWWW = new WWW(currencyURL);
    }

    public void Update()
    {
        

        if (dataLoaded || currencyWWW == null || !currencyWWW.isDone) return;

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

                            
                            CurrencyData data = new CurrencyData(name, price, volume);
                            CreateDataObject(data);




                            //Debug.Log("Found : " + name + " = " + float.Parse(price) + " at " + float.Parse(volume) + " sold");
                        });
                    });
                }
                dataLoaded = true;
            });
        }, delegate (string list) { //"name" will be equal to the name of the missing field.  In this case, "hits"
            Debug.LogWarning("no data found");
        });
    }

    void CreateDataObject(CurrencyData data)
    {
        GameObject spawnedObject;
        cubes.Add(Instantiate(spawnTemplate, transform.position, transform.rotation) as GameObject); 
        //spawnedObject = Instantiate(spawnTemplate, transform.position, transform.rotation) as GameObject;
       // spawnedObject.GetComponent<cubeBehaviour>().SetData(data);
        cubes[counter].GetComponent<cubeBehaviour>().SetData(data);
        counter++;
        Debug.Log("WE HAVE " + counter + " CUBES");
        if(counter > 2000000)
        {
            foreach(GameObject cube in cubes) {
                Destroy(cube);
            }
            // for(int i = 0; i < cubes.Count; i ++) { cubes.}
            cubes.Clear();
            counter = 0;
        }

    }
    }

