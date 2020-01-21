using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListControl : MonoBehaviour
{
    [SerializeField] private GameObject TextTemplate;
    [SerializeField] private GameObject PanelErrorPartidas;
    private List<Partida> lsPartidas = null;

    private void Start()
    {
        lsPartidas = GameManager.Instance.GetRanking();
        if (lsPartidas == null)
        {
            PanelErrorPartidas.SetActive(true);
        }
        else {
            foreach (Partida p in lsPartidas)
            {
                GameObject text = Instantiate(TextTemplate) as GameObject;
                text.SetActive(true);
                text.GetComponent<ItemListItem>().SetText(p.ToString());

                text.transform.SetParent(TextTemplate.transform.parent, false);
            }
        }

    }
}
