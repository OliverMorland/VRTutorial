using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace VRTutorial.Samples {
	public class PhoneRinger : MonoBehaviour
	{
	    public Image callSign;
	    public Sprite[] phoneImages;
	    public float intervalTime = 0.1f;
	    public AudioSource speaker;
	    public AudioClip ringTone;
	    Coroutine ringingCoroutine = null;
	    int counter = 0;
	
	    public void StartRinging()
	    {
	        speaker.clip = ringTone;
	        speaker.loop = true;
	        speaker.Play();
	        callSign.enabled = true;
	        ringingCoroutine = StartCoroutine(cycleThroughPhoneImages());   
	    }
	
	    public void StopRinging()
	    {
	        if (ringingCoroutine != null)
	        {
	            StopCoroutine(ringingCoroutine);
	        }
	        callSign.enabled = false;
	        speaker.Stop();
	        speaker.loop = false;
	    }
	
	    IEnumerator cycleThroughPhoneImages()
	    {
	        while (true)
	        {
	            yield return new WaitForSeconds(intervalTime);
	            callSign.sprite = GetNextSprite();
	        }
	    }
	
	    Sprite GetNextSprite()
	    {
	        if (counter == phoneImages.Length - 1)
	        {
	            counter = 0;
	        }
	        else
	        {
	            counter++;
	        }
	        return phoneImages[counter];
	    }
	}
}
