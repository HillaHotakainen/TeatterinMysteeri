using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class LightReceiver : MonoBehaviour
    {
        [SerializeField] Sprite receiving;
        [SerializeField] Sprite notreceiving;
        private SpriteRenderer spr;
        public bool receivingLight;
        void Start()
        {
            spr = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            // toi 1 << 10 hässäkkä tarkottaa et checkkaa vaa light layer 
            var lightCheck = Physics2D.OverlapPoint(transform.position, (1 << 10));
            if (lightCheck != null) {
                receivingLight = true;
            } else {
                receivingLight = false;
            }

            if (receivingLight) {
                spr.sprite = receiving;
            } else {
                spr.sprite = notreceiving;
            }
        }
    }
}
