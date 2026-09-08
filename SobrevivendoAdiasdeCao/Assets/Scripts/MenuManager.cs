using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip somBotao;

    [Header("Fade")]
    [SerializeField] private Image fade;

    [SerializeField] private float tempoFade = 1.2f;

    private bool carregando = false;

    public void Jogar()
    {
        if (carregando) return;

        carregando = true;

        audioSource.PlayOneShot(somBotao);

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

        // Espera o som terminar
        yield return new WaitForSeconds(somBotao.length);

        SceneManager.LoadScene("Intro1");
    }

    public void Sair()
    {
        Application.Quit();
    }
}