using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class StillGuard : MonoBehaviour
    {
        public Sprite upGuard;
        public Sprite downGuard;
        public Sprite rightGuard;
        public Sprite leftGuard;
        private SpriteRenderer spr;
        [SerializeField] string direction = "down";
        Flashlight fl;
        void Start()
        {
            spr = gameObject.GetComponent<SpriteRenderer>();
            fl = GetComponent<Flashlight>();
            fl.direction = direction;

            switch (direction) {
                case "up":
                    spr.sprite = upGuard;
                    break;
                case "down":
                    spr.sprite = downGuard;
                    break;
                case "right":
                    spr.sprite = rightGuard;
                    break;
                case "left":
                    spr.sprite = leftGuard;
                    break;
                default:
                    break;
            }
        }
    }
}
