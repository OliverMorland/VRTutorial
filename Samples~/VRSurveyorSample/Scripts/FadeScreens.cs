using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreens : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    private Renderer rend;

    public static FadeScreens Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
            FadeIn();
    }
    public void FadeIn()
    {
        Debug.Log("FadingIN");
        Fade(1, 0,false);
     

    }
    public void FadeOut()
    {
        Debug.Log("FadingOut");
        Fade(0, 1,true);
       
    }
    // Update is called once per frame
   public void Fade(float alphaIn, float alphaOut, bool isFadeOut)
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(FadeRoutine(alphaIn,alphaOut, isFadeOut));

    }
    public IEnumerator FadeRoutine(float alphaIn, float alphaOut,bool isFadeOut)
    {

        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            rend.material.SetColor("_Color",newColor);
            timer += Time.deltaTime;
            yield return null;
        }
        Color newColor_new = fadeColor;
        newColor_new.a =  alphaOut;
        rend.material.SetColor("_Color", newColor_new);
        if(!isFadeOut)
        this.GetComponent<MeshRenderer>().enabled = false;
    }
}
