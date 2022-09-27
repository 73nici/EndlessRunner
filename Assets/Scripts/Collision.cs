using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print("Test");
    }


    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        // Game ends here
        print(collision.ToString());
        SceneManager.LoadScene("Scenes/GameOverScene");
    }
}
