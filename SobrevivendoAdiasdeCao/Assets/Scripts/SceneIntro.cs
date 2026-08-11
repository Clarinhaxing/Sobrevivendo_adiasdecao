using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneIntro : MonoBehaviour
{
    [Header("Painel")]
    public Image background;

    [Header("Textos")]
    public RectTransform nomeFase;
    public RectTransform subtitulo;
    public RectTransform objetivo;

    public TextMeshProUGUI textoNome;
    public TextMeshProUGUI textoSubtitulo;
    public TextMeshProUGUI textoObjetivo;

    public TextMeshProUGUI countdown;

    [Header("Informações")]
    public string fase = "MERCADO";
    public string subtituloFase = "Dia 1 • Manhã";
    public string objetivoFase = "OBJETIVO: Buscar alimento para a comunidade.";

    [Header("Velocidades")]
    public float velocidadeEntrada = 8f;
    public float velocidadeSaida = 10f;

    Vector2 posNome;
    Vector2 posSub;
    Vector2 posObj;

    void Start()
    {
        Time.timeScale = 0;

        textoNome.text = fase;
        textoSubtitulo.text = subtituloFase;
        textoObjetivo.text = objetivoFase;

        posNome = nomeFase.anchoredPosition;
        posSub = subtitulo.anchoredPosition;
        posObj = objetivo.anchoredPosition;

        nomeFase.anchoredPosition += Vector2.left * 1200;
        subtitulo.anchoredPosition += Vector2.left * 1200;
        objetivo.anchoredPosition += Vector2.left * 1200;

        countdown.gameObject.SetActive(false);

        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        Color c = background.color;
        c.a = 1;
        background.color = c;

        yield return new WaitForSecondsRealtime(.3f);

        // Fade
        while (background.color.a > .45f)
        {
            c.a -= Time.unscaledDeltaTime;
            background.color = c;
            yield return null;
        }

        // Entrada dos textos
        yield return StartCoroutine(Mover(nomeFase, posNome));

        yield return new WaitForSecondsRealtime(.15f);

        yield return StartCoroutine(Mover(subtitulo, posSub));

        yield return new WaitForSecondsRealtime(.15f);

        yield return StartCoroutine(Mover(objetivo, posObj));

        yield return new WaitForSecondsRealtime(1.4f);

        // Saída dos textos
        yield return StartCoroutine(SairDireita(nomeFase));
        yield return StartCoroutine(SairDireita(subtitulo));
        yield return StartCoroutine(SairDireita(objetivo));

        // Contagem
        yield return StartCoroutine(Contagem());

        // Fade final
        while (background.color.a > 0)
        {
            c.a -= Time.unscaledDeltaTime * 2;
            background.color = c;
            yield return null;
        }

        gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    IEnumerator Mover(RectTransform alvo, Vector2 destino)
    {
        while (Vector2.Distance(alvo.anchoredPosition, destino) > 1)
        {
            alvo.anchoredPosition =
                Vector2.Lerp(
                    alvo.anchoredPosition,
                    destino,
                    velocidadeEntrada * Time.unscaledDeltaTime);

            yield return null;
        }

        alvo.anchoredPosition = destino;
    }

    IEnumerator SairDireita(RectTransform alvo)
    {
        Vector2 destino = alvo.anchoredPosition + Vector2.right * 1800;

        while (Vector2.Distance(alvo.anchoredPosition, destino) > 5)
        {
            alvo.anchoredPosition =
                Vector2.Lerp(
                    alvo.anchoredPosition,
                    destino,
                    velocidadeSaida * Time.unscaledDeltaTime);

            yield return null;
        }

        alvo.gameObject.SetActive(false);
    }

    IEnumerator Contagem()
    {
        countdown.gameObject.SetActive(true);

        string[] numeros =
        {
            "3",
            "2",
            "1",
            "VAI!"
        };

        foreach (string numero in numeros)
        {
            countdown.text = numero;

            countdown.transform.localScale = Vector3.one * 2f;

            float tempo = 0;

            while (tempo < 1)
            {
                tempo += Time.unscaledDeltaTime * 6;

                countdown.transform.localScale =
                    Vector3.Lerp(
                        Vector3.one * 2f,
                        Vector3.one,
                        tempo);

                yield return null;
            }

            yield return new WaitForSecondsRealtime(.6f);
        }

        countdown.gameObject.SetActive(false);
    }
}