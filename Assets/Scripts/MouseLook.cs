using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]

/// <summary>
/// Class for adding MouseLook to a camera or a transform with a camera attached to it.
/// This will rotate the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation
/// </summary>
/// 
public class MouseLook : MonoBehaviour
{
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;

    public float minimumY = -60f;
    public float maximumY = 60f;


    void Start()
    {
        rotation = new Vector3();
    }


    void Update()
    {
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        rotation.Set(-rotationY, rotationX, 0);
        transform.localEulerAngles = rotation;
    }

    private float rotationY = 0F;
    private Vector3 rotation;
}