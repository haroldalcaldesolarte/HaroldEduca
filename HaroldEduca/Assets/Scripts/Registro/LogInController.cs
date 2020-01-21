﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInController : MonoBehaviour
{
    [SerializeField]private InputField username_field, password_field;
    [SerializeField]private Text m_error;
    [SerializeField] private Button Btnlogin;
    [SerializeField]private GameObject Panel_error;

    GameManager g = GameManager.Instance;

    public void checkPassword()
    {
        Btnlogin.onClick.RemoveAllListeners();
        if (g.CheckConexion())
        {
            g.comprobarCredencialesLogIn(username_field, password_field, m_error);
        }
        else {
            Panel_error.SetActive(true);
        }
    }

    public void Reintentar() {
        Panel_error.SetActive(false);
        StartCoroutine(ReintentarCoroutine());
    }

    IEnumerator ReintentarCoroutine() {
        yield return new WaitForSeconds(2f);
        checkPassword();
    }
}