using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class Level2CS : MonoBehaviour
    {
        [SerializeField] InputProcessor inputProcessor;
        [SerializeField] Image joystick1;
        [SerializeField] Image joystick2;
        [SerializeField] Camera kamera; //13,4.5
        [SerializeField] MultiTextBox firstDialogue;
        [SerializeField] MultiTextBox secondDialogue;
        [SerializeField] GameObject cameraDestination;
        public bool skipCutscene;
        bool movementDone = false;
        CharacterControl characterControl;
        CameraFollow cameraFollow;
        void Start()
        {
            cameraFollow = kamera.GetComponent<CameraFollow>();
            inputProcessor.enabled = false;
            joystick1.enabled = false;
            joystick2.enabled = false;
            characterControl = inputProcessor.GetComponent<CharacterControl>();
            if(!skipCutscene)
            {
                StartCoroutine("StartCutscene");
            }
            else
            {
                inputProcessor.transform.position = new Vector2(3.5f, 1f);
                EnableStuff();
            }
        }

        IEnumerator StartCutscene()
        {
            StartCoroutine("MoveCharacter");
            secondDialogue.BeginText();
            yield return new WaitUntil(() => movementDone);
            movementDone = false;
            StartCoroutine("MoveCamera");
            yield return new WaitUntil(() => movementDone);
            firstDialogue.BeginText();
            yield return new WaitUntil(() => firstDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            Debug.Log("Cutscene done");
            EnableStuff();
        }

        IEnumerator MoveCamera()
        {
            cameraFollow.Target = cameraDestination.transform;
            yield return new WaitForSeconds(2.0f);
            cameraFollow.Target = inputProcessor.transform;
            yield return new WaitForSeconds(1.0f);
            movementDone = true;
        }
        IEnumerator MoveCharacter()
        {
            Vector2 heroposition = inputProcessor.transform.position;
            while (heroposition != (new Vector2(3.5f, 1f)))
            {
                characterControl.MoveInput = Vector2.right;
                heroposition = Vector2.MoveTowards(heroposition, new Vector2(3.5f, 1f), 1 * Time.deltaTime);
                inputProcessor.transform.position = heroposition;
                yield return null;
            }
            characterControl.MoveInput = Vector2.zero;
            yield return new WaitForSeconds(0.5f);
            movementDone = true;
        }
        private void EnableStuff()
        {
            inputProcessor.enabled = true;
            joystick1.enabled = true;
            joystick2.enabled = true;
        }
    }
}
