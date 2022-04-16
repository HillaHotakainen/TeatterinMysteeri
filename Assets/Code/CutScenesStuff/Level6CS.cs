using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class Level6CS : MonoBehaviour
    {
        [SerializeField] InputProcessor inputProcessor;
        [SerializeField] Image joystick1;
        [SerializeField] Image joystick2;
        [SerializeField] Camera kamera;
        [SerializeField] MultiTextBox firstDialogue;
        [SerializeField] MultiTextBox secondDialogue;
        [SerializeField] bool skipCutscene = false;
        [SerializeField] GameObject cameraTarget;
        CameraFollow cameraFollow;
        private CharacterControl characterControl;
        void Start()
        {
            cameraFollow = kamera.GetComponent<CameraFollow>();
            characterControl = inputProcessor.GetComponent<CharacterControl>();
            inputProcessor.enabled = false;
            joystick1.enabled = false;
            joystick2.enabled = false;
            cameraFollow.enabled = false;
            characterControl.dontMove = true;
            skipCutscene = PlayerPrefs.GetInt("Level6Cutscene") == 1;
            if(!skipCutscene)
            {
                StartCoroutine("StartCutscene");
            }
            else
            {
                inputProcessor.transform.position = new Vector2(-1.5f, 0f);
                EnableStuff();
            }
        }
        IEnumerator StartCutscene()
        {
            yield return null;
            StartCoroutine("MoveCharacter");
            firstDialogue.BeginText();
            yield return new WaitUntil(() => firstDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            cameraFollow.enabled = true;
            cameraFollow.Target = cameraTarget.transform;
            yield return new WaitForSeconds(2.0f);
            cameraFollow.Target = inputProcessor.transform;
            yield return new WaitForSeconds(1.0f);
            secondDialogue.BeginText();
            yield return new WaitForSeconds(1.0f);
            characterControl.MoveInput = Vector2.down;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            yield return null;
            yield return new WaitUntil(() => secondDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            EnableStuff();
        }

        IEnumerator MoveCharacter()
        {
            Vector2 heroposition = inputProcessor.transform.position;
            while (heroposition != (new Vector2(-1.5f, 0f)))
            {
                characterControl.MoveInput = Vector2.up;
                heroposition = Vector2.MoveTowards(heroposition, new Vector2(-1.5f, 0f), 2 * Time.deltaTime);
                inputProcessor.transform.position = heroposition;
                yield return null;
            }
            characterControl.MoveInput = Vector2.right;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            yield return new WaitForSeconds(0.5f);
            characterControl.MoveInput = Vector2.left;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            yield return new WaitForSeconds(0.5f);
            characterControl.MoveInput = Vector2.right;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            yield return new WaitForSeconds(0.5f);
        }
        void EnableStuff()
        {
            inputProcessor.enabled = true;
            joystick1.enabled = true;
            joystick2.enabled = true;
            cameraFollow.enabled = true;
            characterControl.dontMove = false;
            PlayerPrefs.SetInt("Level6Cutscene", 1);
        }
    }
}
