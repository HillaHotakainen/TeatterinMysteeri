using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TeatterinMysteeri
{
    public class Key : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private Sprite keyIcon;
        [SerializeField]
        private TextBox textBox;
        private KeyHolder keyHolder;
        private GameObject hero;
        private bool closeEnough;
        public Sprite Icon
        {
            get 
            {
                // Jos objektin iconi on määritetty palautetaan se.
                if (keyIcon != null)
                {
                    return keyIcon;
                }

                // Jos iconia ei oo määritetty niin käytetään spriteRenderin spriteä.
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    return spriteRenderer.sprite;
                }

                // jos koko spriteRenderiä ei löydy
                return null;
            }
        }
        public void Start()
        {
            keyHolder = FindObjectOfType<KeyHolder>();
            hero = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        public void Update()
        {
            // jatkuva tsekkaus onko Hero-objekti alle 2 unitin päässä
            if (Vector3.Distance(transform.position, hero.transform.position) < 2) {
                closeEnough = true;
            }
            else {
                closeEnough = false;
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            float delay = 0;

            if (closeEnough && audio != null) 
            {
                audio.Play();
                delay = audio.clip.length;
                if(delay != 0)
                {
                    renderer.enabled = false;
                }
                Debug.Log("Got the key!");
                keyHolder.GetKey();         //Hakee keyHolderin GetKey-metodia, joka laittaa tämän avaimen
                Destroy(gameObject, delay);        //Spriten keyHolderille, jonka jälkeen poistaa tämän gameobjectin
                textBox.StartFade();
            }
        }
    }
}
