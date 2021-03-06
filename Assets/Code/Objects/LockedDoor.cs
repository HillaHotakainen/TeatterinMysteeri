using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TeatterinMysteeri
{
    public class LockedDoor : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private KeyHolder keyHolder;    //Tein näin, voisi varmaan tehdä myös FindObjectOfTypellä

        [SerializeField]
        Sprite openDoor;                //Oven aukinainen sprite
        private GameObject hero;
        [SerializeField] TextBox tooltip;
        private bool closeEnough;
        Collider2D hitbox;
        AudioSource audioSource;
        private bool soundPlaying = false;
        private void Start()
        {
            hero = GameObject.FindGameObjectsWithTag("Player")[0];
            hitbox = GetComponent<Collider2D>();
            audioSource = GetComponent<AudioSource>();
        }
        private void Update()
        {
            CheckDistance();        // jatkuva tsekkaus onko Hero-objekti alle 2 unitin päässä
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(transform.position, hero.transform.position) < 2)
            {
                closeEnough = true;
            }
            else
            {
                closeEnough = false;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(closeEnough && keyHolder.GotKey)         //jos on tarpeeksi lähellä ja on avain
            {                                           //vaihtaa klikkauksella oven spriten aukimaiseen
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = openDoor;
                hitbox.enabled = false;
                if(audioSource != null && !soundPlaying)
                {
                    audioSource.Play();
                    soundPlaying = true;
                }
            }
            else if(closeEnough && !keyHolder.GotKey)
            {
                if(tooltip!=null)
                {
                    tooltip.StartFade();
                }
            }
        }
    }
}
