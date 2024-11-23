using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int PlayerHealthPoints = 100;
    public GameObject VirusScreen;

    public void PlayerTakeDamage(int PlayerDamageAmount)
    {
        PlayerHealthPoints -= PlayerDamageAmount;

        if (PlayerHealthPoints <= 0)
        {
            print("ERROR 404");
        }
        else
        {
            print("WARNING ANTIVIRUS FOUND");
            StartCoroutine(DamageScreenEffect());
        }
    }

    private IEnumerator DamageScreenEffect()
    {
        if (VirusScreen.activeInHierarchy == false)
        {
            VirusScreen.SetActive(true);
        }

        var image = VirusScreen.GetComponentInChildren<Image>();

        Color startColor = image.color;
        startColor.a = 0.5f;
        image.color = startColor;

        float Duration = 1f;
        float TimeElapsed = 0f;

        while (TimeElapsed < Duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, TimeElapsed / Duration);

            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            TimeElapsed += Time.deltaTime;

            yield return null;
        }

        

        if (VirusScreen.activeInHierarchy)
        {
            VirusScreen.SetActive(false);
        }
    }


}
