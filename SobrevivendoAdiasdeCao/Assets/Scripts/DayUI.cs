using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    public TextMeshProUGUI textoDia;

    void Update()
    {
        textoDia.text =
            "Dia " +
            DayManager.Instance.diaAtual +
            " / " +
            DayManager.Instance.totalDias;
    }
}