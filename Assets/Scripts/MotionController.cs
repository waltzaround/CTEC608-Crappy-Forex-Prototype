using UnityEngine;
using System.Collections;

public class MotionController : MonoBehaviour
{
    public float movementForce = 5.0f;

    void Start()
    {
        direction = new Vector3();
    }


    // Update is called once per physical simulation frame
    void FixedUpdate()
    {
        direction.x = movementForce * Input.GetAxis("Horizontal");
        direction.z = movementForce * Input.GetAxis("Vertical");

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().AddRelativeForce(direction);
        }

    }

    private Vector3 direction;
}
