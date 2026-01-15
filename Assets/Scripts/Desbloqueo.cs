using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desbloqueo : MonoBehaviour
{
    [SerializeField]
    private GameObject objecto;

    public void Debloque()
    {
        objecto.SetActive(true);
        gameObject.SetActive(false);
    }
}
