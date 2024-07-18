using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoSystem.Control;
namespace GoSystem
{
    [GBehaviourAttributeAttribute("ZipLine", true)]
    public class GoZipLineSystem : GoSystemsBehaviour
    {
        zipLineApi zipline;

        Transform A, B, C;
        public float Speed = 6f, OffSet= -2.3f;
        bool lockcode = true,lockInput;
        public KeyCode input = KeyCode.E;

        private float TimeToEnd=1;
        GoSystems Gs;
        GoCharacterController GCC;

      public  UnityEngine.Events.UnityEvent OnStartAction , OnExitAction, OnAction;

        private void Awake()
        {

            Gs = GoSystems.getSystem(gameObject);
        }


        // Update is called once per frame
        void Update()
        {
          
            if (GoTrigger.other != null) {
                if (GoTrigger.other.GetComponent<zipLineApi>() != null)
                {
                    if (lockInput == false)
                        if (Input.GetKeyDown(input))
                        {
         
                            LockCode();
                            zipline = GoTrigger.other.GetComponent<zipLineApi>();
                            A = zipline.PointA;
                            B = zipline.PointB;
                            C = zipline.PointC;
                            Collider point = GoTrigger.other.GetComponent<Collider>();
                            C.position = point.ClosestPoint(transform.position);

                        }
                }
            }
                if (lockcode == false)
            {

                Gs.Dircection(C, B.transform.position, Speed,true,true);
                transform.position = Gs.FixPosition(C.position, transform.position, 0, OffSet);
               
                if (TimeToEnd > 0)
                {
                    TimeToEnd -= Time.deltaTime;
                    Gs.FixRotation(transform, B, 8,Vector3.zero);
                }
                var Dis = Vector3.Distance(C.position, B.position);
                if (Dis<=0+1)
                {
                    UnLockCode();
                }
            }

        }


        private void LockCode()
        {
            OnStartAction.Invoke();
            Gs.Axis = false;
            GoSystems.IsLags = false;
            GoSystems.IsFoods = false;

            Gs.IsActivte = true;
            Gs.IsZipLine = true;
            Gs.IsLockJump = true;
            GoSystemsController.GoisKinematic(true);
            GoSystemsController.GoUseGravety(false);
            lockcode = false;
            lockInput = true;
          
            GoSystems.animatorControler.SetTrigger("zipline");
          

        }

        private void UnLockCode()
        {
            OnExitAction.Invoke();
            Gs.IsLockMove = false;
            Gs.IsLockJump = false;
            Gs.Axis = true;
            Gs.IsActivte = false;
            GoSystems.IsLags = true;
            GoSystems.IsFoods = true;
            Gs.IsZipLine = false;
            Gs.IsJumping = true;
            GoSystemsController.GoisKinematic(false);
           GoSystemsController.GoUseGravety(true);
            lockcode = true;
            lockInput = false;
           
            TimeToEnd = 1;
            GoSystems.animatorControler.SetTrigger("zipline");

        }
    }
}