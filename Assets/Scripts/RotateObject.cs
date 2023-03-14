using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    float smooth = 5.0f;
    float tiltAngle = 60.0f;
    // Start is called before the first frame update
    float rotationSpeed = 10f;

    void OnMouseDrag() {
        float rotX = Input.GetAxis("Mouse X")*rotationSpeed*Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y")*rotationSpeed*Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -rotX);
        transform.Rotate(Vector3.right, rotY);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }
}
