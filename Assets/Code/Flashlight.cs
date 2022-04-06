using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class Flashlight : MonoBehaviour
    {
        public Transform lightObj;
        private GameObject parent;
        private GameObject flashlight;
        private SpriteRenderer lightSpr;
        public string direction;
        void FixedUpdate()
        {
            // en tiiä teinkö tän turhaan hirveen monimutkasesti mut tää on syvältä joka tapauksessa
            // syy miks toi parent pitää tehä on et ainoo tapa kääntää collideria on jos se on
            // child objekti jollekkin muulle objektille mitä käännetään (googlen mukaan)
            if (parent == null) {
                parent = Instantiate(new GameObject(), transform.position, Quaternion.identity);
            }
            if (flashlight == null) {
                flashlight = Instantiate(lightObj, transform.position, Quaternion.identity).gameObject;
                lightSpr = flashlight.GetComponent<SpriteRenderer>();
                flashlight.transform.parent = parent.transform;
            }

            switch(direction) {
                case "right":
                    parent.transform.rotation = Quaternion.Euler(0, 0, 0);
                    parent.transform.position = new Vector2(transform.position.x + 2.75f, transform.position.y + 0.05f);
                    lightSpr.sortingOrder = 7;
                    break;
                case "left":
                    parent.transform.rotation = Quaternion.Euler(0, 0, 180);
                    parent.transform.position = new Vector2(transform.position.x - 2.75f, transform.position.y + 0.05f);
                    lightSpr.sortingOrder = 7;
                    break;
                case "up":
                    parent.transform.rotation = Quaternion.Euler(0, 0, 90);
                    parent.transform.position = new Vector2(transform.position.x, transform.position.y + 2.5f);
                    lightSpr.sortingOrder = 3;
                    break;
                case "down":
                    parent.transform.rotation = Quaternion.Euler(0, 0, -90);
                    parent.transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y - 2.5f);
                    lightSpr.sortingOrder = 7;
                    break;
                default:
                    break;
            }
        }
    }
}
