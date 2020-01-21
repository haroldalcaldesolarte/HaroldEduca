[System.Serializable]
public class Respuesta
{
    private int id_respuesta;
    private string descripcion;
    private int correcta;
    private int id_preg;

    public Respuesta() { }

    public Respuesta(int id_respuesta, string descripcion, int correcta, int id_preg)
    {
        this.id_respuesta = id_respuesta;
        this.descripcion = descripcion;
        this.correcta = correcta;
        this.id_preg = id_preg;
    }

    public int Id_respuesta { get => id_respuesta; set => id_respuesta = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Correcta { get => correcta; set => correcta = value; }
    public int Id_preg { get => id_preg; set => id_preg = value; }

    public override string ToString() {
        
        return "Id_res: " + Id_respuesta + "|Descripcion: " + Descripcion + " |correcta: " + Correcta + " |id_pregunta: " + Id_preg + "||"; 
    }
}
