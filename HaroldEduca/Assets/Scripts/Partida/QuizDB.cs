using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuizDB
{
    public List<Pregunta> ListaPreguntas = null;
    public List<Pregunta> backup = null;

    public QuizDB() {
    }

    public QuizDB(List<Pregunta> ListaPreguntas)
    {
        this.ListaPreguntas = ListaPreguntas;
    }

    public void Awake()
    {
        backup = ListaPreguntas.ToList();
    }

    public Pregunta GetRandom(bool remove = true) {

        int index = Random.Range(0, ListaPreguntas.Count);

        if (!remove)
            return ListaPreguntas[index];

        Pregunta p = ListaPreguntas[index];
        ListaPreguntas.RemoveAt(index);

        return p;
    }

    private void RestoreBackup() {
        ListaPreguntas = backup.ToList();
    }
}
