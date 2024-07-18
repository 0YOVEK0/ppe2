
using UnityEngine;


namespace GoSystem.editor
{
    public class GoAnimationEvite : GoAnimationEditor
    {


     
        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            GoStateExit();
        }
   

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            GoStateEnter();
      
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            GoStateUpdate();
      
        }
    }
}