using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro1Manager : MonoBehaviour
{
    public Image fade;
    public float tempoCutscene = 5f;
    public float tempoFade = 1f;

    private bool pulou = false;

    void Start()
    {
        StartCoroutine(Intro());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pulou)
        {
            pulou = true;
            StopAllCoroutines();
            StartCoroutine(CarregarProximaCena());
        }
    }

    IEnumerator Intro()
    {
        // Fade In
        Color c = fade.color;

        while (c.a > 0)
        {
            c.a -= Time.deltaTime / tempoFade;
            fade.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(tempoCutscene);

        yield return StartCoroutine(CarregarProximaCena());
    }

    IEnumerator CarregarProximaCena()
    {
        Color c = fade.color;

        while (c.a < 1)
        {
            c.a += Time.deltaTime / tempoFade;
            fade.color = c;
            yield return null;
        }

        SceneManager.LoadScene("Intro2");
    }
}