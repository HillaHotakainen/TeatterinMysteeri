using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class Level4CS : MonoBehaviour
    {
        [SerializeField] InputProcessor inputProcessor;
        [SerializeField] Image joystick1;
        [SerializeField] Image joystick2;
        [SerializeField] Camera kamera;
        [SerializeField] MultiTextBox firstDialogue;
        [SerializeField] bool skipCutscene = false;
        [SerializeField] GameObject cameraDestination;
        CameraFollow cameraFollow;
        CharacterControl characterControl;
        void Start()
        {
            cameraFollow = kamera.GetComponent<CameraFollow>();
            joystick1.enabled = false;
            joystick2.enabled = false;
            inputProcessor.enabled = false;
            characterControl = inputProcessor.GetComponent<CharacterControl>();
            skipCutscene = PlayerPrefs.GetInt("Level4Cutscene") == 1;
            if (!skipCutscene)
            {
                StartCoroutine("StartCutscene");
            }
            else
            {
                EnableStuff();
            }
        }

        IEnumerator StartCutscene()
        {
            yield return new WaitForSeconds(0.5f);
            characterControl.MoveInput = Vector2.up;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            yield return null;
            yield return new WaitForSeconds(0.5f);
            cameraFollow.Target = cameraDestination.transform;
            yield return new WaitForSeconds(3.0f);
            cameraFollow.Target = inputProcessor.transform;
            yield return new WaitForSeconds(0.5f);
            firstDialogue.BeginText();
            characterControl.MoveInput = Vector2.down;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            yield return null;
            yield return new WaitUntil(() => firstDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            EnableStuff();
        }
        void EnableStuff()
        {
            joystick1.enabled = true;
            joystick2.enabled = true;
            inputProcessor.enabled = true;
            PlayerPrefs.SetInt("Level4Cutscene", 1);
        }
    }
}
