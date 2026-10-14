using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BasicInteractions : MonoBehaviour
{
    [SerializeField] private Light2D objectLight;

    private bool playerNearby = false; //Value that states whether player is in trigger

    // Update is called once per frame
    void Update()
    {
        //If player enters trigger, the player can press E to interact with light
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            objectLight.enabled = !objectLight.enabled;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
