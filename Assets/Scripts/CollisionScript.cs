using UnityEngine;
using System.Collections;

public class CollisionScript : MonoBehaviour
{


    void Start()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("stuff");
       if(collision.gameObject.tag == "cube") { Destroy(collision.gameObject); }

    }
}