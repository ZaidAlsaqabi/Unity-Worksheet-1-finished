using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; 

public class ImageTracker : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager m_TrackedImageManager;
    public GameObject shipPrefab; 
    public GameObject shipTwoPrefab; 

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage newImage in eventArgs.added)
        {
            UpdateTrackableImage(newImage);
        }

        foreach (ARTrackedImage updatedImage in eventArgs.updated)
        {
            if (updatedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                UpdateTrackableImage(updatedImage);
            }
            else if (updatedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
            {
                if (spawnedObjects.ContainsKey(updatedImage.referenceImage.name))
                {
                    spawnedObjects[updatedImage.referenceImage.name].SetActive(false);
                }
            }
        }

        foreach (ARTrackedImage removedImage in eventArgs.removed)
        {
            if (spawnedObjects.ContainsKey(removedImage.referenceImage.name))
            {
                Destroy(spawnedObjects[removedImage.referenceImage.name]);
                spawnedObjects.Remove(removedImage.referenceImage.name);
            }
        }
    }

    private void UpdateTrackableImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        GameObject prefabToSpawn = null;

        if (imageName == "puddle" || imageName == "shiptwo")
        {
            prefabToSpawn = imageName == "puddle" ? shipPrefab : shipTwoPrefab;
        }

        if (prefabToSpawn != null)
        {
            GameObject spawnedObject;
            
            if (!spawnedObjects.TryGetValue(imageName, out spawnedObject))
            {
                spawnedObject = Instantiate(prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
                spawnedObject.transform.SetParent(trackedImage.transform, false);
                spawnedObjects.Add(imageName, spawnedObject);
            }
            else
            {
                spawnedObject.SetActive(true);
            }
        }
    }
    
}
