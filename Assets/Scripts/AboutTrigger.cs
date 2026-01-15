using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutTrigger : MonoBehaviour
{
    private UIManager _manager;

    //Almacena el GameObject de la camara
    private Camera arCamara;

    [SerializeField]

    void Start()
    {
        _manager = FindObjectOfType<UIManager>();
        arCamara = Camera.main;
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0); // Obtener el primer toque


            Ray ray = arCamara.ScreenPointToRay(touchOne.position);
            //RaycastHit hitInfo;

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider == null)
                {
                    return;
                }
                _manager.PoweredBy2();
            }
        }
    }



    //private void OnTriggerEnter(Collider other)
    //{
    //    _manager.PoweredBy2();
    //}
}
