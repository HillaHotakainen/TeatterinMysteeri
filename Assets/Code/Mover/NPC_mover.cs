using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class NPC_mover : MonoBehaviour
    {
        private Animator animator;
        private Vector2 moveInput;
        private float speed;
        public float Speed
        {
            get{return speed;}
            set{speed = value;}
        }
        public Vector2 MoveInput
		{
			get{return moveInput;}
			set{moveInput = value;}
		}
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("speed", speed);
			animator.SetFloat("horizontal", moveInput.x);
			animator.SetFloat("vertical", moveInput.y);
            animator.SetFloat("lastmoveX", moveInput.x);
            animator.SetFloat("lastmoveY", moveInput.y);
        }
    }
}
