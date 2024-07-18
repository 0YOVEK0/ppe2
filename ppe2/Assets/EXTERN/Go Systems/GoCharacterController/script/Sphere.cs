using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoSystem
{
    public class Sphere : MonoBehaviour
    {
        public float angle = 90;
        public float speed = 4;
        float angletest;
        public Transform sphere;
        bool lockcode = false;
        private void Start()
        {
            if (angle < 0) { lockcode = true; }
            if (angle > 0) { lockcode = false; }
        }
        // Update is called once per frame
        void Update()
        {

            var rotation = transform.rotation;
            var rotatedVector = rotation * Vector3.forward;
            angletest = Mathf.MoveTowards(angletest, angle, speed * Time.deltaTime);
            rotation.x = angletest;
            sphere.rotation = rotation;
            if (angletest > angle - 1 && lockcode == false)
            {
                angle = -angle;
                lockcode = true;
            }
            if (angletest < angle + 1 && lockcode == true)
            {
                angle = -angle;
                lockcode = false;
            }


        }
    }
}