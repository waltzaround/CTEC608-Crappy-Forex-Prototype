using UnityEngine;
using System.Collections;

public class cubeBehaviour : MonoBehaviour {
    TextMesh text;
    public string values;

    // Use this for initialization
    public void SetData(CurrencyData data)
    {

        text = gameObject.GetComponentInChildren<TextMesh>();
        this.data = data;
        speed = (Random.insideUnitSphere) * 0.02f;
        DataUpdated();
        values = data.GetName() + '\n' + data.GetPrice() + '\n' + data.Getvolume();

        text.text = values;   
    }

    public void Update()
    {
        transform.Translate(speed);
        text.text = values;
    }

	public void DataUpdated()
    {
        Debug.Log("Data updated on " + data.GetName());
        // here is wewh you cxhange the look of the obejct
        transform.name = data.GetName();
        
	}

    private CurrencyData data;
    private Vector3 speed;
}
