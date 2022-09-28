using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_script : MonoBehaviour
{
    GameObject Player;
    public float forceFactor=1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 enemyPos = this.GetComponent<Transform>().position;
        Vector3 playerPos = Player.GetComponent<Transform>().position;
        Vector3 forceVector = playerPos-enemyPos;
        this.GetComponent<Rigidbody>().AddForce(forceFactor*forceVector);
    }
}
