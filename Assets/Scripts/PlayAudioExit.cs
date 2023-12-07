using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioExit : MonoBehaviour
{
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        //audioData.Play(0);
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.8f);
        audioData.Play();
        Debug.Log("play clip");
    }

        private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {  
            StartCoroutine(Pause());   
        }
    }
}

