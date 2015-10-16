using UnityEngine;
using System.Collections;

public class IoTSkybox : MonoBehaviour
{
    public Material clearSky;
    public Material cloudySky;

    IEnumerator AdjustSkyToWeather()
    {
        while (true)
        {
            string weatherUrl = "http://api.openweathermap.org/data/2.5/weather?zip=2000,au";

            WWW weatherWWW = new WWW(weatherUrl);
            yield return weatherWWW;

            JSONObject tempData = new JSONObject(weatherWWW.text);

            JSONObject weatherDetails = tempData["weather"];
            string WeatherType = weatherDetails[0]["main"].str;

            if (WeatherType == "Clear")
            {
                RenderSettings.skybox = clearSky;
            }
            else if (WeatherType == "Clouds" || WeatherType == "Rain")
            {
                RenderSettings.skybox = cloudySky;
            }

            yield return new WaitForSeconds(60);
        }
    }

    void Start()
    {
        StartCoroutine(AdjustSkyToWeather());
    }
}

string encodedString = "{\"field1\": 0.5,\"field2\": \"sampletext\",\"field3\": [1,2,3]}";
JSONObject j = new JSONObject(encodedString);
accessData(j);
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
            Debug.Log(obj.str);
            break;
        case JSONObject.Type.NUMBER:
            Debug.Log(obj.n);
            break;
        case JSONObject.Type.BOOL:
            Debug.Log(obj.b);
            break;
        case JSONObject.Type.NULL:
            Debug.Log("NULL");
            break;

    }
}