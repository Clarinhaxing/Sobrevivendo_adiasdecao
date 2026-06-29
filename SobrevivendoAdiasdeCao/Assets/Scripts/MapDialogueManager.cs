using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MapDialogueManager : MonoBehaviour
{
    [Header("Referências")]
    public GameObject balaoDialogo;
    public TextMeshProUGUI textoDialogo;

    [Header("Configurações")]
    public float tempoEntreEventos = 8f;
    public float velocidadeDigitacao = 0.03f;
    public float tempoMensagem = 3f;

    bool mostrandoMensagem = false;

    void Start()
    {
        StartCoroutine(SistemaEventos());
    }

    IEnumerator SistemaEventos()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            if (!mostrandoMensagem)
            {
                string mensagem = EscolherMensagem();

                if (!string.IsNullOrEmpty(mensagem))
                {
                    yield return StartCoroutine(MostrarMensagem(mensagem));
                }
            }

            yield return new WaitForSeconds(tempoEntreEventos);
        }
    }

    string EscolherMensagem()
    {
        CommunityManager c = CommunityManager.instance;

        if (c == null)
            return "Olá Sandy!";

        List<string> mensagens = new List<string>();

        // FOME
        if (c.alimentacao < 20)
        {
            mensagens.Add("Essa não! A comunidade está morrendo de fome!");
            mensagens.Add("Precisamos conseguir comida imediatamente!");
        }
        else if (c.alimentacao < 50)
        {
            mensagens.Add("A comida está acabando...");
        }

        // SAÚDE
        if (c.saude < 20)
        {
            mensagens.Add("Há muitos cães doentes!");
            mensagens.Add("Precisamos de medicamentos!");
        }
        else if (c.saude < 50)
        {
            mensagens.Add("A saúde da comunidade está piorando.");
        }

        // FELICIDADE
        if (c.felicidade < 20)
        {
            mensagens.Add("Os cães perderam a esperança...");
        }
        else if (c.felicidade < 50)
        {
            mensagens.Add("A comunidade está ficando triste.");
        }

        // Tudo bem
        if (mensagens.Count == 0)
        {
            mensagens.Add("A comunidade está indo muito bem!");
            mensagens.Add("Continue ajudando todos!");
            mensagens.Add("Hoje parece um ótimo dia!");
        }

        return mensagens[Random.Range(0, mensagens.Count)];
    }

    IEnumerator MostrarMensagem(string mensagem)
    {
        mostrandoMensagem = true;

        balaoDialogo.SetActive(true);

        textoDialogo.text = "";

        RectTransform rect = balaoDialogo.GetComponent<RectTransform>();

        rect.localScale = Vector3.zero;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 6f;
            rect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }

        rect.localScale = Vector3.one;

        foreach (char letra in mensagem)
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }

        yield return new WaitForSeconds(tempoMensagem);

        t = 1;

        while (t > 0)
        {
            t -= Time.deltaTime * 6f;
            rect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }

        balaoDialogo.SetActive(false);

        mostrandoMensagem = false;
    }
}