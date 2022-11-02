using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    public float RotateSpeed = 1.2f;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        print("Hello");
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);
    }
}
