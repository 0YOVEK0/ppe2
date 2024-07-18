
using UnityEngine;
using UnityEditor;

using UnityEngine.UI;
using UnityEditor.Animations;
namespace GoSystem.editor
{
    public class GoSystemsEditor
    {
        public class GUIWindowZipLine : EditorWindow
        {

            string Label;
            string Massage = "Put your Character ";
            Vector2 rect = new Vector2(550, 250);

            Object Charcter;


            Editor humanoidpreview;
            float totalMass = 20;
            float strength = 0.0F;
            public Texture2D logo;



            [MenuItem("Tools/Go Systems/Actions/Zipline", false, 0)]
            public static void ShowWindow()
            {
                GetWindow<GUIWindowZipLine>("Add ZipLine");
            }
            public virtual void OnGUI()
            {
                this.minSize = rect;


                GUILayout.BeginVertical(minue.MySkin.window);
                GUILayout.Label("ZipLine Systems", minue.MySkin.label);

                EditorGUILayout.Space(15);
                if (humanoidpreview != null)
                {
                    humanoidpreview.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(100, 200), minue.MySkin.window);
                }
                //controler
                Charcter = EditorGUILayout.ObjectField("Charcter", Charcter, typeof(GameObject), true, GUILayout.ExpandWidth(true)) as GameObject;


                if (GUILayout.Button("Create", minue.MySkin.button))
                {

                    
                    if (Charcter != null)
                    {
                        GameObject player = Charcter as GameObject;
                        if (player.GetComponent<GoZipLineSystem>() == null)
                        {
                            player.AddComponent<GoZipLineSystem>();
                            this.Close();
                        }
                        else
                        {
                            Massage = "You already have zipline system";
                        }
                    }
                    else
                    {
                        Massage = "Charcter can not be Empty";
                    }


                }
                
                EditorGUILayout.Space(10);
                //massage
                EditorGUILayout.HelpBox(Massage, MessageType.Info);

                //skin
                // Label = _mySkin.label;
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();



            }

        }
        public  class minue : EditorWindow
        {
            string NamePlayer = "R52 Controller";
            string Label;
            string Massage = "Through this window, you can put the Go System system on your character without any problems and with the click of a single button";
            public Object player;
            public Object camera;
            public Object UI;
            public Object PhysiceMaterial;
            public Object Animator;
            public Object settings;
            public Texture2D logo;
            MessageType mt = MessageType.Info;
            Editor humanoidpreview;
            //Skin
           public static GUISkin _mySkin;
            Vector2 rect = new Vector2(550, 360);
            [MenuItem("Tools/Go Systems/Basic Locomotion/Create Basic Controller", false, 0)]
            public static void ShowWindow()
            {
                GetWindow<minue>("Create Basic Controller");
            }
            private void OnGUI()
            {
                this.minSize = rect;
                _mySkin = MySkin;
                GUILayout.BeginVertical(_mySkin.window);
                GUILayout.Label("GO SYSTEMS", _mySkin.label);

                if (humanoidpreview != null)
                {
                    humanoidpreview.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(100, 200), _mySkin.window);
                }

                NamePlayer = EditorGUILayout.TextField(NamePlayer, _mySkin.textField);

                EditorGUILayout.Space(15);

                //controler
                player = EditorGUILayout.ObjectField("Controller", player, typeof(GameObject), true);
                camera = EditorGUILayout.ObjectField("Camera", camera, typeof(GameObject), true);
                Animator = EditorGUILayout.ObjectField("AnimatorController", Animator, typeof(Animator), true);
                PhysiceMaterial = EditorGUILayout.ObjectField("Physice Material", PhysiceMaterial, typeof(PhysicMaterial), true);
                settings = EditorGUILayout.ObjectField("Settings", settings, typeof(ControlSetting), true);
                //UI
                UI = EditorGUILayout.ObjectField("GUI", UI, typeof(GameObject), true);
                EditorGUILayout.Space(20);

                bool IsHuman;
                if (GUILayout.Button("Create", _mySkin.button))
                {
                    //   Debug.Log(NamePlayer);


                    var Controler = Instantiate(player) as GameObject;
                    var GUI = Instantiate(UI as GameObject, Controler.transform);
                    var NewCamera = new GameObject("camera");
                    Controler.name = NamePlayer;
                    Controler.tag = "Player";

                    if (Controler.GetComponent<Animator>() == null)
                    {
                        var anim = Controler.AddComponent<Animator>();
                        if (anim.avatar.isHuman == true)
                        {
                            anim.runtimeAnimatorController = Animator as AnimatorController;
                            Controler.GetComponent<Animator>().applyRootMotion = false;

                            var Spicar = Controler.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).gameObject.AddComponent<AudioSource>();
                            Spicar.playOnAwake = false;
                        }
                    }
                    else
                    {
                        var anim = Controler.GetComponent<Animator>();
                        if (anim.avatar.isHuman == true)
                        {
                            Controler.GetComponent<Animator>().runtimeAnimatorController = Animator as AnimatorController;
                            Controler.GetComponent<Animator>().applyRootMotion = false;

                            var Spicar = Controler.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).gameObject.AddComponent<AudioSource>();
                            Spicar.playOnAwake = false;
                        }
                    }


                    if (Controler.GetComponent<Animator>() != null)
                    {
                        if (Controler.GetComponent<Animator>().avatar.isHuman == true)
                        {

                            NewCamera.AddComponent<Camera>();
                            NewCamera.AddComponent<GoCameraSystem>();
                            NewCamera.AddComponent<AudioListener>();

                            var Target = new GameObject("targetCam");
                            Target.transform.SetParent(Controler.transform);
                            Target.transform.position = Controler.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position;
                            NewCamera.GetComponent<GoCameraSystem>().target = Target.transform;
                            NewCamera.GetComponent<Camera>().tag = "MainCamera";
                            var hips = Controler.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).gameObject;
                            hips.AddComponent<AudioSpeaker>();
                            #region control setting
                            var control = Controler.AddComponent<GoCharacterController>();
                            GoSystems.Player = control;
                            GUI.name = "GUI";
                            ControlSetting CS = settings as ControlSetting;
                            control.MoveSpeed = CS.speed;
                            control.SprintRunSpeed = CS.fastSpeed;
                            control.SprintRun = CS.fastRun;
                            control.JumpKey = CS.JumpKey;
                            control.gameObject.layer= LayerMask.NameToLayer("Player"); 
                            control.layerJump = CS.layerJump;
                            control.LayerWall = CS.LayerWall;
                            control.jumpForce = CS.jumpForce;
                            control.JumpHigh = CS.JumpHigh;
                            control.TimerJump = CS.TimerJump;
                            control.IKfootWeight = CS.IKfood;
                            control.SprintBar = GUI.transform.GetComponentInChildren<Slider>();
                            control.CrouchKey = CS.Crouch;
                            control.CrouchSpeed = CS.CrouchSpeed;
                            control.TimeSprintRun = CS.TimeRun;
                            control.offsetFoodPosition = CS.offsetFoodPosition;
                            control.IKlayer = CS.IKlayer;
                            control.offsetLLeg = CS.offsetLLeg;
                            control.offsetRLeg = CS.offsetRLeg;
                            GoSystems.Add(ref control.Gs, ref control.Index);
                            #endregion
                            Collider coll = Controler.AddComponent<CapsuleCollider>();
                            coll.material = PhysiceMaterial as PhysicMaterial;
                            Controler.GetComponent<CapsuleCollider>().height = 3.0f;
                            Controler.GetComponent<CapsuleCollider>().center = hips.transform.position;
                            hips.GetComponent<AudioSpeaker>().Clip = CS.Clips;
                            Rigidbody rb = Controler.AddComponent<Rigidbody>();
                            rb.mass = 1.0f;
                            rb.constraints = RigidbodyConstraints.FreezeRotation;
                            Control.GoSystemsController.Getbone();
                            this.Close();
                        }
                        else
                        {
                            DestroyImmediate(Controler.gameObject);
                            DestroyImmediate(NewCamera.gameObject);
                            mt = MessageType.Error;
                            Massage = "you should put character have humanoid rigging";
                        }
                    }
                }
            
            
                EditorGUILayout.Space(10);

                //massage
                EditorGUILayout.HelpBox(Massage, mt);

                string path = "Assets/Go Systems/GoCharacterController/";
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                #region auto get item
                if (player == null)
                {
                    try
                    {
                        player = (GameObject)AssetDatabase.LoadAssetAtPath(path + "models/Models/Go Systems Charcter/R52.fbx", typeof(Object));
                    }
                    catch
                    {
                        player = null;
                    }
                }
                if (camera == null)
                {
                    camera = (GameObject)AssetDatabase.LoadAssetAtPath(path + "prefab/camera.prefab", typeof(Object));
                }
                if (UI == null)
                {
                    UI = (GameObject)AssetDatabase.LoadAssetAtPath(path + "prefab/GUI.prefab", typeof(Object));
                }
                if (Animator == null)
                {

                    Animator = (AnimatorController)AssetDatabase.LoadAssetAtPath(path + "animation/Go-systems animator.controller", typeof(Object));
                }
                if (PhysiceMaterial == null)
                {
                    PhysiceMaterial = (PhysicMaterial)Resources.Load("MoveMateral");
                }
                if (settings == null)
                {
                    settings = (ControlSetting)Resources.Load("Setting");
                }

                #endregion
            }


            public static GUISkin MySkin
            {
                get
                {
                    if (_mySkin == null)
                    {
                        _mySkin = (GUISkin)Resources.Load("GUISkin");
                    }
                    return _mySkin;
                }
            }






        }
        public class GUIWindow : EditorWindow
        {

            string Label;
            string Massage = "Put your player in character bar";
            Vector2 rect = new Vector2(550, 630);

            Object Charcter;

            Object root;

            Object leftHips;
            Object leftKnee;
            Object leftFoot;

            Object rightHips;
            Object rightKnee;
            Object rightFoot;

            Object leftArm;
            Object leftElbow;

            Object rightArm;
            Object rightElbow;

            Object middleSpine;
            Object head;
            Editor humanoidpreview;
            float totalMass = 20;
            float strength = 0.0F;
            public Texture2D logo;

            //Skin
            GUISkin _mySkin;

            [MenuItem("Tools/Go Systems/Basic Locomotion/Ragdoll", false, 0)]
            public static void ShowWindow()
            {
                GetWindow<GUIWindow>("Create Ragdoll");
            }
            public virtual void OnGUI()
            {
                this.minSize = rect;
                _mySkin = minue.MySkin;

                GUILayout.BeginVertical(_mySkin.window);
                GUILayout.Label("GO SYSTEMS", _mySkin.label);

                EditorGUILayout.Space(15);
                if (humanoidpreview != null)
                {
                    humanoidpreview.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(100, 400), _mySkin.window);
                }
                //controler
                Charcter = EditorGUILayout.ObjectField("Character", Charcter, typeof(GameObject), true, GUILayout.ExpandWidth(true)) as GameObject;

                GUILayout.Space(5);

                root = EditorGUILayout.ObjectField("root", root, typeof(Transform), true, GUILayout.ExpandWidth(true)) as Transform;

                GUILayout.Space(5);

                leftHips = EditorGUILayout.ObjectField("leftHips", leftHips, typeof(Transform), true, GUILayout.ExpandWidth(true)) as Transform;
                leftKnee = EditorGUILayout.ObjectField("leftKnee", leftKnee, typeof(Transform), true) as Transform;
                leftFoot = EditorGUILayout.ObjectField("leftFoot", leftFoot, typeof(Transform), true) as Transform;

                GUILayout.Space(5);

                rightHips = EditorGUILayout.ObjectField("rightHips", rightHips, typeof(Transform), true) as Transform;
                rightKnee = EditorGUILayout.ObjectField("rightKnee", rightKnee, typeof(Transform), true) as Transform;
                rightFoot = EditorGUILayout.ObjectField("rightFoot", rightFoot, typeof(Transform), true) as Transform;

                GUILayout.Space(5);

                leftArm = EditorGUILayout.ObjectField("leftArm", leftArm, typeof(Transform), true) as Transform;
                leftElbow = EditorGUILayout.ObjectField("leftElbow", leftElbow, typeof(Transform), true) as Transform;

                GUILayout.Space(5);

                rightArm = EditorGUILayout.ObjectField("rightArm", rightArm, typeof(Transform), true) as Transform;
                rightElbow = EditorGUILayout.ObjectField("rightElbow", rightElbow, typeof(Transform), true) as Transform;

                GUILayout.Space(5);

                middleSpine = EditorGUILayout.ObjectField("middleSpine", middleSpine, typeof(Transform), true) as Transform;
                head = EditorGUILayout.ObjectField("head", head, typeof(Transform), true) as Transform;

                GUILayout.Space(5);

                totalMass = EditorGUILayout.FloatField("Total Mass", totalMass);
                strength = EditorGUILayout.FloatField("strength", strength);
                if (Charcter != null)
                {
                    try
                    {
                        GameObject charc = Charcter as GameObject;
                        Animator animator = charc.GetComponent<Animator>();
                        root = animator.GetBoneTransform(HumanBodyBones.Hips);

                        leftHips = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
                        leftKnee = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
                        leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);

                        rightHips = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
                        rightKnee = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
                        rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);

                        leftArm = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
                        leftElbow = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);

                        rightArm = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
                        rightElbow = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);

                        middleSpine = animator.GetBoneTransform(HumanBodyBones.Chest);

                        head = animator.GetBoneTransform(HumanBodyBones.Head);

                      
                    }

                    catch
                    {
                        Massage = "Your Object Dont Have Bones";
                    }
                }
                if (GUILayout.Button("Create", _mySkin.button))
                {


                    if (root != null && leftArm != null && leftElbow != null && leftFoot != null && rightArm != null && rightElbow != null && rightFoot != null && head != null && middleSpine != null && rightHips != null && rightKnee != null && leftHips != null && leftKnee != null)
                    {
                        Transform Root = root as Transform;
                        if (Root.GetComponent<Rigidbody>() == null)
                        {
                            Control.GoSystemsController.Getbone();
                            ragdolleditor mm = new ragdolleditor();
                            mm.root = root as Transform;
                            mm.leftHips = leftHips as Transform;
                            mm.leftKnee = leftKnee as Transform;
                            mm.rightHips = rightHips as Transform;
                            mm.rightKnee = rightKnee as Transform;
                            mm.leftArm = leftArm as Transform;
                            mm.leftElbow = leftElbow as Transform;
                            mm.rightArm = rightArm as Transform;
                            mm.rightElbow = rightElbow as Transform;
                            mm.middleSpine = middleSpine as Transform;
                            mm.head = head as Transform;
                            mm.CreateRagboll();
                            GoSystems.Player = Root.GetComponentInParent<GoCharacterController>();
                       
                  
                        
                           var Car = Charcter as GameObject;
                     
                            Car.AddComponent<GoRagdollController>();
                 
                              RagdallDisable();
                            this.Close();
                        }
                        else
                        {
                            Massage = "You already have RagDoll";
                        }
                        
                    }

                }
                EditorGUILayout.Space(10);
                //massage
                EditorGUILayout.HelpBox(Massage, MessageType.Info);

                //skin
                // Label = _mySkin.label;
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();



            }
            private void RagdallDisable()
            {

                for (int i = 0; i < Control.GoSystemsController.bones.Count-4; i++)
                {
                    Control.GoSystemsController.bones[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Control.GoSystemsController.bones[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    Control.GoSystemsController.bones[i].gameObject.GetComponent<Collider>().enabled = false;
                }

            }
        }
        public class footSound : EditorWindow
        {

            public Object player;
            public Object LiftFoot;
            public Object RightFoot;
            string Label;
            string Massage = "Put Your Charcter";
            Editor humanoidpreview;
            //Skin
            static GUISkin _mySkin;
            Vector2 rect = new Vector2(500, 250);
            [MenuItem("Tools/Go Systems/Actions/Foot Sound", false, 0)]
            public static void ShowWindow()
            {
                GetWindow<footSound>("Create Food Sound ");

            }
            private void OnGUI()
            {
            
                GetWindow<footSound>().minSize = rect;
                _mySkin = minue.MySkin;
                GUILayout.BeginVertical(_mySkin.window);
                GUILayout.Label("GO SYSTEMS", _mySkin.label);
                player = EditorGUILayout.ObjectField("Controler", player, typeof(GameObject), true);
                LiftFoot = EditorGUILayout.ObjectField("LeftFoot", LiftFoot, typeof(Transform), true);
                RightFoot = EditorGUILayout.ObjectField("RiahtFoot", RightFoot, typeof(Transform), true);
     

                EditorGUILayout.Space(15);
                if (player != null)
                {

                    var p = player as GameObject;
                    Animator animator = p.GetComponent<Animator>();
                    Control.GoSystemsController.Getbone();
                    RightFoot = p.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightFoot);
                    LiftFoot = p.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot);

                }
                //controler

                if (GUILayout.Button("Create", _mySkin.button))
                {
                    GameObject p = player as GameObject;
                    if (p.GetComponent<FootActions>() != null)
                    {
                        Massage = "You already have Food Actions on your character";

                    }
                    else
                    {

                         p.AddComponent<FootActions>();

                        Transform L = LiftFoot as Transform;
                        Transform R = RightFoot as Transform;
                        if (L.GetComponent<AudioSource>() == null && R.GetComponent<AudioSource>() == null)
                        {
                            L.gameObject.AddComponent<AudioSource>();
                            R.gameObject.AddComponent<AudioSource>();
                            this.Close();
                        }
                        else
                        {
                            Massage = "You Have Audio Source on your character";
                        }
                    }
                }
                EditorGUILayout.HelpBox(Massage, MessageType.Info);
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
            }
        }
    }

}