using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partida
{
    private string username;
    private int puntos;
    private int id_categoria;


    public Partida()
    {
    }

    public Partida(string username, int puntos, int id_categoria)
    {
        this.username = username;
        this.puntos = puntos;
        this.id_categoria = id_categoria;
    }

    public string Username { get => username; set => username = value; }
    public int Puntos { get => puntos; set => puntos = value; }
    public int Id_categoria { get => id_categoria; set => id_categoria = value; }

    public override string ToString() {
        string nombre_cat = GameManager.Instance.GetNombreCategoria(Id_categoria);
        if (nombre_cat.Length > 25)
        {
            return Username.PadRight(15, ' ').ToLower() + Puntos.ToString().PadLeft(4, ' ').PadRight(4, ' ') + "  " + nombre_cat.Substring(0,25)+"...";
        }
        else {
            return Username.PadRight(15, ' ').ToLower() + Puntos.ToString().PadLeft(4, ' ').PadRight(4, ' ') + "  " + nombre_cat;

        }
    }
}
