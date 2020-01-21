using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumenPartida : MonoBehaviour
{
    [SerializeField] private Text aciertos;
    [SerializeField] private Text fallos;
    [SerializeField] private Text usuario;
    [SerializeField] private Text score;
    [SerializeField] private GameObject panelError;

    GameManager g = GameManager.Instance;
    // Start is called before the first frame update
    void Start()
    {
        int[] resumen =g.GetAciertoFallos();
        g.ActualizarNombre(usuario);
        aciertos.text = resumen[0].ToString();
        fallos.text = resumen[1].ToString();
        score.text = resumen[2].ToString();
        string response = response = g.GuardarPartida(usuario.text, score.text);
        if (response.Equals("0"))
            MostrarError();

        g.setAciertosFallos();
    }

    public void MostrarError() {
        panelError.SetActive(true);
    }

    public void OcultarPanelError() {
        panelError.SetActive(false);
    }
}
