using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnListButton : MonoBehaviour
{
    [SerializeField] private Text myText;
    [SerializeField] private BtnListControl btnControl;

   private string myTextString;

    public void SetText(string textString) {
        myTextString = textString;
        myText.text = textString;
    }

    public void OnClick() {
        //Debug.Log(myTextString);
        btnControl.BtnClicked(myTextString);
    }
}
