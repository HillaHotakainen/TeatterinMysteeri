
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TeatterinMysteeri
{
    public class TypeWriterEffect : MonoBehaviour
    {
        TMP_Text _tmpProText;
        string writer;
        GameObject textBox;

        [SerializeField] float delayBeforeStart = 0f;
        [SerializeField] float timeBetweenCharacters = 0.1f;
        [SerializeField] float secondsBeforeDestroy = 2.0f;
        bool textDone = false;
        bool startDestroy = false;
        void Start()
        {
            _tmpProText = GetComponent<TMP_Text>()!;    //Hakee tmp-tekstin ja textboxin muuttujiin
            textBox = this.transform.parent.gameObject;
            
            writer = _tmpProText.text;                  //Pistää alkuperäisen tekstin muuttujaan
            _tmpProText.text = "";                      //ja tyhjentää tmp:n tekstikentän
            StartCoroutine("TypeWriterTMP");
        }

        void Update()
        {
            if(writer.Equals(_tmpProText.text))     //jos teksti on lisätty kokonaisuudessaan
            {
                textDone = true;
            }
            if(textDone && !startDestroy)           //poistetaan textbox
            {
                Debug.Log("Destroying textbox.");
                StartCoroutine("DestroyTextbox");
                startDestroy = true;                //startDestroy booleania käytetään ettei coroutinea aloiteta joka framella
            }
        }

        IEnumerator TypeWriterTMP()
        {
            yield return new WaitForSeconds(delayBeforeStart);      //odottaa alussa jos muuttuja > 0

            foreach (char c in writer)              //ottaa jokaisen kirjaimen ja lisää ne kerrallaan
            {
                //if (_tmpProText.text.Length > 0)      //en ole satavarma mitä tämä if-lause tekee joten kommentoin sen pois
                //{                                     //saattaa olla tärkeä osille mitä muokkasin pois muiden koodista
                //    _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length);
                //}
                _tmpProText.text += c;
                yield return new WaitForSeconds(timeBetweenCharacters);     //
            }
        }
        IEnumerator DestroyTextbox()
        {
            yield return new WaitForSeconds(secondsBeforeDestroy); //odottaa tässä ja poistaa sen jälkeen textboxin ja tekstin
            Destroy(textBox);
        }
    }
}