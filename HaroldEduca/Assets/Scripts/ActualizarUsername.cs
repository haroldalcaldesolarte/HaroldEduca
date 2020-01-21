using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualizarUsername : MonoBehaviour
{
    [SerializeField] private Text nombre;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ActualizarNombre(nombre);
    }
}
