using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TeatterinMysteeri
{
    public class EndingCS : MonoBehaviour
    {
        [SerializeField] InputProcessor inputProcessor;
        [SerializeField] SpriteRenderer doorClose;
        [SerializeField] Sprite doorOpen;
        [SerializeField] Camera kamera;
        [SerializeField] MultiTextBox firstDialogue;
        [SerializeField] MultiTextBox secondDialogue;
        [SerializeField] MultiTextBox thirdDialogue;
        [SerializeField] MultiTextBox fourthDialogue;
        [SerializeField] MultiTextBox fifthDialogue;
        [SerializeField] MultiTextBox sixthDialogue;
        [SerializeField] MultiTextBox seventhDialogue;
        [SerializeField] MultiTextBox eigthDialogue;
        [SerializeField] Sprite standingUp;
        [SerializeField] SpriteRenderer vahtiMestari;
        [SerializeField] GameObject kummitus;
        [SerializeField] RawImage dark;
        [SerializeField] TMP_Text endText;
        [SerializeField] GameObject backButton;
        CameraFollow cameraFollow;
        CharacterControl characterControl;
        bool movementDone = false;

        void Start()
        {
        cameraFollow = kamera.GetComponent<CameraFollow>();
        characterControl = inputProcessor.GetComponent<CharacterControl>();
        cameraFollow.enabled = false;
        inputProcessor.enabled = false;
        endText.enabled = false;
        backButton.SetActive(false);
        StartCoroutine("StartCutscene");
        }
        IEnumerator StartCutscene()
        {
            StartCoroutine("MoveCharacter");
            yield return new WaitForSeconds(0.3f);
            firstDialogue.BeginText();
            yield return new WaitUntil(() => firstDialogue.TextDone && movementDone);
            yield return new WaitForSeconds(1f);
            movementDone = false;
            cameraFollow.enabled = true;
            cameraFollow.Target = vahtiMestari.transform;
            yield return new WaitForSeconds(1.0f);
            secondDialogue.BeginText();
            yield return new WaitUntil(() => secondDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            cameraFollow.Target = kummitus.transform;
            thirdDialogue.BeginText();
            doorClose.sprite = doorOpen;
            StartCoroutine("MoveCharacter2");
            yield return new WaitUntil(() => thirdDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            cameraFollow.Target = inputProcessor.transform;
            fourthDialogue.BeginText();
            yield return new WaitUntil(() => fourthDialogue.TextDone);
            vahtiMestari.sprite = standingUp;
            yield return new WaitForSeconds(1.0f);
            cameraFollow.Target = vahtiMestari.transform;
            fifthDialogue.BeginText();
            yield return new WaitUntil(() => fifthDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            cameraFollow.Target = inputProcessor.transform;
            sixthDialogue.BeginText();
            yield return new WaitUntil(() => sixthDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            cameraFollow.Target = vahtiMestari.transform;
            seventhDialogue.BeginText();
            yield return new WaitUntil(() => seventhDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            cameraFollow.Target = inputProcessor.transform;
            eigthDialogue.BeginText();
            yield return new WaitUntil(() => eigthDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            StartCoroutine("Fade");
        }
        IEnumerator MoveCharacter()
        {
            Vector2 heroposition = inputProcessor.transform.position;
            while (heroposition != (new Vector2(-0.5f, -0.8f)))
            {
                characterControl.MoveInput = Vector2.up;
                heroposition = Vector2.MoveTowards(heroposition, new Vector2(-0.5f, -0.8f), 2 * Time.deltaTime);
                inputProcessor.transform.position = heroposition;
                yield return null;
            }
            characterControl.MoveInput = Vector2.zero;
            yield return null;
            movementDone = true;
        }
        IEnumerator MoveCharacter2()
        {
            Vector2 heroposition = inputProcessor.transform.position;
            while (heroposition != (new Vector2(-0.5f, 7.25f)))
            {
                characterControl.MoveInput = Vector2.up;
                heroposition = Vector2.MoveTowards(heroposition, new Vector2(-0.5f, 7.25f), 2 * Time.deltaTime);
                inputProcessor.transform.position = heroposition;
                yield return null;
            }
            characterControl.MoveInput = Vector2.zero;
            yield return null;
            characterControl.MoveInput = Vector2.right;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            yield return null;
            movementDone = true;
        }
        IEnumerator Fade()
        {
            Color c = dark.color;
            for (float alpha = 0f; alpha <= 1; alpha +=  Time.deltaTime)
            {
                c.a = alpha;
                dark.color = c;
                if (alpha >= 1 -  Time.deltaTime)
                {
                    c.a = 1;
                    dark.color = c;
                } 
                else
                {
                    yield return null;
                }
            }
            yield return new WaitForSeconds(1.0f);
            endText.enabled = true;
            yield return new WaitForSeconds(1.0f);
            backButton.SetActive(true);
        }
    }
}
