using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{

    bool annotationVisible = false; 
    public GameObject annotation;
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    public void selected()
    {

        if (annotationVisible)
        {
            annotation.SetActive(false);
            annotationVisible = false;
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        else {
            annotation.SetActive(true);
            annotationVisible = true;
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }

    }
}


