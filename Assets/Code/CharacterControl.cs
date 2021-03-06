using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TeatterinMysteeri
{
	[RequireComponent(typeof(InputProcessor))]
	public class CharacterControl : MonoBehaviour
	{
		public enum ControlState
		{
			GamePad,
			Touch
		}

		[SerializeField]
		private float velocity = 1;

		private Animator animator;

		private new SpriteRenderer renderer;

		private Vector2 moveInput;

		private Vector2 touchPosition;

		private Vector2 targetPosition;

		private ControlState controlState = ControlState.GamePad;

		private Rigidbody2D rigidBody;

		public bool dontMove = false;
		private float lastmoveX;
		private float lastmoveY;
		public Vector2 MoveInput
		{
			get{return moveInput;}
			set{moveInput = value;}
		}
		private void Awake()
		{
			rigidBody = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
			if (animator == null)
			{
				Debug.LogError("Character is missing an animator component!");
				Debug.Break();
			}

			renderer = GetComponent<SpriteRenderer>();
			if (renderer == null)
			{
				Debug.LogError("Character is missing an renderer component!");
				Debug.Break();
			}
		}

		private void Update()
		{
			UpdateAnimator();
		}

		private void FixedUpdate()
		{
			if(!dontMove)
			{
				MoveCharacter();
			}
		}

		private void UpdateAnimator()
		{
			//renderer.flipX = moveInput.x < 0;
			// Same as
			// if (moveInput.x < 0)
			// {
			// 	renderer.flipX = true;
			// }
			// else
			// {
			// 	renderer.flipX = false;
			// }
			animator.SetFloat("speed", moveInput.magnitude);
			animator.SetFloat("horizontal", moveInput.x);
			animator.SetFloat("vertical", moveInput.y);
			if(moveInput == Vector2.zero)
			{
				animator.SetFloat("lastmoveX", lastmoveX);
				animator.SetFloat("lastmoveY", lastmoveY);
			}
			else
			{
				lastmoveX = moveInput.x;
				lastmoveY = moveInput.y;
			}
		}

		private void MoveCharacter()
		{
			switch (controlState)
			{
				case ControlState.GamePad:
					Vector2 movement = moveInput * Time.fixedDeltaTime * velocity;
					// transform property allows us to read and manipulate GameObject's position
					// in the game world.
					rigidBody.MovePosition(rigidBody.position + movement);
					// transform.Translate(movement);
					break;

                     //Kommentoin pois t??m??n toistaiseksi, saattaa olla hy??dyllinen tulevaisuudessa.
				case ControlState.Touch:
					// Koska Vector2:sta ei voi v??hent???? Vector3:a, pit???? suorittaa tyyppimuunnos
					Vector2 travel = targetPosition - (Vector2)transform.position;

					// Normalisointi muuntaa vektorin pituuden yhdeksi
					Vector2 frameMovement = travel.normalized * velocity * Time.fixedDeltaTime;

					// magnitude palauttaa vektorin pituuden. T??ss?? vektorin pituus kuvaa
					// j??ljell?? olevaa matkaa
					float distance = travel.magnitude;

					if (frameMovement.magnitude < distance)
					{
						// Matkaa on viel?? j??ljell??, kuljetaan kohti kohdepistett??
						// transform.Translate(frameMovement);
						rigidBody.MovePosition(rigidBody.position + frameMovement);
					}
					else
					{
						// P????m????r?? saavutettu
						rigidBody.MovePosition(targetPosition);
						// transform.position = targetPosition;
						moveInput = Vector2.zero;
					}
					break;
			}
		}

		private void OnMove(InputAction.CallbackContext callbackContext)
		{
			controlState = ControlState.GamePad;
			moveInput = callbackContext.ReadValue<Vector2>();
		}
	}
}