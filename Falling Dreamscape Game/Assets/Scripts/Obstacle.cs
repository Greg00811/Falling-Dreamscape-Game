using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 3f; //speed of obstacle
    private Transform cameraTransform; //gets camera position
    private float previousCameraY; //stores camera's y value from previous frame
    private GameManager gameManager; //refs game manager script
    private bool hasCollided = false; //checks for collision
    private ScreenFade screenFade; //refs fade script

    [SerializeField] private Sprite standingSprite; //Ref to original sprite appearance
    private Transform playerVisuals; //Ref to player sprite

    private void Start()
    {
        cameraTransform = Camera.main.transform; //finds the camera when game starts
        previousCameraY = cameraTransform.position.y;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //finds game manager and grabs the script
        screenFade = FindFirstObjectByType<ScreenFade>(); //Finds fade object and stores it so the obstacle can trigger it later
    }

    private void LateUpdate()
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

    //checks for collision with player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided) return;

        if (other.CompareTag("Player"))
        {
            hasCollided = true;
            //disables player movement upon collision
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            Time.timeScale = 0f; //freezes game

            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            //freezes the player after collision
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            if (screenFade != null)
            {
                StartCoroutine(LoadBedroom()); //Triggers the fade sequence and loads the bedroom scene
            }

            playerVisuals = other.transform.Find("Visuals");

            //rotates player 90 deg and restores OG player sprite upon collision
            if (playerVisuals != null)
            {
                Animator playerAnimator = playerVisuals.GetComponent<Animator>(); //Find the animator component on visuals
                SpriteRenderer spriteRenderer = playerVisuals.GetComponent<SpriteRenderer>(); //Finds OG standing sprite

                if (playerAnimator != null)
                {
                    playerAnimator.enabled = false;
                }

                if (spriteRenderer != null && standingSprite != null)
                {
                    spriteRenderer.sprite = standingSprite;
                }
                
                playerVisuals.rotation = Quaternion.Euler(0f, 0f, -90f);
            }

        }
    }

    private IEnumerator LoadBedroom()
    {
        yield return screenFade.FadeToBlack(); //waits for fade to conclude
        Time.timeScale = 1f; //unfreezes the game
        SceneManager.LoadScene("Bedroom");
    }
}
