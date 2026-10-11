using UnityEngine;
using UnityEngine.UI; //Allows for working with Unity UI elements
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.5f;

    private void Awake()
    {
        fadeImage = GetComponent<Image>(); //gets fade component
    }

    public IEnumerator FadeToBlack()
    {
        float elapsed = 0f; //how much time has passed since the fade began
        Color color = fadeImage.color; //grabs the color from the fade component

        //plays out the fade
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime; //adds the time that has passed to elapsed variable and allows fade to run while game is paused

            //Clamp ensures that the Alpha value never exceeds 1 or drops below 0 during the loop
            color.a = Mathf.Clamp01(elapsed / fadeDuration); //changes the Alpha value of the fade based on how much time has passed
            fadeImage.color = color; //sets the fade to this new value as it plays out

            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    
    public IEnumerator FadeToWhite()
    {
        float elapsed = 0f;
        Color color = fadeImage.color;
        //changes color to white
        color.r = 1f;
        color.g = 1f;
        color.b = 1f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }
}