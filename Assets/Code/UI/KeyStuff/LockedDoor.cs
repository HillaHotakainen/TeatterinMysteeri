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
        private bool closeEnough;

        private void Start()
        {
            hero = GameObject.Find("Hero");
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
            }
            //TODO: Poista oven collider ja pistä lataa seuraava level kun astuu ovesta sisään
        }
    }
}
