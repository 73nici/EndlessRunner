using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: this class handles player movement and 
public class collision_cube : MonoBehaviour
{
    public int jumpMultiplier = 10;
    public float playerSpeed = 0.1f;

    private int _score = 0;
    private bool _isGrounded = true;
    private bool _lockedInput = false;
    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        // Game ends here
        if (collision.gameObject.CompareTag("Obstacle"))
            SceneManager.LoadScene("Scenes/GameOverScene");
    }


    private void OnCollisionStay(UnityEngine.Collision collisionInfo)
    {
        // print("onStay");
        _isGrounded = true;
    }

    private void FixedUpdate()
    {
        _score++;
        PlayerPrefs.SetInt("Score", _score);
        var newVector = transform.position;
        newVector.z += playerSpeed;
        transform.position = newVector;

        if (!_lockedInput)
        {
            StartCoroutine(HandleInput());
            _lockedInput = true;
        }
    }

    private IEnumerator HandleInput()
    {
        // getestet mit Xbox Controller und funktioniert :
        var horizontalValue = Input.GetAxis("Horizontal");
        var verticalValue = Input.GetAxis("Vertical");
        var newVector = transform.position;
        // move to right if possible

        if (horizontalValue > 0 && transform.position.x <= 1f)
        {
            newVector.x += 1;
        } // move to left if possible
        else if (horizontalValue < 0 && transform.position.x >= -0.4f)
        {
            newVector.x -= 1;
        }

        if (verticalValue > 0 && _isGrounded)
        {
            _isGrounded = false;
            _rigidbody.AddForce(new Vector3(0, 2, 0) * jumpMultiplier, ForceMode.Impulse);
        }

        transform.position = newVector;
        yield return new WaitForSeconds(0.1f);
        _lockedInput = false;
    }
}