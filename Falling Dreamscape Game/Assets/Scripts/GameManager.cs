using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int cloudMove;
    public int obstacleCount;
    public GameObject cloudPrefab;
    public GameObject obstaclePrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 7f;
        cloudMove = 1;

        CreateSky();
        CreateObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(
                cloudPrefab, 
                new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize),
                    Random.Range(-verticalScreenSize, verticalScreenSize),
                    0
                ), 
                Quaternion.identity
            );
        }
        
    }

    void CreateObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            Instantiate(
                obstaclePrefab, 
                new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize),
                    Random.Range(-verticalScreenSize, verticalScreenSize),
                    0f
                ),
                Quaternion.identity
            );
        }
    }
}
