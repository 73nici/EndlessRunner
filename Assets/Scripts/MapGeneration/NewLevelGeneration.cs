using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NewLevelGeneration : MonoBehaviour
{
    public GameObject player;
    private float _nextPlayerCheck = 0;

    public int amountOfPreGeneratedFloors = 3;

    public GameObject[] obstacles;
    public int obstaclePercentageGrowthFactor = 3;
    private readonly float[] _xPosForObstacles = { -0.5f, 0.5f, 1.5f };
    private int _percentageForGeneratingObstacle = 0;
    private int _leftBorder = 33;
    private int _rightBorder = 66;
    private int _nextPlayerZForDiffInc = 100;

    public GameObject floor;
    public int zPositionDifference = 20;
    public float zPosition = 0.5f;

    // Allocation is important!!!
    private List<GameObject> _generatedObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        zPosition += zPositionDifference;
        for (int i = 0; i < amountOfPreGeneratedFloors; i++)
        {
            GenerateSection();
        }

        _nextPlayerCheck = player.transform.position.z + zPositionDifference;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z >= _nextPlayerCheck)
        {
            _nextPlayerCheck += zPositionDifference;
            GenerateSection();
            ClearSections();
        }

        if (player.transform.position.z >= _nextPlayerZForDiffInc)
        {
            obstaclePercentageGrowthFactor += 1;
            _nextPlayerZForDiffInc += 100;
        }
    }

    private void GenerateSection()
    {
        // Create Floor
        _generatedObjects.Add(Instantiate(floor, new Vector3(0.5f, 0.5f, zPosition), Quaternion.identity));

        // Create Obstacle
        for (int i = -zPositionDifference / 2; i < +zPositionDifference / 2; i++)
        {
            if (RandomizerObstacles())
            {
                Random rnd = new Random();
                int xPosForObstacleIndex = RandomizerXPosForObstacle();
                int selectedObstacleIndex = rnd.Next(0, obstacles.Length);

                _generatedObjects.Add(
                    Instantiate(
                        obstacles[selectedObstacleIndex],
                        new Vector3(_xPosForObstacles[xPosForObstacleIndex],
                            1.5f, zPosition + i), Quaternion.identity)
                );
            }
        }

        // End Function
        zPosition += zPositionDifference;
    }

    private void ClearSections()
    {
        List<GameObject> objectsToRemove = new List<GameObject>();

        // Destroy Objects if Condition
        foreach (var generatedObject in _generatedObjects)
        {
            if (generatedObject.transform.position.z < player.transform.position.z - zPositionDifference)
            {
                Destroy(generatedObject);
                objectsToRemove.Add(generatedObject);
            }
        }

        // Remove them out of the list
        foreach (var objectToRemove in objectsToRemove)
        {
            _generatedObjects.Remove(objectToRemove);
        }

    }

    private bool RandomizerObstacles()
    {
        Random rnd = new Random();

        bool isGeneratingObstacle = rnd.Next(1, 100) <= _percentageForGeneratingObstacle;

        _percentageForGeneratingObstacle = (isGeneratingObstacle) ? 0 : _percentageForGeneratingObstacle + obstaclePercentageGrowthFactor;

        return isGeneratingObstacle;
    }

    private int RandomizerXPosForObstacle()
    {
        Random rnd = new Random();

        //      ObstacleLeft  |  ObstacleMiddle  |  ObstacleRight
        //  0             leftBorder       rigthBorder           100

        int randomNumber = rnd.Next(0, 100);

        if (randomNumber < _leftBorder)
        {
            if (_leftBorder >= 10)
            {
                _leftBorder -= 10;
                _rightBorder -= 5;
            }
            return 0;
        }
        else if (randomNumber > _leftBorder && randomNumber < _rightBorder)
        {
            if (_rightBorder - _leftBorder >= 11)
            {
                _leftBorder += 5;
                _rightBorder -= 5;
            }
            return 1;
        }
        else if (randomNumber > _rightBorder)
        {
            if (_rightBorder <= 89)
            {
                _leftBorder += 5;
                _rightBorder += 10;
            }
            return 2;
        }

        //This Shouldn't happen
        return 0;
    }
}
