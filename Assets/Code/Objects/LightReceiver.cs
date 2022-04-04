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
            var lightCheck = Physics2D.OverlapPoint(transform.position, (1 << 10));
            if (lightCheck != null) {
                var verTest = lightCheck.GetComponent<LightBeamVertical>();
                var horTest = lightCheck.GetComponent<LightBeamHorizontal>();
                if (verTest != null | horTest != null) {
                    receivingLight = true;
                } else {
                    receivingLight = false;
                }
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
