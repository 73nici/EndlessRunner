using System;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public float cameraSpeed = 0.1f;
    // Update is called once per frame
    void FixedUpdate()
    {
        var newVector = this.transform.position;
        newVector.z += cameraSpeed;
        this.transform.position = newVector;
    }
}
