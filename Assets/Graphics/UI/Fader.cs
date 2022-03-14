using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeatterinMysteeri
{
    public class Fader : MonoBehaviour
    {
        public enum FadeState
        {
            None,
            FadeIn,
            FadeOut
        }
        [SerializeField]
        private Image backGround;

        [SerializeField]
        private float speed = 1;
        private FadeState state = FadeState.None;
        private Color bgColor;
        void Start()
        {
            bgColor = backGround.color;
            bgColor.a = 0; // Täys läpinäkyvyys
            backGround.color = bgColor; // kopioidaan muutettu väriarvo taustakuvalle
        }

        // Update is called once per frame
        void Update()
        {
            switch(state)
            {
                case FadeState.FadeIn:
                bgColor.a = Mathf.Clamp01(bgColor.a + Time.deltaTime * speed); // Pidetään arvo aina välillä [0,1]
                backGround.color = bgColor;

                if(bgColor.a == 1)
                {
                    state = FadeState.None;
                }
                break;
                case FadeState.FadeOut:
                bgColor.a = Mathf.Clamp01(bgColor.a - Time.deltaTime * speed); // Pidetään arvo aina välillä [0,1]
                backGround.color = bgColor;

                if(bgColor.a == 0)
                {
                    state = FadeState.None;
                }
                break;
            }
        }
        public float FadeIn()
        {
            state = FadeState.FadeIn;
            return 1 / speed; // Fade animaatio aika
        }
        public float FadeOut()
        {
            state = FadeState.FadeOut;
            return 1 / speed; // Fade animaatio aika
        }
    }
}
