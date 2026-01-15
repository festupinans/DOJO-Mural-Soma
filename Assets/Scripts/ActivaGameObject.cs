using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaGameObject : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        Activa();
    }


    public void Activa()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
