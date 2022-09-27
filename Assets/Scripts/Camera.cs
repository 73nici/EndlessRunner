using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [CanBeNull] private UnityEngine.Camera camera;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        camera = this.GetComponentInChildren<UnityEngine.Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        camera = this.GetComponentInChildren<UnityEngine.Camera>();
        score++;
        PlayerPrefs.SetInt("Score", score);
        var newVector = this.transform.position;
        newVector.z += 0.1f;
        this.transform.position = newVector;
        
        handleInput();
    }



    private void handleInput()
    {
        // getestet mit Xbox Controller und funktioniert :D
        print(Input.GetAxis("Horizontal"));
    }
}
