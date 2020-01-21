using UnityEngine;
using UnityEngine.UI;

public class VolumenController : MonoBehaviour
{
    [SerializeField] private Slider volumen;
    void Start()
    {
        volumen.value = GameManager.Instance.GetVolumen();
    }

    // Update is called once per frame
    public void setVolumen() 
    {
        GameManager.Instance.SetVolumen(volumen.value);
    }
}
