using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    //TODO: on tällä hetkellä "klikkausten kohteena", sen takia textbox on toistaiseksi yläreunassa
    //muuten se ei hyväksy klikkauksia mitkä menee sen "taakse"
    public class TextBox : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;
        [SerializeField]
        private float fadeSpeed = 1;
        [SerializeField]
        private float onScreenFor = 2;
        private Image image;
        void Start()
        {
            image = GetComponent<Image>();
            text.enabled = false;
            image.enabled = false;
            text.faceColor = new Color32(1, 1, 1, 0);
            image.color = new Color(1, 1, 1, 0);
        }

        public void StartFade()
        {
            image.enabled = true;
            text.enabled = true;
            StartCoroutine(Fade());
        }
        IEnumerator Fade()          //simppeli for-looppi joka fadee textboxin sisään ja sitten ulos muuttamalla sen alpha arvoja
        {
            Color c = image.color;
            for (float alpha = 0f; alpha <= 1; alpha += fadeSpeed * Time.deltaTime)
            {
                c.a = alpha;
                image.color = c;
                text.faceColor = c;
                if (alpha >= 1 - fadeSpeed * Time.deltaTime)    //jotta alpha menee aina tasan yhteen
                {                                               //katsotaan meneekö seuraava loppi yli, ja jos menee pyöristetään yhteen
                    c.a = 1;
                    image.color = c;
                    text.faceColor = c;
                } 
                else                                            //tänne mennään jos alpha ei mene yllä olemaan iffiin
                {
                    yield return null;                          //tästä aloitetaan seuraava looppi seuraavalla framella
                }
            }
            yield return new WaitForSeconds(onScreenFor);       //kun fadein on valmis, odotetaan x-määrä sekuntteja,
            StartCoroutine(FadeOut());                          //jonka jälkeen aloitetaan fadeout
        }
        IEnumerator FadeOut()                                   //Sama kuin fadein, mutta alpha lasketaan pikkuhiljaa yhdestä nollaan
        {
            Color c = image.color;
            for (float alpha = 1f; alpha >= 0; alpha -= fadeSpeed * Time.deltaTime)
            {
                c.a = alpha;
                image.color = c;
                text.faceColor = c;
                if (alpha <= 0 - fadeSpeed * Time.deltaTime)
                {
                    c.a = 0;
                    image.color = c;
                    text.faceColor = c;
                }
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
