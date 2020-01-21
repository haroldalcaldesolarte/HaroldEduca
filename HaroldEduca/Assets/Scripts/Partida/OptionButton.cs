using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class OptionButton : MonoBehaviour
{
    private Text r_text = null;
    private Button btn = null;
    private Image image = null;
    private Color color_original = Color.black;

    public Respuesta Respuesta { get; set; }

    private void Awake()
    {
        btn = GetComponent<Button>();
        image = GetComponent<Image>();
        r_text = transform.GetChild(0).GetComponent<Text>();
        color_original = Color.white;
    }

    public void constructorOP(Respuesta respuesta, Action<OptionButton> callback) {
        Awake();
        r_text.text = respuesta.Descripcion;
        btn.onClick.RemoveAllListeners();
        btn.enabled = true;
        image.color = color_original;

        Respuesta = respuesta;

        btn.onClick.AddListener(delegate
        {
            callback(this);
        });
    }

    public void SetColor(Color c) {
        btn.enabled = false;
        image.color = c;
    }
}
