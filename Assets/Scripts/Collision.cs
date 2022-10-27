using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: this class handles player movement and 
public class Collision : MonoBehaviour
{
    public int jumpMultiplier = 10;
    public float playerSpeed = 0.1f;

    private Animator animator;
    private CharacterController characterController;
    private Rigidbody _rigidbody;
    private float originalStepOffset;
    private float ySpeed;

    private int _score = 0;
    private bool is_grounded = true;
    private bool is_jumping = false;
    private bool _lockedInput = false;

    private AudioSource audioSource;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
         //Game ends here
        if (collision.gameObject.CompareTag("Obstacle"))
        SceneManager.LoadScene("Scenes/GameOverScene");
    }


    private void OnCollisionStay(UnityEngine.Collision collisionInfo)
    {
        print("onStay");
        is_grounded = true;
        animator.SetBool("is_grounded", true);
        is_jumping = false;
        animator.SetBool("is_jumping", false);
        animator.SetBool("is_falling", false);

    }

    private void FixedUpdate()
    {
        _score++;
        PlayerPrefs.SetInt("Score", _score);
        var newVector = transform.position;
        newVector.z += playerSpeed;
        transform.position = newVector;
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (!_lockedInput)
        {
            StartCoroutine(HandleInput());
            _lockedInput = true;
        }

        if ((is_jumping && ySpeed < 0) || ySpeed < -2)
        {
            animator.SetBool("is_falling", true);
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

        if (verticalValue > 0 && is_grounded)
        {
            Debug.Log(is_grounded);
            is_grounded = false;
            animator.SetBool("is_grounded", false);
            animator.SetBool("is_jumping", true);
            is_jumping = true;
            audioSource.Play(0);
            _rigidbody.AddForce(new Vector3(0, 2, 0) * jumpMultiplier, ForceMode.Impulse);
        }

        transform.position = newVector;
        yield return new WaitForSeconds(0.1f);
        _lockedInput = false;
    }
}