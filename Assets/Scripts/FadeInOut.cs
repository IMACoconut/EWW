using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour {
    public Texture2D blackScreen; 
    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
    public bool sceneStarting = true;      // Whether or not the scene is still fading in.

    void Awake()
    {
        // Set the texture so that it is the the size of the screen and covers it.
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }

    void Start()
    {
       
    }


    void Update()
    {
       
        
        // If the scene is starting...
        if (sceneStarting) {

            // ... call the StartScene function.
            StartCoroutine(StartScene());
            
        }
        else
        {
            StopCoroutine("StartScene");
        }
        Debug.Log(sceneStarting);
            

    }


    void FadeToClear()
    {

        //StartCoroutine(Wait()); 
        // Lerp the colour of the texture between itself and transparent.
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
       
    }


    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    IEnumerator StartScene()
    {
        
        guiTexture.enabled = true;
        yield return new WaitForSeconds(2f);
        // Fade the texture to clear.
        FadeToClear();
        Debug.Log(guiTexture.color.a);

        // If the texture is almost clear...
        if (guiTexture.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.

            Color colorT = Color.black;
            colorT.a = 1f;
            guiTexture.color = colorT;
            guiTexture.enabled = false;
            
           
            // The scene is no longer starting.
            sceneStarting = false;
        }
    }


    public void EndScene()
    {
        // Make sure the texture is enabled.
        guiTexture.enabled = false;
        Debug.Log(guiTexture.color.a);

        // Start fading towards black.
        FadeToBlack();

            

    }

}
