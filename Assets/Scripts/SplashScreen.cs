using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private Image splashImage;
    [SerializeField] private string loadLevel;
    [SerializeField] float fadeInTime = 1.5f;
    [SerializeField] float fadeOutTime = 2.5f;
    private AudioSource spawnSound;

    
    
    IEnumerator Start()
    {
        spawnSound = splashImage.gameObject.GetComponent<AudioSource>();
        splashImage.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        spawnSound.Play();
        yield return new WaitForSeconds(fadeInTime);


    }

    void FadeIn()
    {
        splashImage.CrossFadeAlpha(1.0f, fadeInTime, false);
    }

    void FadeOut()
    {
        splashImage.CrossFadeAlpha(0.0f, fadeOutTime, false);
    }
}
