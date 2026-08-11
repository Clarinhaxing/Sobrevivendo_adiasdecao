using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private Image fade;

    [SerializeField] private float tempoFade = 1.2f;

    private bool carregando = false;

    public void Jogar()
    {
        if (carregando) return;

        carregando = true;

        StartCoroutine(CarregarIntro());
    }

    IEnumerator CarregarIntro()
    {
        Color cor = fade.color;

        while (cor.a < 1)
        {
            cor.a += Time.deltaTime / tempoFade;
            fade.color = cor;

            yield return null;
        }

        SceneManager.LoadScene("Intro1");
    }

    public void Sair()
    {
        Application.Quit();
    }
}