using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEspera : MonoBehaviour
{
    private float tiempo_inicio = 0.0f;
    private float tiempo_fin = 2.0f;

    // Update is called once per frame
    void Update()
    {
        tiempo_inicio += Time.deltaTime;
        if (tiempo_inicio >= tiempo_fin) {
            GameManager.Instance.ComprobarInicioSesion();
        }
    }
}
