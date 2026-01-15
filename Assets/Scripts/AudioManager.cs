using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private Dictionary<string, GameObject> childs;

    private Boolean playBool = true; 

    void Awake()
    {
        childs = new Dictionary<string, GameObject>();

        // Obtener el número de hijos
        int numChild = transform.childCount;

        // Recorrer todos los hijos
        for (int i = 0; i < numChild; i++)
        {
            // Obtener el GameObject hijo en el índice i
            GameObject hijo = transform.GetChild(i).gameObject;
            childs.Add(hijo.name, hijo);
        }
    }

    public void PlayAudio(string name)
    {
        //Inicia todas las pistas
        if (playBool)
        {
            foreach (KeyValuePair<string, GameObject> hijos in childs)
            {
                hijos.Value.GetComponent<AudioSource>().Play();
            }
            playBool = false;
        }

        //Sube el volument a la pista correspondiente
        foreach (KeyValuePair<string, GameObject> child in childs)
        {
            if (name == child.Key)
            {
                child.Value.GetComponent<AudioSource>().volume = 1.0f;
            }
        }

        //GameObject audio = childs[name];
        //audio.GetComponent<AudioSource>().Play();
    }
}
