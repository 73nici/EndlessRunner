using System.Collections;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private int score = 0;

    private bool lockedInput = false;
    // Update is called once per frame
    void Update()
    {
        score++;
        PlayerPrefs.SetInt("Score", score);
        var newVector = this.transform.position;
        newVector.z += 0.1f;
        this.transform.position = newVector;

        if (!lockedInput)
        {
            StartCoroutine(handleInput());
            print(Time.fixedDeltaTime);
            lockedInput = true;
        }
        
    }


    private IEnumerator handleInput()
    {
        // getestet mit Xbox Controller und funktioniert :
        var horizontalValue = Input.GetAxis("Horizontal");

        // move to right if possible
        if (horizontalValue > 0 && transform.position.x != 1.5)
        {
            var newVector = transform.position;
            newVector.x += 1;
            transform.position = newVector;
        } // move to left if possible
        else if (horizontalValue < 0 && transform.position.x != -0.5)
        {
            var newVector = transform.position;
            newVector.x -= 1;
            transform.position = newVector;
        }

        yield return new WaitForSeconds(0.1f);
        lockedInput = false;
    }
}
