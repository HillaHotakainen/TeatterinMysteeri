// Teemu Herrala, teemu.herrala@tuni.fi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class TwoPointMover : MonoBehaviour, IMover
    {
        [SerializeField] private Vector3 point1 = new Vector3(0,0,0);
        [SerializeField] private Vector3 point2 = new Vector3(1,1,0);
        [SerializeField] private float speed = 1;
        private bool goingToPoint1 = false;
        public Vector2 Position {
            get {return transform.position;}
        }
        public float Speed {
            get {return speed;}
            set {speed = value;}
        }
        
        public void Move(Vector2 direction, float deltaTime)
        {
            transform.position = Vector2.MoveTowards(transform.position, direction, deltaTime);
        }
        void Start()
        {
            transform.position = point1;
        }

        void Update()
        {
            if (goingToPoint1) {
                if (transform.position == point1) {
                    goingToPoint1 = false;
                } else {
                    Move(point1, speed * Time.deltaTime);
                }
            } else {
                if (transform.position == point2) {
                    goingToPoint1 = true;
                } else {
                    Move(point2, speed * Time.deltaTime);
                }
            }
        }
    }
}
