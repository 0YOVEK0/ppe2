using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GoSystem
{
    [CreateAssetMenu(fileName = "ControlSetting", menuName = "GoSystems/ControlSetting")]
    public class ControlSetting : ScriptableObject

    {

        public float speed = 4f;
        public float fastSpeed = 10f;
        public KeyCode fastRun = KeyCode.LeftShift;
        public KeyCode JumpKey = KeyCode.Space;
        public KeyCode Crouch = KeyCode.LeftControl;
        public float TimeRun = 3f;
        public float CrouchSpeed = 2;
        public LayerMask layerJump;
        public LayerMask LayerWall;
        public LayerMask PlayerLayer;
        public float jumpForce = 5f;
        public float JumpHigh = 5;
        public float TimerJump;
        [Space(10)]
        [Range(0, 1)]
        public float IKfood;
        public Vector3 offsetFoodPosition;
        public LayerMask IKlayer;
        public Vector3 offsetLLeg;
        public Vector3 offsetRLeg;
        [HideInInspector]
        public Slider run;


        [Header("AudioSpiker")]
        public AudioClip[] Clips;
    }
}