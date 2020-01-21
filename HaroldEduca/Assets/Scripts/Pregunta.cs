using UnityEngine;
using System.Collections.Generic;

public class Pregunta
{
    private int id_pregunta;
    private string descripcion;
    private int dificultad;
    private int id_categoria;
    private List<Respuesta> respuestas;

    public Pregunta() {
    }

    public Pregunta(int id_pregunta, string descripcion, int dificultad, int id_categoria)
    {
        this.Id_pregunta = id_pregunta;
        this.Descripcion = descripcion;
        this.Dificultad = dificultad;
        this.Id_categoria = id_categoria;
    }

    public Pregunta(int id_pregunta, string descripcion, int dificultad, int id_categoria, List<Respuesta> respuestas)
    {
        this.Id_pregunta = id_pregunta;
        this.Descripcion = descripcion;
        this.Dificultad = dificultad;
        this.Id_categoria = id_categoria;
        this.Respuestas = respuestas;
    }

    public int Id_pregunta { get => id_pregunta; set => id_pregunta = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Dificultad { get => dificultad; set => dificultad = value; }
    public int Id_categoria { get => id_categoria; set => id_categoria = value; }
    public List<Respuesta> Respuestas { get => respuestas; set => respuestas = value; }

    public override string ToString()
    {
        List<Respuesta> listaR = Respuestas;
        string respuestas = "";
        foreach (Respuesta r in listaR) {
            respuestas = respuestas + r.Id_respuesta + "--" + r.Descripcion + "\n";
        }
        return "Id_pregunta: " + Id_pregunta + "\nPregunta: " + Descripcion + "\nDificultad: "+ Dificultad + "\nId_categoria: "+ Id_categoria + "\n Respuestas: " + respuestas;
    }

}
