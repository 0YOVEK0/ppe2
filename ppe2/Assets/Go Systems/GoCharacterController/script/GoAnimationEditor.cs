
using UnityEngine;
using UnityEditor;
namespace GoSystem
{
    namespace editor
    {


        public class GoAnimationEditor : StateMachineBehaviour
        {


            public static void GoStateEnter()
            {
                if (GoSystems.OnStarted != null)
                {
                    GoSystems.OnStarted.Invoke();
                }
            }
            public static void GoStateExit()
            {
                if (GoSystems.OnExit != null)
                {
                    GoSystems.OnExit.Invoke();
                }
            }
            public static void GoStateUpdate()
            {
                if (GoSystems.OnUpdate != null)
                {
                    GoSystems.OnUpdate.Invoke();
                }
            }

        }
    }
}
