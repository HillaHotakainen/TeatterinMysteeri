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

        private KeyHolder keyHolder;
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
        public void Awake()
        {
            keyHolder = FindObjectOfType<KeyHolder>();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Got the key!");
            keyHolder.GetKey();         //Hakee keyHolderin GetKey-metodia, joka laittaa tämän avaimen
            Destroy(gameObject);        //Spriten keyHolderille, jonka jälkeen poistaa tämän gameobjectin
        }
    }
}
