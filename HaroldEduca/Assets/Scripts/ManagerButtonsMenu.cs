using UnityEngine;
using UnityEngine.UI;

public class ManagerButtonsMenu : MonoBehaviour
{
    [SerializeField] private Text nombre_usuario;
    GameManager g = GameManager.Instance;
    // Start is called before the first frame update
    private void Start()
    {
        g.ActualizarNombre(nombre_usuario);
    }

    public void click_Btn(string nombreEscena) {
        g.CambiarEscena(nombreEscena);
    }

    public void click_Btn_LogOut(string nombreEscena)
    {
        g.CambiarEscena(nombreEscena);
        PlayerPrefs.SetString("LastUserLoggin", "");
    }

    public void click_salirJuego() {
        g.SalirJuego();
    }
}
