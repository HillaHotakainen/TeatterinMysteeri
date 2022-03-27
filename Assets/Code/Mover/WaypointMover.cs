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
        private int targetWaypoint = 0;
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
            transform.position = waypoints[0].position;
        }

        void Update()
        {
            if (transform.position == waypoints[targetWaypoint].position) {
                targetWaypoint += 1;
                if (targetWaypoint == waypoints.Count) {
                    targetWaypoint = 0;
                }
            } else {
                Move(waypoints[targetWaypoint].position, speed * Time.deltaTime);
            }
        }
    }
}
