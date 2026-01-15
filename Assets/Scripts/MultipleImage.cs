using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleImage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabsToSpawn;

    [SerializeField]
    private TextMeshProUGUI consola;



    //[SerializeField]
    //private GameObject scan;

    private ARTrackedImageManager _arTrackedImageManager;
    private Dictionary<string, GameObject> _arObjects;

    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObjects = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChange;

        foreach (GameObject prefab in prefabsToSpawn)
        {
            GameObject newARObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newARObject.name = prefab.name;
            newARObject.gameObject.SetActive(false);
            _arObjects.Add(newARObject.name, newARObject);
        }
    }

    private void OnDestroy()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChange;
    }

    private void OnTrackedImageChange(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateTrackedImage(trackedImage);

        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateTrackedImage(trackedImage);

        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
                        
        }
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            //scan.SetActive(true);
            consola.text = "Imagen removida = " + trackedImage.referenceImage.name;
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }

        if (prefabsToSpawn != null)
        {
            consola.text = "Imagen encontrada = " + trackedImage.referenceImage.name;
            Quaternion sumaRotacion = Quaternion.Euler(-90f, 0f, 180f);
            Vector3 sumaPosicion = new(0f,-0.7f,0f);

            Quaternion rotacionFinal = trackedImage.transform.rotation * sumaRotacion;
            Vector3 posicionFinal = trackedImage.transform.position + sumaPosicion;
            //Vector3 posicionFinal = trackedImage.transform.position ;

            GameObject objeto = _arObjects[trackedImage.referenceImage.name];
            objeto.SetActive(true);
            objeto.transform.SetPositionAndRotation(posicionFinal, rotacionFinal);
        }
    }
}
