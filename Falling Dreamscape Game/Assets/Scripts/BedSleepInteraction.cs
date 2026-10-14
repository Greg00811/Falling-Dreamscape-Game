using UnityEngine;
using UnityEngine.SceneManagement; //Gives the script access to Unity's scene loading functions
using System.Collections; //A collection of tools that deal with groups of items and sequences

public class BedSleepInteraction : MonoBehaviour
{
    //These variables store the transform values of the sleep position above the bed and the player sprite
    [SerializeField] private Transform sleepPosition; 
    [SerializeField] private Transform playerVisuals;
    [SerializeField] private GameObject interactionPrompt;
    
    private bool playerNearby = false; //Value that says whether or not player is near bed
    private Rigidbody2D playerRigidbody; //Freezes player position when sleeping
    private PlayerMovement playerMovement; //Ref to player movement
    private ScreenFade screenFade; //Ref to fade script

    // Update is called once per frame
    void Update()
    {
        //Allows the player to sleep and changes the current position to the sleep position
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.enabled = false; //disables movement when asleep
            transform.position = sleepPosition.position;

            Vector3 playerScale = transform.localScale; //gets player scale
            playerScale.x = Mathf.Abs(playerScale.x); //corrects the direction of the player when sleeping
            transform.localScale = playerScale; //redefines player scale

            //turns off gravity and freezes player's position
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.gravityScale = 0f;
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            
            // Rotate the player's visuals to lie down
            playerVisuals.localRotation = Quaternion.Euler(0f, 0f, 90f); //Quaternion.Euler converts angles into a rotation value

            StartCoroutine(LoadDreamSequence(1.5f));
        }
    }

    //Checks to see if player enters the trigger box and then redefines the value of the bool
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bed"))
        {
            playerNearby = true;

            //Unhides prompt if player enters trigger box
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true);
            }
        }
    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bed"))
        {
            playerNearby = false;

            //Hides prompt if player exits trigger box
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
        }
    }

    //Grabs the player's rigid body and movement upon starting the game
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        screenFade = FindFirstObjectByType<ScreenFade>(); //finds fade component
    }

    private IEnumerator LoadDreamSequence(float delay)
    {
        yield return new WaitForSeconds(delay);

        //triggers fade
        if (screenFade != null)
        {
            yield return screenFade.FadeToWhite();
        }

        SceneManager.LoadScene("DreamSequence");
    }
}
