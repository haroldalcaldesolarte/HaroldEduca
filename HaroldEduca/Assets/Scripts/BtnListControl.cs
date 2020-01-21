using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Text;
using Universe;
using UnityEngine.SceneManagement;

public class BtnListControl : MonoBehaviour
{
    [SerializeField] private GameObject btnTemplate;
    [SerializeField] private GameObject lista;
    [SerializeField] private GameObject PanelError;

    void Start()
    {
        DeleteBtns(lista);
        GameManager.Instance.GetPanelError(PanelError);
        StartCoroutine(GameManager.Instance.GetCategoriasBien("null",btnTemplate));  
    }

    public void BtnClicked(string nombreCatPadre) {
        DeleteBtns(lista);
        StartCoroutine(GameManager.Instance.GetCategoriasBien(nombreCatPadre, btnTemplate));
    }

    public void DeleteBtns(GameObject lista) {
        Button[] btns = lista.GetComponentsInChildren<Button>();
        if (btns.Length > 0) {
            foreach (Button btn in btns)
            {
                Destroy(btn.gameObject);
            }
        }         
    }

    public void CambiarEscena() {
        SceneManager.LoadScene("Categorias");
    }
}
