using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Transform cameraTransform; //gets camera position
    private float previousCameraY; //stores camera's y value from previous frame
    private GameManager gameManager; //refs game manager script

    private void Start()
    {
        cameraTransform = Camera.main.transform; //finds the camera when game starts
        previousCameraY = cameraTransform.position.y;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //finds game manager and grabs the script
        
    }

    private void Update()
    {
        float cameraYDelta = cameraTransform.position.y - previousCameraY; //subtracts camera's previous y pos from current y pos
        //adjusts obstacle to move in accordance to the camera's position
        transform.position += new Vector3(
            0f,
            cameraYDelta + speed * Time.deltaTime,
            0f
        ); 

        previousCameraY = cameraTransform.position.y; //stores the camera y pos for the next frame

        //allows obstacles to recycle as they reach the top
        if (transform.position.y > cameraTransform.position.y + gameManager.verticalScreenSize)
        {
            transform.position = new Vector3(
                Random.Range(-gameManager.horizontalScreenSize, gameManager.horizontalScreenSize),
                cameraTransform.position.y - gameManager.verticalScreenSize - 1f,
                transform.position.z
            );
        }
    }
}
