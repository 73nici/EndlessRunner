using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NewLevelGeneration : MonoBehaviour
{
    //public GameObject[] section;
    public GameObject[] obstacles;
    public int percentageForGeneratingObstaclePerField = 15;
    public float[] xPosForObstacles = new []{ -0.5f, 0.5f, 1.5f };
    public int minimumSpaceBetweenObstacles = 3;
    
    public GameObject floor;
    public int zPositionDifference = 20;
    public float zPositionStart = 0.5f;
    
    private bool _creatingSection = false;
    //public int sectionNumber;
    
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
        //sectionNumber = Random.Range(0, 0);
        //Instantiate(section[sectionNumber], new Vector3(0,0,zPos), Quaternion.identity);
        
        //Create Floor
        Instantiate(floor, new Vector3(0.5f, 0.5f, zPositionStart), Quaternion.identity);

        //Create Obstacle
        Random rnd = new Random();
        
        for (int i = 0; i < zPositionDifference; i++)
        {
            bool isGeneratingObstacle = rnd.Next(1,100) <= percentageForGeneratingObstaclePerField;
            if (isGeneratingObstacle)
            {
                int xPosForObstacleIndex = rnd.Next(0, xPosForObstacles.Length);
                Console.Write(xPosForObstacles.Length);
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

        
        //https://www.youtube.com/watch?v=sB-kXEOTBcU&list=PLZ1b66Z1KFKit4cSry_LWBisrSbVkEF4t&index=7 13:18
    }
}
