using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NewLevelGeneration : MonoBehaviour
{
    public GameObject[] obstacles;
    public int percentageForGeneratingObstaclePerField = 15;
    public float[] xPosForObstacles = new []{ -0.5f, 0.5f, 1.5f };
    public int minimumSpaceBetweenObstacles = 3;
    
    public GameObject floor;
    public int zPositionDifference = 20;
    public float zPositionStart = 0.5f;
    
    private bool _creatingSection = false;

    // Start is called before the first frame update
    void Start()
    {
        zPositionStart += zPositionDifference;
    }

    // Update is called once per frame
    void Update()
    {
        if (_creatingSection == false)
        {
            _creatingSection = true;
            StartCoroutine(GenerateSection());
        }
    }

    IEnumerator GenerateSection()
    {
        //Create Floor
        Instantiate(floor, new Vector3(0.5f, 0.5f, zPositionStart), Quaternion.identity);

        //Create Obstacle
        Random rnd = new Random();
        
        for (int i = -zPositionDifference / 2; i < +zPositionDifference / 2; i++)
        {
            bool isGeneratingObstacle = rnd.Next(1,100) <= percentageForGeneratingObstaclePerField;
            if (isGeneratingObstacle)
            {
                int xPosForObstacleIndex = rnd.Next(0, xPosForObstacles.Length);

                int selectedObstacleIndex = rnd.Next(0, obstacles.Length);
                Instantiate(
                    obstacles[selectedObstacleIndex], 
                    new Vector3(xPosForObstacles[xPosForObstacleIndex], 
                        1.5f, zPositionStart + i), Quaternion.identity);
                
                i += minimumSpaceBetweenObstacles; //Dont generate impossible Parcour with one obstacle every field
            }
        }
        
        //End Function
        zPositionStart += zPositionDifference;
        yield return new WaitForSeconds(2);
        _creatingSection = false;
    }
}
