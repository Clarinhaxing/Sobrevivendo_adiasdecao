using UnityEngine;
using System.Collections;

public class DialogueBubble : MonoBehaviour
{
    RectTransform rect;
    Vector2 posicaoInicial;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        posicaoInicial = rect.anchoredPosition;
    }

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(PopAnimation());
    }

    IEnumerator PopAnimation()
    {
        rect.localScale = Vector3.zero;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 8f;

            rect.localScale = Vector3.Lerp(
                Vector3.zero,
                Vector3.one,
                t
            );

            yield return null;
        }

        rect.localScale = Vector3.one;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * 3f) * 5f;

        rect.anchoredPosition = posicaoInicial + Vector2.up * y;
    }
}