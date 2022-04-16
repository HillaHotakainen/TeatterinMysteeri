using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TeatterinMysteeri
{
    public class Level1CS : MonoBehaviour
    {
        [SerializeField] InputProcessor inputProcessor;
        [SerializeField] Image joystick1;
        [SerializeField] Image joystick2;
        [SerializeField] Camera kamera;
        [SerializeField] MultiTextBox firstDialogue;
        [SerializeField] MultiTextBox secondDialogue;
        [SerializeField] GameObject ghost;
        [SerializeField] bool skipCutscene = false;
        CameraFollow cameraFollow;
        bool movementDone = false;
        private CharacterControl characterControl;
        [SerializeField] Sprite lookup;
        SpriteRenderer spriteRenderer1;
        Sprite original;
        [SerializeField] Sprite sitting;
        [SerializeField] Sprite sitSleep;
        [SerializeField] GameObject zzz;
        [SerializeField] TextBox tutorial;
        Animator animator;
        NPC_mover ghostMover;
        void Start()
        {
            cameraFollow = kamera.GetComponent<CameraFollow>();
            characterControl = inputProcessor.GetComponent<CharacterControl>();
            spriteRenderer1 = inputProcessor.GetComponent<SpriteRenderer>();
            original = spriteRenderer1.sprite;
            animator = spriteRenderer1.GetComponent<Animator>();
            ghostMover = ghost.GetComponent<NPC_mover>();
            inputProcessor.enabled = false;
            joystick1.enabled = false;
            joystick2.enabled = false;
            cameraFollow.enabled = false;
            characterControl.dontMove = true;
            animator.enabled = false;
            spriteRenderer1.sprite = sitSleep;
            skipCutscene = PlayerPrefs.GetInt("Level1Cutscene") == 1;
            if (!skipCutscene)
            {
                StartCoroutine(Cutscene());
            }
            else
            {
                Destroy(ghost);
                inputProcessor.transform.position = new Vector3(-0.5f, -0.2f, 0f);
                EnableStuff();
            }
        }
        IEnumerator WaitFrame()
        {
            yield return null;
            tutorial.StartFade();
        }

        IEnumerator Cutscene()
        {
            yield return null;
            firstDialogue.BeginText();
            yield return new WaitForSeconds(2.0f);
            zzz.SetActive(false);
            spriteRenderer1.sprite = sitting;
            yield return new WaitUntil(() => firstDialogue.TextDone);
            spriteRenderer1.sprite = original;
            animator.enabled = true;
            StartCoroutine(MoveCharacter());
            yield return new WaitUntil(() => movementDone);
            movementDone = false;
            StartCoroutine(MoveGhost());
            yield return new WaitUntil(() => movementDone);
            movementDone = false;
            cameraFollow.Target = inputProcessor.transform;
            Destroy(ghost);
            animator.enabled = true;
            spriteRenderer1.sprite = original;
            secondDialogue.BeginText();
            yield return new WaitUntil(() => secondDialogue.TextDone);
            yield return new WaitForSeconds(secondDialogue.SecondsBfrDestroy);
            EnableStuff();
        }

        IEnumerator MoveCharacter()
        {
            Vector2 heroposition = inputProcessor.transform.position;
            while (heroposition != (new Vector2(-0.5f, -0.2f)))
            {
                characterControl.MoveInput = Vector2.down;
                heroposition = Vector2.MoveTowards(heroposition, new Vector2(-0.5f, -0.2f), 2 * Time.deltaTime);
                inputProcessor.transform.position = heroposition;
                yield return null;
            }
            characterControl.MoveInput = Vector2.zero;
            animator.enabled = false;
            spriteRenderer1.sprite = lookup;
            yield return new WaitForSeconds(0.5f);
            movementDone = true;
        }

        IEnumerator MoveGhost()
        {
            cameraFollow.enabled = true;
            ghostMover.Speed = 1.0f;
            ghostMover.MoveInput = Vector2.right;
            cameraFollow.Target = ghost.transform;
            Vector2 ghostPosition = ghost.transform.position;
            Vector2 target1 = new Vector2(7.5f, 7.5f);
            Vector2 target2 = new Vector2(7.5f, 13.2f);
            while (ghostPosition != target1)
            {
                ghostPosition = Vector2.MoveTowards(ghostPosition, target1, 3 * Time.deltaTime);
                ghost.transform.position = ghostPosition;
                yield return null;
            }
            ghostMover.MoveInput = Vector2.up;
            while (ghostPosition != target2)
            {
                ghostPosition = Vector2.MoveTowards(ghostPosition, target2, 3 * Time.deltaTime);
                ghost.transform.position = ghostPosition;
                yield return null;
            }
            SpriteRenderer spriteRenderer = ghost.GetComponent<SpriteRenderer>();
            Color color = spriteRenderer.color;
            for (float alpha = 1f; alpha >= 0; alpha -= Time.deltaTime)
            {
                color.a = alpha;
                spriteRenderer.color = color;
                if (alpha <= 0 - Time.deltaTime)
                {
                    color.a = 0;
                    spriteRenderer.color = color;
                }
                yield return null;
            }
            movementDone = true;
        }
        private void EnableStuff()
        {
            Destroy(zzz);
            cameraFollow.enabled = true;
            inputProcessor.enabled = true;
            joystick1.enabled = true;
            joystick2.enabled = true;
            characterControl.dontMove = false;
            animator.enabled = true;
            PlayerPrefs.SetInt("Level1Cutscene", 1);
            StartCoroutine("WaitFrame");
        }
    }
}
