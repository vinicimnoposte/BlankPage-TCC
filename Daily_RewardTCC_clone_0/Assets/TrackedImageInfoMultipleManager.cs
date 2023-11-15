using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoMultipleManager : MonoBehaviour
{
    //[SerializeField]
    //private GameObject welcomePanel;
    //
    //[SerializeField]
    //private Button dismissButton;
    //
    [SerializeField]
    private TMP_Text imageTrackedText;

    [SerializeField]
    private GameObject[] arObjectsToPlace;

    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f, 0.1f, 0.1f);

    private ARTrackedImageManager m_TrackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        //dismissButton.onClick.AddListener(Dismiss);
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

        // setup all game objects in dictionary
        foreach (GameObject arObject in arObjectsToPlace)
        {
            GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;
            arObjects.Add(arObject.name, newARObject);
            newARObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    //private void Dismiss() => welcomePanel.SetActive(false);

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            arObjects[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        // Display the name of the tracked image in the canvas
        imageTrackedText.text = trackedImage.referenceImage.name;

        // Assign and Place Game Object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position, Quaternion.identity);

       

        Debug.Log($"trackedImage.referenceImage.name: {trackedImage.referenceImage.name}");
    }

    
    void AssignGameObject(string name, Vector3 newPosition, Quaternion rot)
    {
        if (arObjectsToPlace != null)
        {
            GameObject goARObject = arObjects[name];
            goARObject.SetActive(true);
            goARObject.transform.position = newPosition;
            goARObject.transform.localScale = scaleFactor;
            goARObject.transform.rotation = rot;
            //foreach (GameObject go in arObjects.Values)
            //{
            //    Debug.Log($"Go in arObjects.Values: {go.name}");
            //    if (go.name != name)
            //    {
            //        goARObject.SetActive(false);
            //    }
            //}
        }
        else
        {
            GameObject goARObject = arObjects[name];
            goARObject.SetActive(false);
        }
    }
}