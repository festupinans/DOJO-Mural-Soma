using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject contenedor;

    [SerializeField]
    private GameObject siguiente;
    
    [SerializeField]
    private GameObject adelante;
    
    [SerializeField]
    private GameObject anterior;

    [SerializeField]
    private TMP_Text texto;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private GameObject poweredBy;

    [SerializeField]
    private GameObject[] cerrar;

    //[SerializeField]
    //private GameObject estrellas;

    //[SerializeField]
    //private GameObject cajonEncontrados;

    [SerializeField]
    private Sprite[] icons;

    private string[] textos;

    private int i;

    //VARIABLES PARA DOTWEEN
    private Vector3 escalaUno = new(1f,1f,1f);

    private Vector3 escalaCero = new(0f,0f,0f);

    [SerializeField]
    private float duracion = .3f;

    [SerializeField]
    private float duracionMover = 1f;

    [SerializeField]
    private float duracionShake = .5f;
    [SerializeField]
    private int cantidadShake = 5;

    private void Start()
    {
        i = 0;
        //Debug.Log(i);
        textos = new string[]
        {
            "Las estrellas están perdidas, tu puedes traerlas de vuelta, tienes que buscarlas en el mural y tocarlas para activar la experiencia AR.",
            "Tienes un cajon donde encuentras pistas de la ubicación de las experiencias ocultas, toca el icono del contador."
        };
        
    }

    private void Update()
    {
        if (i == 0)
        {
            anterior.SetActive(false);
        }
        else
        {
            anterior.SetActive(true);
        }

        if (i == textos.Length-1)
        {
            siguiente.SetActive(false);
            adelante.SetActive(true);
        }
        else
        {
            siguiente.SetActive(true);
            adelante.SetActive(false);
        }
    }

    public void Siguiente()
    {
        //Debug.Log("Siguiente");
        if (i <= textos.Length) 
        {
            // Incrementar el índice para la siguiente vez
            i++;

            // Actualizar el texto con el siguiente elemento del arreglo
            texto.text = textos[i];
            icon.sprite = icons[i];

            //Debug.Log(i);
        }

        
    }

    public void Retroceder()
    {
        //Debug.Log("Anterior");

        if (!(i <= 0))
        {
            i--;

            texto.text = textos[i];
            icon.sprite = icons[i];
            //Debug.Log(i);
        }
    }


    public void ContenedorActive()
    {
        contenedor.SetActive(!contenedor.activeSelf);
    }

    public void PoweredBy()
    {
        poweredBy.SetActive(!poweredBy.activeSelf);
    }

    public void PoweredBy2()
    {
        Scale(poweredBy.transform, escalaUno);
        Move(poweredBy.transform, new(0f, 0f, 0f));
    }

    //public void Estrellas()
    //{
    //    estrellas.SetActive(!estrellas.activeSelf);
    //}

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }


    void Scale(Transform toScale, Vector3 scale)
    {
        toScale.DOScale(scale, duracion).SetEase(Ease.InOutQuint);
    }
    void Move(Transform toMove, Vector3 posicion)
    {
        toMove.DOLocalMove(posicion, duracion, true);
    }


    public void AperturaYCierre(GameObject openClose)
    {
        foreach (var item in cerrar)
        {
            
            Vector3 posicionClose1 = (item == poweredBy) ? new(320f, -941f, 0f) : new(-320f, -941f, 0f);

            Vector3 posicionClose = (item == contenedor) ? new(0f, 0f, 0f) : posicionClose1;

            Vector3 posicionOpen = new(0f, 0f, 0f);

            if (item == openClose)
            {
                if(item.transform.localScale == escalaCero)
                {
                    Scale(item.transform, escalaUno);
                    Move(item.transform, posicionOpen);

                }else if(item.transform.localScale == escalaUno)
                {
                    Scale(item.transform, escalaCero);
                    Move(item.transform, posicionClose);
                }
            }
            else
            {
                Scale(item.transform, escalaCero) ;
                Move(item.transform, posicionClose);
            }
        }

    }

    public void ButtonScaleShake(Transform toShake)
    {
        toShake.DOPunchScale(new Vector3(.2f, .2f, .2f), duracionShake, cantidadShake, .5f);
    }
}
