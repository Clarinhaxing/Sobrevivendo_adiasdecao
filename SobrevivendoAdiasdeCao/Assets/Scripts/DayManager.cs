using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    [Header("Dias")]
    public int diaAtual = 1;
    public int totalDias = 15;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ProximoDia()
    {
        if (diaAtual < totalDias)
        {
            diaAtual++;
            Debug.Log("Novo dia: " + diaAtual);
        }
        else
        {
            Debug.Log("Último dia!");
        }
    }
}