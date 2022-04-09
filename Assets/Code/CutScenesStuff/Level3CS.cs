using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class Level3CS : MonoBehaviour
    {

        [SerializeField] InputProcessor inputProcessor;
        [SerializeField] Image joystick1;
        [SerializeField] Image joystick2;
        [SerializeField] Camera kamera;
        [SerializeField] MultiTextBox firstDialogue;
        [SerializeField] MultiTextBox secondDialogue;
        [SerializeField] MultiTextBox thirdDialogue;
        [SerializeField] MultiTextBox fourthDialogue;
        [SerializeField] MultiTextBox fifthDialogue;
        CameraFollow cameraFollow;
        CharacterControl characterControl;
        public bool skipCutscene;
        [SerializeField] GameObject vahtiMestari;
        SpriteRenderer vmSprite;
        NPC_mover vmMover;
        bool movementDone = false;
        void Start()
        {
            cameraFollow = kamera.GetComponent<CameraFollow>();
            inputProcessor.enabled = false;
            joystick1.enabled = false;
            joystick2.enabled = false;
            characterControl = inputProcessor.GetComponent<CharacterControl>();
            vmSprite = vahtiMestari.GetComponent<SpriteRenderer>();
            vmMover = vahtiMestari.GetComponent<NPC_mover>();
            vmSprite.enabled = false;
            if(!skipCutscene)
            {
                StartCoroutine("StartCutscene");
            }
            else
            {
                inputProcessor.transform.position = new Vector2(-3.5f, 1.8f);
                EnableStuff();
            }
        }

        IEnumerator StartCutscene()
        {
            StartCoroutine("MoveCharacter");
            yield return new WaitForSeconds(1f);
            firstDialogue.BeginText();
            yield return new WaitUntil(() => firstDialogue.TextDone && movementDone);
            movementDone = false;
            yield return new WaitForSeconds(1f);
            StartCoroutine("MoveVM");
            yield return new WaitForSeconds(1f);
            secondDialogue.BeginText();
            yield return new WaitUntil(() => secondDialogue.TextDone && movementDone);
            movementDone = false;
            yield return new WaitForSeconds(1.0f);
            cameraFollow.Target = inputProcessor.transform;
            thirdDialogue.BeginText();
            yield return new WaitUntil(() => thirdDialogue.TextDone);
            yield return new WaitForSeconds(1.0f);
            cameraFollow.Target = vahtiMestari.transform;
            fourthDialogue.BeginText();
            StartCoroutine("MoveVM2");
            yield return new WaitUntil(() => fourthDialogue.TextDone);
            yield return new WaitForSeconds(0.7f);
            cameraFollow.Target = inputProcessor.transform;
            characterControl.MoveInput = Vector2.down;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            fifthDialogue.BeginText();
            yield return new WaitForSeconds(0.7f);
            characterControl.MoveInput = Vector2.left;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            yield return new WaitUntil(() => fifthDialogue.TextDone);
            yield return new WaitForSeconds(1f);
            EnableStuff();
        }

        IEnumerator MoveCharacter()
        {
            Vector2 heroposition = inputProcessor.transform.position;
            while (heroposition != (new Vector2(-3.5f, 1.8f)))
            {
                characterControl.MoveInput = Vector2.up;
                heroposition = Vector2.MoveTowards(heroposition, new Vector2(-3.5f, 1.8f), 1.2f * Time.deltaTime);
                inputProcessor.transform.position = heroposition;
                yield return null;
            }
            characterControl.dontMove = true;
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
            characterControl.MoveInput = Vector2.left;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            yield return new WaitForSeconds(0.5f);
            characterControl.MoveInput = Vector2.down;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            movementDone = true;
        }
        IEnumerator MoveVM()
        {
            vmSprite.enabled = true;
            cameraFollow.Target = vahtiMestari.transform;
            Vector2 vmPos = vahtiMestari.transform.position;
            Vector2 target1 = new Vector2(-1.5f, 2f);
            vmMover.Speed = 1.0f;
            vmMover.MoveInput = Vector2.up;
            while (vmPos != target1)
            {
                vmPos = Vector2.MoveTowards(vmPos, target1, 1.6f * Time.deltaTime);
                vahtiMestari.transform.position = vmPos;
                yield return null;
            }
            characterControl.MoveInput = Vector2.right;
            yield return null;
            characterControl.MoveInput = Vector2.zero;
            vmMover.Speed = 0;
            vmMover.MoveInput = Vector2.left;
            movementDone = true;
        }
        IEnumerator MoveVM2()
        {
            Vector2 vmPos = vahtiMestari.transform.position;
            Vector2 target1 = new Vector2(-1.32f, -3.01f);
            vmMover.Speed = 1.0f;
            vmMover.MoveInput = Vector2.down;
            while (vmPos != target1)
            {
                vmPos = Vector2.MoveTowards(vmPos, target1, 1.6f * Time.deltaTime);
                vahtiMestari.transform.position = vmPos;
                yield return null;
            }
            vmSprite.enabled = false;
            movementDone = true;
        }
        private void EnableStuff()
        {
            characterControl.dontMove = false;
            Destroy(vahtiMestari);
            inputProcessor.enabled = true;
            joystick1.enabled = true;
            joystick2.enabled = true;
        }
    }
}
