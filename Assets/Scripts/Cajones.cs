using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MiClasePersonalizada
{
    [SerializeField]
    private string clave;

    [SerializeField]
    private string nombre;
    
    [SerializeField]
    private string pista;
    
    [SerializeField]
    private Sprite color;
    
    [SerializeField]
    private Sprite noColor;
    
    [SerializeField]
    private GameObject cajon;

    // Constructor
    public MiClasePersonalizada(string clave, string valor, string music,Sprite sprite1, Sprite sprite2, GameObject gameObject)
    {
        this.clave = clave;
        this.nombre = valor;
        this.pista = music;
        this.color = sprite1;
        this.noColor = sprite2;
        this.cajon = gameObject;
    }

    // Propiedades públicas para acceder a los valores
    public string Clave => clave;
    public string Valor => nombre;
    public string Music => pista;
    public Sprite Sprite1 => color;
    public Sprite Sprite2 => noColor;
    public GameObject GameObject => cajon;
}


public class Cajones : MonoBehaviour
{
    [SerializeField]
    private List<MiClasePersonalizada> listaDeValores;

    [SerializeField]
    private GameObject contenedor;

    private Dictionary<string, GameObject> spawnedObjects = new();

    private void Start()
    {
        // Coloca los cajones en la UI
        foreach (var item in listaDeValores)
        {
            Image img = item.GameObject.transform.GetChild(0).GetComponent<Image>();
            img.sprite = item.Sprite2;

            TMP_Text nombre = item.GameObject.transform.GetChild(1).GetComponent<TMP_Text>();
            nombre.text = item.Valor;

            TMP_Text pista = item.GameObject.transform.GetChild(2).GetComponent<TMP_Text>();
            pista.text = item.Music;

            //item.GameObject.transform.localScale = Vector3.one;

            GameObject spawn = Instantiate(item.GameObject);
            spawn.transform.SetParent(contenedor.transform);

            spawn.name = item.Clave;

            //spawnedObjects.Add(item.Clave, spawn);
        }

        // Obtener el número de hijos
        int numChild = contenedor.transform.childCount;

        // Recorrer todos los hijos
        for (int i = 0; i < numChild; i++)
        {
            // Obtener el GameObject hijo en el índice i
            GameObject hijo = contenedor.transform.GetChild(i).gameObject;
            contenedor.transform.GetChild(i).gameObject.transform.localScale = Vector3.one;
            spawnedObjects.Add(hijo.name, hijo);
        }
    }

    public void ChangeImage(string name)
    {
        int contador = 0;
        //Sube el volument a la pista correspondiente
        foreach (KeyValuePair<string, GameObject> child in spawnedObjects)
        {
            if (name == child.Key)
            {
                child.Value.transform.GetChild(0).GetComponent<Image>().sprite = listaDeValores[contador].Sprite1;
            }
            contador++;
        }
    }
}
