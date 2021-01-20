using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{
    public Text subtitles;
    public GameObject titulky;
    public GameObject napoveda;

    private void Start()
    {
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence() {
        yield return new WaitForSeconds(5.5f);
        subtitles.text = "Ahoj! Vítam tě ve hře na schovávanou, ja jsem Pepper";
        yield return new WaitForSeconds(6);
        subtitles.text = "Jsem robotem vytvořeným v laboratořích této školy.";
        yield return new WaitForSeconds(5);
        subtitles.text = "Tohle je Fluffy tvuj pomocník při plnění úkolů.";
        yield return new WaitForSeconds(5);
        subtitles.text = "První úkol už čeká.";
        yield return new WaitForSeconds(4);
        subtitles.text = "Můžeme začít.";
        yield return new WaitForSeconds(1);
        subtitles.text = "Následuj Fluffyho.";
        yield return new WaitForSeconds(2);
        subtitles.text = "";

        yield return new WaitForSeconds(8);
        subtitles.text = "Jejda! To je ale nadělení.";
        yield return new WaitForSeconds(3);
        subtitles.text = "Naše robotická kočka se rozbila.";
        yield return new WaitForSeconds(4);
        subtitles.text = "Musíme to opravit.";
        yield return new WaitForSeconds(2);
        subtitles.text = "Potřeboval bych tvojí pomoc při hledání nových součástek.";
        yield return new WaitForSeconds(7);
        subtitles.text = "Napověda je v levém horním rohu. Musíš to zvládnout sám bez Flufyho.";
        napoveda.SetActive(true);
        yield return new WaitForSeconds(6);
        titulky.SetActive(false);
    }
}
