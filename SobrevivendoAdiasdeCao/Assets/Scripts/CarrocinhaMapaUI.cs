using UnityEngine;

public class CarrocinhaMapaUI : MonoBehaviour
{
    public RectTransform[] rota;
    public float speed = 200f;

    private RectTransform rt;
    private int alvo = 0;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = rota[0].anchoredPosition;
    }

    void Update()
    {
        rt.anchoredPosition = Vector2.MoveTowards(
            rt.anchoredPosition,
            rota[alvo].anchoredPosition,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(rt.anchoredPosition, rota[alvo].anchoredPosition) < 5f)
        {
            alvo++;

            if (alvo >= rota.Length)
                alvo = 0;
        }
    }
}