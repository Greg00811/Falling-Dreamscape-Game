using UnityEngine;
using System.Collections;

public class StarCollectible : MonoBehaviour
{

    public enum StarType
    {
        Navy,
        Rainbow
    }

    [SerializeField] private float respawnDelay = 2f; //time before another star spawns
    [SerializeField] private StarType starType; //allows for declaring of star type
    [SerializeField] private float speed = 3f; //speed of collectible
    private Transform cameraTransform; //gets camera position
    private float previousCameraY; //stores camera's y value from previous frame
    private GameManager gameManager; //refs game manager script
    private bool hasCollided = false; //checks for collision
    private Transform playerVisuals; //Ref to player sprite


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        cameraTransform = Camera.main.transform; //finds the camera when game starts
        previousCameraY = cameraTransform.position.y;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //finds game manager and grabs the script
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        float cameraYDelta = cameraTransform.position.y - previousCameraY; //subtracts camera's previous y pos from current y pos
        //adjusts collectible to move in accordance to the camera's position
        transform.position += new Vector3(
            0f,
            cameraYDelta + speed * Time.deltaTime,
            0f
        );

        previousCameraY = cameraTransform.position.y; //stores the camera y pos for the next frame

        //Destroys and respawns stars if they go off screen
        if (transform.position.y > cameraTransform.position.y + gameManager.verticalScreenSize)
        {
            gameManager.StartCoroutine(
                gameManager.RespawnStar(starType, respawnDelay)
            );

            Destroy(gameObject);
        }
    }


    //checks for collision with player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided) return;

        if (other.CompareTag("Player"))
        {
            hasCollided = true;
            
            //calls gamemanager for delay
            gameManager.StartCoroutine(
                gameManager.RespawnStar(starType, respawnDelay)
            ); 

            Destroy(gameObject); //Destroys star upon collision w player
        }
    }
}
