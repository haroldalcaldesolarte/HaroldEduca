using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Categoria
{
    private int id_cat;
    private string nombre_cat;
    private int id_cat_padre;

    public Categoria() {
    }

    public Categoria (int id_cat, string nombre_cat, int id_cat_padre)
    {
        this.id_cat = id_cat;
        this.nombre_cat = nombre_cat;
        this.id_cat_padre = id_cat_padre;
    }

    public int Id_cat {
        get => id_cat;
        set => id_cat = value;
    }

    public string Nombre_cat {
        get => nombre_cat;
        set => nombre_cat = value;
    }

    public int Id_cat_padre
    {
        get => id_cat_padre;
        set => id_cat_padre = value;
    }

    public override string ToString()
    {
        return "Id_Categoria: " + Id_cat + "|Nombre: " + Nombre_cat +"|Id_padre: "+ Id_cat_padre ;
    }
}
