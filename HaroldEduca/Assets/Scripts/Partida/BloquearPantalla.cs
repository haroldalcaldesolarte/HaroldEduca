using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloquearPantalla : MonoBehaviour
{
    [SerializeField] private GameObject PanelBloqueante;
    private void Start()
    {
        GetBloquearPantalla();
    }

    // Start is called before the first frame update
    private void GetBloquearPantalla() {
        GameManager.Instance.GetPanelBloqueante(PanelBloqueante);
    }
}
