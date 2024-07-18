
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoSystem.Control;
namespace GoSystem {
    [GBehaviourAttributeAttribute("Camera controller", false)]
    public class GoCameraSystem : GoSystemsBehaviour
    {
        public float CameraSensitivity = 10;
        public float DistenceWalking = 3, DistenceCrouch = 1;
        public Vector2 maxrotatecamera = new Vector2(-50, 50);
        public float smoothtime = 0.2f;
        public Transform target;
        public Vector3 OffSetCameraWithCrouch;
        public Vector3 offSetCameraWalking;
        private float _DistenceCamera, Distencecollition;
        private float distence; float x, y;
        private  GoSystems Gs;
        private bool Collintion, GetTargetByCrouch = false;
        private Vector3 currentRotation, smoothVelocity = Vector3.zero, _offsetMainCamera, cameraPositionBehaviour;
        private Transform _target;
        private RaycastHit hit;
        private GoCharacterController cc;
        private void Start()
        {
            _DistenceCamera = DistenceWalking;
            _offsetMainCamera = offSetCameraWalking;

            if (target == null)
            {
                if (Control.GoSystemsController.bones.Count > 1)
                   target = Control.GoSystemsController.bones[10].transform;
                Gs = GoSystems.getSystem(target.gameObject);
                _target = target;
            }
            else
            {
                Gs = GoSystems.getSystem(target.gameObject);
                _target = target;
            }
        }
        private void Update()
        {
            PositionCameraBehaviour();
            TargetBehaviour();

        }
        private void collitionBehaviour()
        {
            Ray ray = new Ray(_target.position, -transform.forward);
            Physics.SphereCast(ray, 0.1f, out hit, _DistenceCamera);
            Distencecollition = Vector3.Distance(_target.position, hit.point);
            if (hit.transform != null)
            {
                Collintion = true;
                if (hit.transform.tag != "Player")
                {
                    _DistenceCamera = Transition(_DistenceCamera, Distencecollition, 7f);
                 
                }
            }
            else
            {
                if (Gs.IsCrouch != false)
                    return;
                Collintion = false;
                _DistenceCamera = Transition(_DistenceCamera, DistenceWalking, 7f);
            }
        }
        public float Transition(float startValue, float endValue, float duration)
        {
            var transitionValue = startValue;
            var transitionDuration = duration;
            if (startValue < endValue)
            {
                transitionValue = startValue + (endValue - startValue) / 1 * Time.deltaTime * transitionDuration;
            }
            else
            {
                transitionValue = startValue - (startValue - endValue) / 1 * Time.deltaTime * transitionDuration;
            }
            return transitionValue;
        }
        private void PositionCameraBehaviour()
        {
            x += Input.GetAxis("Mouse X") * CameraSensitivity;
            y -= Input.GetAxis("Mouse Y") * CameraSensitivity;
            y = Mathf.Clamp(y, maxrotatecamera.x, maxrotatecamera.y);
            var mousePos = new Vector3(y, x);
            currentRotation = Vector3.SmoothDamp(currentRotation, mousePos, ref smoothVelocity, smoothtime);
            transform.localEulerAngles = currentRotation;
        }
        private void TargetBehaviour()
        {
            if (_target != null)
            {
                cameraPositionBehaviour = _target.position - transform.forward * _DistenceCamera;
                transform.position = cameraPositionBehaviour + _offsetMainCamera;
                if (Gs.IsRagdall != true)
                {
                    GetTargetCrouch();
                    if (GetTargetByCrouch == true) return;
                    _target = target;
                }
                else
                {
                    _target.position = GoSystemsController.bones[10].position;
                }
                collitionBehaviour();
            }
        }
        private void GetTargetCrouch()
        {
            if (Gs.IsCrouch == true)
            {
                _offsetMainCamera = Vector3.Lerp(_offsetMainCamera, OffSetCameraWithCrouch, 5 * Time.deltaTime);
                _DistenceCamera = Transition(_DistenceCamera, DistenceCrouch, 7f);
                GetTargetByCrouch = true;
             _target = GoSystemsController.bones[10];
            }
            if (Gs.IsCrouch == false && GetTargetByCrouch == true)
            {
                _DistenceCamera = Mathf.Lerp(_DistenceCamera, DistenceWalking, 5 * Time.deltaTime);
                _offsetMainCamera = Vector3.Lerp(_offsetMainCamera, offSetCameraWalking, 5 * Time.deltaTime);
                GoSystems.OnExit.AddListener(GetTarget);
            }
        }
        private void GetTarget()
        {
            GetTargetByCrouch = false;
            _target = target;
        }

    }
}