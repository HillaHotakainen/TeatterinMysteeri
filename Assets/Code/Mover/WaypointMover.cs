// Teemu Herrala, teemu.herrala@tuni.fi

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class WaypointMover : MonoBehaviour, IMover
    {
        [SerializeField] private float speed = 1;
        public List<Transform> waypoints = new List<Transform>();
        NPC_mover mover;
        Flashlight fl;
        private int targetWaypoint = 0;
        public bool caught = false;
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
            mover.MoveInput = waypoints[targetWaypoint].position - transform.position;
        }
        void Start()
        {
            transform.position = waypoints[0].position;
            mover = GetComponent<NPC_mover>();
            mover.Speed = 1.0f;
            fl = GetComponent<Flashlight>();
        }

        void Update()
        {
            if (transform.position == waypoints[targetWaypoint].position) {
                targetWaypoint += 1;
                if (targetWaypoint == waypoints.Count) {
                    targetWaypoint = 0;
                }
                DetermineDirection();
            } else if (!caught) {
                Move(waypoints[targetWaypoint].position, speed * Time.deltaTime);
            }
        }
        private void OnTriggerEnter2D(Collider2D other) {
            caught = true;
        }
        private void DetermineDirection()
        {
            var dir = waypoints[targetWaypoint].position - transform.position;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) {
                if (dir.x > 0) {
                    fl.direction = "right";
                } else {
                    fl.direction = "left";
                }
            } else {
                if (dir.y > 0) {
                    fl.direction = "up";
                } else {
                    fl.direction = "down";
                }
            }
        }
    }
}
