using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int cloudMove;
    public int obstacleCount;
    public int navyStarCount;
    public int rainbowStarCount;
    public GameObject cloudPrefab;
    public GameObject obstaclePrefab;
    public GameObject navyStarPrefab;
    public GameObject rainbowStarPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 7f;
        cloudMove = 1;

        CreateSky();
        CreateObstacles();
        CreateNavyStars(navyStarCount);
        CreateRainbowStars(rainbowStarCount);
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

    //this function spawns obstacles randomly between the values of the screen size with no rotation applied
    void CreateObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            Instantiate(
                obstaclePrefab,
                new Vector3(
                    Random.Range(-horizontalScreenSize, horizontalScreenSize),
                    -verticalScreenSize - 3f - (i * 5f), //obstacles spawn lower on screen and farther apart vertically
                    0f
                ),
                Quaternion.identity
            );
        }
    }

    //this function spawns navy stars randomly between the values of the screen size with no rotation applied
    //func is public so StarCollectible script can access
    public void CreateNavyStars(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(
                navyStarPrefab,
                new Vector3(
                    Random.Range(-horizontalScreenSize, horizontalScreenSize),
                    Camera.main.transform.position.y - verticalScreenSize - 3f - (i * 5f), //new stars spawn lower on the screen
                    0f
                ),
                Quaternion.identity
            );
        }
    }

    //this function spawns rainbow stars randomly between the values of the screen size with no rotation applied
    public void CreateRainbowStars(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(
                rainbowStarPrefab,
                new Vector3(
                    Random.Range(-horizontalScreenSize, horizontalScreenSize),
                    Camera.main.transform.position.y - verticalScreenSize - 3f - (i * 5f), //new stars spawn lower on the screen
                    0f
                ),
                Quaternion.identity
            );
        }
    }

    //initiates a delay before spawning another star
    public IEnumerator RespawnStar(StarCollectible.StarType starType, float respawnDelay)
    {
        yield return new WaitForSeconds(respawnDelay);

        if (starType == StarCollectible.StarType.Navy)
        {
            CreateNavyStars(1);
        }
        else if (starType == StarCollectible.StarType.Rainbow)
        {
            CreateRainbowStars(1);
        }
    }
}
