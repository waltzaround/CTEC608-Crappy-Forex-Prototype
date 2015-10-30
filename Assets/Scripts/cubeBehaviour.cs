using UnityEngine;
using System.Collections;

public class cubeBehaviour : MonoBehaviour {

    // Use this for initialization
    public void SetData(CurrencyData data)
    {
        this.data = data;
        speed = (Random.insideUnitSphere) * 0.02f;
        DataUpdated();
    }

    public void Update()
    {
        transform.Translate(speed);
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
