using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoSystem
{
    [GBehaviourAttributeAttribute("Foot Action", true)]
    public class FootActions : GoSystemsBehaviour
    {

        private bool LGrawndFood;
        private bool RGrawndFood;
        private AudioSource LFoot;
        private AudioSource Rfoot;
        private RaycastHit Lhit, Rhit, Ghit;
        private GameObject grund, SaveLayer;
        private GoSystems Gs;
        private GoCharacterController cc;
        public List<Describshen> LayersSaunds = new List<Describshen>();
        private  int Index;

        void Start()
        {
            cc = gameObject.GetComponent<GoCharacterController>();
            Gs = GoSystems.getSystem(gameObject);
            GoSystem.Control.GoSystemsController.Getbone();
            LFoot = Control.GoSystemsController.bones[11].gameObject.GetComponent<AudioSource>();
            Rfoot = Control.GoSystemsController.bones[12].gameObject.GetComponent<AudioSource>();
            Gs.IsFoodSound = true;
            SaveLayer = gameObject;

          
        }

        private void Update()

        {
            if (Gs.IsFoodSound == true)
            {
                if (GoSystems.x != 0 || GoSystems.z != 0)
                {

                    var Gray = new Ray(transform.position + Vector3.up, Vector3.down);
                    var Rray = new Ray(Control.GoSystemsController.bones[12].position, Vector3.down);
                    var Lray = new Ray(Control.GoSystemsController.bones[11].position, Vector3.down);

                    if (cc.IsGround == true)
                    {
                        LGrawndFood = Physics.Raycast(Lray, out Lhit, 0.15F);
                        RGrawndFood = Physics.Raycast(Rray, out Rhit, 0.15F);
                        if (Physics.Raycast(Gray, out Ghit, 1f))
                        {

                            grund = Ghit.transform.gameObject;
                            checkLayes(grund);
                        }

                    }
                    if (LGrawndFood == true)
                    {
                        LiftFood();
                    }

                    if (RGrawndFood == true)
                    {
                        rightFoot();
                    }
                }

            }
        }
        [System.Serializable]
        public class Describshen
        {
            public string name = "hakona matata";
            public LayerMask layer;
          
            public List<AudioClip> audio = new List<AudioClip>();
        }
        void LiftFood()
        {
       
                int ClipIndex = Random.Range(0, LayersSaunds[Index].audio.Count - 1);

                LFoot.clip = LayersSaunds[Index].audio[ClipIndex];
            LFoot.Play();

        }

        void rightFoot()
        {
                int ClipIndex = Random.Range(0, LayersSaunds[Index].audio.Count - 1);
                Rfoot.clip = LayersSaunds[Index].audio[ClipIndex];
                Rfoot.Play();
        }
 
        void checkLayes(GameObject Ghit)
        {

            int LayesNumber = LayersSaunds.Count;
            
            if (Ghit.layer != SaveLayer.layer)
            {

                for (int i = 0; i < LayersSaunds.Count; i++)
                {


                    if ((LayersSaunds[i].layer & (1 << Ghit.layer)) != 0)
                    {
                     
                        Index = i;
                        SaveLayer = Ghit;
                        i = LayesNumber;

                    }


                }
            }



        }
    

    }

}