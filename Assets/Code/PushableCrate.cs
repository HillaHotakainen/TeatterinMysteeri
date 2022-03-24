using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TeatterinMysteeri
{
    public class PushableCrate : MonoBehaviour, IPointerDownHandler
    {
        private GameObject hero;
        private bool closeEnough = false;
        private bool moving = false;
        private float distance = 1;
        private const float speed = 5;
        private Vector3 direction = Vector3.zero;
        private Vector3 targetPos;

        void Start()
        {
            hero = GameObject.Find("Hero");
        }
        void Update()
        {
            // tsekkaus onko Hero-objekti tarpeeks lähellä
            if (Vector3.Distance(transform.position, hero.transform.position) < 2) {
                closeEnough = true;
            }
            else {
                closeEnough = false;
            }
            
            // jos ollaan tarpeeksi lähellä ja ei liikuta, päätellään mahdollisen liikkumisen suunta heron positiosta
            if (closeEnough & !moving) {
                // ensimmäiseksi selvitetään, onko hero vasemmalla vai oikealla ja ylhäällä vai alhaalla
                if (transform.position.x - hero.transform.position.x < 0) {
                    direction.x = -1;
                } else {
                    direction.x = 1;
                }
                if (transform.position.y - hero.transform.position.y < 0) {
                    direction.y = -1;
                } else {
                    direction.y = 1;
                }
                // sitten otetaan jompikumpi suunta pois, perustuen siihen kumman puolella hero on enemmän
                if (Mathf.Abs(hero.transform.position.x - transform.position.x) > Mathf.Abs(hero.transform.position.y - transform.position.y)) {
                    direction.y = 0;
                } else {
                    direction.x = 0;
                }
            }

            // liikutaan, jos matkaa on mennyt alle 1 unit
            if (moving & distance > 0) {
                Vector3 oldPos = transform.position;
                transform.Translate(direction * Time.deltaTime * speed);
                distance -= Vector3.Distance(oldPos, transform.position);
            }
            // kun perillä, pysähdytään ja tasataan positio
            if (moving & distance < 0) {
                moving = false;
                distance = 1;
                transform.position = targetPos;
                Debug.Log("Crate's new position: " + transform.position);
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            // liikkumiskäsky lähetetään vain, jos ollaan tarpeeksi lähellä ja laatikko ei jo liiku
            if (closeEnough & !moving) {
                targetPos = transform.position + direction;
                // unityn dokumentaatiosta löytyny koodi tapa tarkistaa olisko edessä collider
                if (Physics2D.OverlapPoint(targetPos) == null) {
                    Debug.Log("Crate pushed with the direction " + direction);
                    moving = true;
                } else {
                    Debug.Log("The crate can't be pushed there!");
                }
            }
        }
    }
}
