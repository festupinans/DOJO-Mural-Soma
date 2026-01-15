using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class RaySelector : MonoBehaviour
{
    //Almacena el Raycast Manager
    private ARRaycastManager raycastManager;

    private UIManager uiManger;

    //Almacena los cajones 
    private Cajones cajones;

    //Almacena los hits
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //Almacena el GameObject de la camara
    private Camera arCamara;

    [SerializeField]
    private GameObject sonido;

    [SerializeField]
    private TextMeshProUGUI contadorTexto;

    private int contador = 0;

    [SerializeField]
    private GameObject hasTerminado;

    private string[] namesAudios;
    private int numAudios;

    void Start()
    {
        arCamara = Camera.main;
        raycastManager = FindObjectOfType<ARRaycastManager>();
        uiManger = FindObjectOfType<UIManager>();

        cajones = FindObjectOfType<Cajones>();

        numAudios = sonido.transform.childCount;
        namesAudios = new string[numAudios];

        for (int i = 0; i < numAudios; i++)
        {
            namesAudios[i] = sonido.transform.GetChild(i).gameObject.name;
            //Debug.Log(sonido.transform.GetChild(i).cajon.name);
        }
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0); // Obtener el primer toque


            Ray ray = arCamara.ScreenPointToRay(touchOne.position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                string nombreHit = hit.transform.gameObject.name;

                cajones.ChangeImage(nombreHit);

                for (int i = 0; i < numAudios; i++)
                {
                    if(nombreHit == namesAudios[i])
                    {
                        sonido.transform.gameObject.GetComponent<AudioManager>().PlayAudio(nombreHit);
                        hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                        hit.transform.GetChild(0).gameObject.SetActive(false);
                        hit.transform.GetChild(1).gameObject.SetActive(true);
                        contador++;
                        contadorTexto.text = contador+" / 12";
                        if (contador == 12)
                        {
                            uiManger.AperturaYCierre(hasTerminado);
                        }
                    }
                }
            }
        }
    }


   
}

