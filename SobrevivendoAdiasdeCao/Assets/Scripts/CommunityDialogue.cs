using UnityEngine;
using TMPro;
using System.Collections;

public class CommunityDialogue : MonoBehaviour
{
    [Header("Referências")]
    public GameObject balao;
    public TextMeshProUGUI textoBalao;

    [Header("Configuração")]
    public float tempoVisivel = 4f;
    public float velocidadeAnimacao = 8f;

    private bool exibindo = false;

    public void MostrarMensagem(string mensagem)
    {
        if (!exibindo)
            StartCoroutine(AnimarBalao(mensagem));
    }

    IEnumerator AnimarBalao(string mensagem)
    {
        exibindo = true;

        balao.SetActive(true);
        textoBalao.text = mensagem;

        RectTransform rect = balao.GetComponent<RectTransform>();

        // Começa invisível
        rect.localScale = Vector3.zero;

        // === ANIMAÇÃO DE ENTRADA ===
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * velocidadeAnimacao;

            // efeito pop (passa um pouco do tamanho)
            float escala = Mathf.Lerp(0f, 1.15f, t);
            rect.localScale = Vector3.one * escala;

            yield return null;
        }

        // Volta suavemente para 1
        while (rect.localScale.x > 1f)
        {
            rect.localScale = Vector3.Lerp(
                rect.localScale,
                Vector3.one,
                Time.deltaTime * velocidadeAnimacao
            );

            yield return null;
        }

        // === FLUTUAÇÃO ===
        Vector2 posInicial = rect.anchoredPosition;
        float tempo = 0;

        while (tempo < tempoVisivel)
        {
            tempo += Time.deltaTime;

            float offset = Mathf.Sin(Time.time * 3f) * 5f;
            rect.anchoredPosition = posInicial + new Vector2(0, offset);

            yield return null;
        }

        // === ANIMAÇÃO DE SAÍDA ===
        while (rect.localScale.x > 0.05f)
        {
            rect.localScale = Vector3.Lerp(
                rect.localScale,
                Vector3.zero,
                Time.deltaTime * velocidadeAnimacao
            );

            yield return null;
        }

        rect.localScale = Vector3.zero;
        rect.anchoredPosition = posInicial;
        balao.SetActive(false);

        exibindo = false;
    }
}