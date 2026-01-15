using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlLobby : MonoBehaviour
{
    public void SiguienteScena()
    {
        SceneManager.LoadScene(1);
    }

    public void CerrarApp() 
    {
        Application.Quit();
    }
}
