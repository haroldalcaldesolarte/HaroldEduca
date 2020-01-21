using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEscena : MonoBehaviour
{
    public void CambiarEscena(string escena)
    {
        GameManager.Instance.CambiarEscena(escena);
    }

    public void RestartJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SalirJuego()
    {
        GameManager.Instance.SalirJuego();
    }
}