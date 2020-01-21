
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private Text texto_pregunta = null;
    [SerializeField] private List<OptionButton> btnList = null;

    public void Constructor(Pregunta p, Action<OptionButton> callback ) {
        texto_pregunta.text = p.Descripcion;
        for (int n = 0; n < btnList.Count ; n++) {
            btnList[n].constructorOP(p.Respuestas[n], callback);
        }
    }


}
