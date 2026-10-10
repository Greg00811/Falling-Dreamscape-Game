using UnityEngine;
using UnityEngine.UI; //Allows for working with Unity UI elements

public class ScreenFade : MonoBehaviour
{
    private Image fadeImage;

    private void Awake()
    {
        fadeImage = GetComponent<Image>(); //gets fade component
    }
}