using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; 

public class ImageTracker : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;
    public GameObject shipPrefab; 
    public GameObject shipTwoPrefab; 

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
     
        foreach (ARTrackedImage newImage in eventArgs.added)
        {
           
            if (newImage.referenceImage.name == "puddle")
            {
                GameObject newObject = GameObject.Instantiate(shipPrefab);
                newObject.transform.SetParent(newImage.transform, false);
            }
            else if (newImage.referenceImage.name == "shiptwo")
            {
                GameObject newObject = GameObject.Instantiate(shipTwoPrefab);
                newObject.transform.SetParent(newImage.transform, false);
            }
        }
    }
}