
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using GoSystem.Control;
namespace GoSystem
{
    public class GoSystems
    {

        private static readonly GoSystems gs = new GoSystems(); 
        private GoCharacterController player;  
        public static GoCharacterController Player  
        {
            get => gs.player;  // return gs.Player;
            set => gs.player = value;  // set gs.player to value
        }
        public static GoSystems[] GetGs;
        public static void GetGoSystems()
        {
            if (GetGs == null)
            {
                GoCharacterController[] GetControllerGoSystems = (GoCharacterController[])GameObject.FindObjectsOfType(typeof(GoCharacterController));
                int IndexConteoller = GetControllerGoSystems.Length;
                if (IndexConteoller > 0)
                {
                    Array b = Array.CreateInstance(gs.GetType(), IndexConteoller);
                  
                    for (int i =0; i < IndexConteoller; i++)
                    {
                        if (i == 0)
                        {
                            GetControllerGoSystems[i].Index = i ;
                        }
                        else
                        {
                            GetControllerGoSystems[i].Index = i - 1;
                        }
                        b.SetValue(GetControllerGoSystems[i].Gs, i);
                      
                        GetGs = b as GoSystems[];
                    }

                }

            }
        }
        public static GoSystems getSystem(GameObject script)
        {
            return  script.GetComponentInParent<GoCharacterController>().Gs;
        }
        public static void Add(ref GoSystems Gs, ref int Index)
        {
            GoCharacterController[] kk = (GoCharacterController[])GameObject.FindObjectsOfType(typeof(GoCharacterController));

            int ll = kk.Length;
            if (ll > 0)
            {
             
                for (int i = 0; i < ll; i++)
                {
                    Array b = Array.CreateInstance(gs.GetType(), ll);
                    if (GetGs != null)
                    {
                        GetGs.Clone();

                        if (i == 0)
                        {
                            kk[i].Index = i;
                        }
                        else
                        {
                            kk[i].Index = i - 1;
                        }
                        b.SetValue(kk[i].Gs,i);
                        GetGs = b as GoSystems[];
                        if (i == ll - 1)
                        {
                            Array s = AddToArray(GetGs, ref Gs, ll, ref Index);
                            GetGs = s as GoSystems[];
                        }
                    }
                    else
                    {

                        if (i == 0)
                        {
                            kk[i].Index = i;
                        }
                        else
                        {
                            kk[i].Index = i - 1;
                        }
                        b.SetValue(kk[i].Gs, i);
                        GetGs = b as GoSystems[];
                        Array s = AddToArray(GetGs, ref Gs, ll, ref Index);
                        GetGs = s as GoSystems[];
                    }
                }
            }
            else
            {
                Array s = AddToArray(GetGs, ref Gs, ll, ref Index);
                GetGs = s as GoSystems[];
            }
        }
        private static Array AddToArray(Array a, ref GoSystems o, int Index, ref int NewIndex)
        {
            if (a != null)
            {
                Array A = a;
                Array b = Array.CreateInstance(A.GetType().GetElementType(), Index + 1);
                A.CopyTo(b, 1);
                b.SetValue(o, 0);
                NewIndex = a.Length-1;
                A = b;
                a = A;
            }
            else
            {
                Array b = Array.CreateInstance(gs.GetType(), 1);
                b.SetValue(o, 0);
                NewIndex = 0;
                a = b;
            }
            return a;
        }
        #region IK Var
        //IkFoodControler
        public static bool IsFoods = true;
        public static bool IsLags = true;
        private static bool IsRun;
        public static float angle;
        private static RaycastHit hit;
        #endregion
        #region character Var
        public static Animator animatorControler
        {
            get
            {
                Animator Anim = GoSystemsController.Charcter.GetComponent<Animator>();
                return Anim;
            }
        }
        public static Rigidbody Grigidbody
        {
            get
            {
                return GoSystemsController.rigidbody;
            }
        
        }
        #endregion
        #region Axis Var
        public static float x
        {
            get
            {
                float X = Input.GetAxis("Horizontal");
                return X;
            }
        }
        public static float z
        {
            get
            {
                float Z = Input.GetAxis("Vertical");
                return Z;
            }
        }
        public static float velocity;
        public static float Speed;
        #endregion
        #region Is Var
        public  bool IsGround { get; set; }
        public  bool IsLockMove;
        private bool IntoObject;
        private bool IntoTag;
        public  bool Axis = true;
        public  bool IsLockJump;
        public  bool IsDie;
        public bool IsFoodSound;
        public  bool NotIdel;
        public bool IsCrouch;
        public  bool IsActivte { get; set; }
        public  bool IsPip { get; set; }
        public  bool RagdallActivate { get; set; }
        public bool IsRagdall;
        public  bool IsZipLine { get; set; }
        public  bool IsMoveVilasty = true;
        public  bool IsJumping { get; set; }
        public float Go_Velocity;
        public static bool lockGoSystemsChontorl { get; set; }
        public static UnityEngine.Events.UnityEvent OnStarted;
        public static UnityEngine.Events.UnityEvent OnUpdate;
        public static UnityEngine.Events.UnityEvent OnExit;
        public class AxisUpdate
        {
            public float x;
            public float z;
        }
        public AxisUpdate axisUpdate = new AxisUpdate();
        #endregion
        private static bool fastrun = true;
        private static bool LockCrouch;
        private static float savespeed;
        private static Slider Sprint;
        public static float TimeFall = 3f;
        public static bool CameraPositionCrouch;
        public void MovimentBehaviour(float speed ,float fastRunSpeed, KeyCode fastRun)
        {
            if (Axis == true)
            {
                var x = Input.GetAxis("Horizontal");
                var z = Input.GetAxis("Vertical");
                axisUpdate.x = x;
                axisUpdate.z = z;
                animatorControler.SetFloat("Y Movement", z);
                animatorControler.SetFloat("Movement", x);
                var vel = Velocity() * 3;
                Go_Velocity = vel;
                if (vel > 1)
                {
                    vel = 1;
                }
                animatorControler.SetFloat("InputMovmint", vel);

            }
            
            if (IsLockMove == false)
            {
               
                var Angle = x + z;
                angle = Angle;
                var animator = animatorControler;
                GoCharacterController cc = GoSystemsController.Charcter.GetComponent<GoCharacterController>();
                GoSystemsController.rigidbody.AddForce(Physics.gravity * 1, ForceMode.Acceleration);
                Sprint = cc.SprintBar;
                Sprint.maxValue = cc.TimeSprintRun;
                if (Velocity() != 0)
                {
                    IsFoods = false;
                    IsLags = false;
                }
                else
                {
                    IsFoods = true;
                    IsLags = true;
                }
                if (fastrun == true)
                {
                    if (animator.GetFloat("ActionState")< 1)
                    {
                        savespeed = speed;
                    }
                    else if (Input.GetKey(fastRun) == true && animator.GetFloat("ActionState") >0 && cc.SprintBar.value != 0 && IsJumping == false)
                    {
                        savespeed = fastRunSpeed;
                        cc.SprintBar.value -= Time.deltaTime;
                        IsRun = false;
                    }
                }
                if (cc.SprintBar.value < cc.SprintBar.maxValue && Input.GetKeyUp(fastRun))
                {
                    IsRun = true;
                }
                if(IsRun == true)
                {
                    cc.SprintBar.value += Time.deltaTime;
                    if(cc.SprintBar.value >= cc.SprintBar.maxValue && Input.GetKeyUp(fastRun)){
                        IsRun = false;
                    }
                }
                    var moveDir = UpdateDir();
                if (IsMoveVilasty == true)
                {
                    Grigidbody.velocity = new Vector3(moveDir.x * savespeed, Grigidbody.velocity.y, moveDir.z * savespeed);
                }
                if (moveDir != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                    Grigidbody.transform.rotation = Quaternion.Slerp(Grigidbody.transform.rotation, desiredRotation, 14 * Time.deltaTime);
                }
                #region animator
                if (Input.GetKey(fastRun)&&angle!=0&&fastrun==true&& cc.SprintBar.value > 0)
                {
                    animator.SetFloat("ActionState", Mathf.MoveTowards(animator.GetFloat("ActionState"), 1, Time.deltaTime*fastRunSpeed));
                }
                else if (angle == 0 || cc.SprintBar.value<=0)
                {
                    animator.SetFloat("ActionState", Mathf.MoveTowards(animator.GetFloat("ActionState"),0, Time.deltaTime * fastRunSpeed));

                }else if (Input.GetKey(fastRun)==false ||GoSystemsController.Charcter.GetComponent<GoCharacterController>().SprintBar.value==0)
                {
                    animator.SetFloat("ActionState", Mathf.MoveTowards(animator.GetFloat("ActionState"), 0, Time.deltaTime * fastRunSpeed));
                }
                #endregion
                if (x != 0 || z != 0)
                {
                    NotIdel = true;
                }
                else
                {
                    NotIdel = false;
                }
            }
            if(IsLockJump != true)
            {
                animatorControler.SetBool("Jump", IsJumping);
            }
        }
        public static Vector3 UpdateDir()
        {
            
                var cameraPos = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
                var inputPos = cameraPos * new Vector3(x, 0, z);
                var moveDir = inputPos.normalized;
                var right = Camera.main.transform.right;
                return moveDir;
            
        }

        private  Vector3 S_Crouch;
        public  void Crouch(KeyCode CrouchActive, float speed)
        {
            if (IsLockMove == false)
            {
                var Target = GoSystemsController.bones[10].transform;
                var T = ChackUp(Target);
                animatorControler.SetBool("Crouch", IsCrouch);
                
                if (Input.GetKeyDown(CrouchActive) && LockCrouch == false && RagdallActivate != true)
                {
                    CameraPositionCrouch = true;
                    IsCrouch = true;
                    fastrun = false;
                    savespeed = speed;
                    GoSystemsController.Charcter.GetComponent<CapsuleCollider>().height = GoSystemsController.Charcter.GetComponent<CapsuleCollider>().height / 2;
                    S_Crouch = GoSystemsController.Charcter.GetComponent<CapsuleCollider>().center;
                    GoSystemsController.Charcter.GetComponent<CapsuleCollider>().center = Vector3.up;
                    OnExit = new UnityEngine.Events.UnityEvent();
                    OnExit.AddListener(LockBackCrouch);

                }
                if (Input.GetKeyDown(CrouchActive)&&RagdallActivate == false && LockCrouch == true && IsCrouch == true && T != true) 
                {
                    IsCrouch = false;
                    GoSystemsController.Charcter.GetComponent<CapsuleCollider>().height = GoSystemsController.Charcter.GetComponent<CapsuleCollider>().height * 2;
                    GoSystemsController.Charcter.GetComponent<CapsuleCollider>().center = S_Crouch;
                    OnExit = new UnityEngine.Events.UnityEvent();
                    OnExit.AddListener(LockBackCrouch);
                }
                if (T == true && IsCrouch == true)
                {
                    LockJump = true;
                }
                else if (T == false && IsCrouch == true)
                {
                    LockJump = false;
                }
                if (IsGround == false && IsCrouch == true)
                {
                    fastrun = true;
                    IsCrouch = false;
                    LockCrouch = false;
                    GoSystemsController.Charcter.GetComponent<CapsuleCollider>().center = S_Crouch;
                    GoSystemsController.Charcter.GetComponent<CapsuleCollider>().height = GoSystemsController.Charcter.GetComponent<CapsuleCollider>().height * 2;

                }
                if (animatorControler.GetBool("Crouch") == true)
                {
                   
                }
            }
        }
        private void LockBackCrouch()
        {
            if (LockCrouch == true)
            {
                LockCrouch = false;
                fastrun = true;
            }
            else
            {
                LockCrouch = true;
            }
        }
        public void LockAllActions(bool Lock)
        {
            if (Lock)
            {
              
                IsActivte = false;
                IsLockMove = true;
                IsLockJump = true;
                IsRagdall = false;
                IsLockJump = true;
                IsFoods = false;
                IsFoodSound = false;
                IsLags = false;
            }
            else
            {
              
                IsActivte = true;
                IsLockMove = false;
                IsLockJump = false;
                IsRagdall = true;
                IsFoodSound = true;
                IsLockJump = false;
                IsFoods = true;
                IsLags = true;
            }
        }

        private bool ChackUp(Transform Target)
        {
            var ray = new Ray(Target.position, Vector3.up);
            bool T = Physics.Raycast(ray, 2);
            return T;
        }
        private static bool LockJump;
        private static bool LockSeeWall;
        private static float TimerJump;
        private static float Gravity = -10;
        public static void Layers(string[] df)
        {
            for (int i = 8; i <= 31; i++)
            {
                var layerN = LayerMask.LayerToName(i);
                if (layerN.Length > 0)
                    df.SetValue(layerN, i);
            }
        }
        public static float initialJumpVelocity;
        public static bool EndAnimation(string AnimationName)
        {
            bool m = animatorControler.GetCurrentAnimatorStateInfo(0).IsName(AnimationName);
            return m;
        }
        public  bool IsGraunded( LayerMask layer)
        {
            var fixposCast = new Vector3(GoSystemsController.Charcter.transform.position.x, GoSystemsController.Charcter.transform.position.y + 0.5f, GoSystemsController.Charcter.transform.position.z);
            Ray ray = new Ray(fixposCast, Vector3.down);
            IsGround = Physics.SphereCast(ray, 0.1f, out hit, 0.6f, layer);
            animatorControler.SetBool("isGrawnd", IsGround);
            Debug.DrawLine(fixposCast, new Vector3(fixposCast.x, fixposCast.y - 0.4f, fixposCast.z));
            IsGround = IsGround;
            return IsGround;

        }
        [InitializeOnLoad]
        public class Startup
        {
            static Startup()
            {
                GetGoSystems();
            }
        }
        public void JumpBehaviour(float MaxJumpTime,float JumpHeight,float jumpForce, LayerMask layer)
        {
            if (LockJump == false)
            {
                IsJumping = true;
                if (IsGraunded(layer) == true)
                {
                    GoSystems.animatorControler.SetBool("Jump", IsJumping);
                    if (IsJumping == true)
                    {
                        var timeToApox = MaxJumpTime / 2;
                        var force = jumpForce * 2;
                        Gravity = (-2 * MaxJumpTime) / Mathf.Pow(timeToApox, 2);
                        initialJumpVelocity = (2 * JumpHeight / timeToApox);
                        GoSystemsController.rigidbody.AddForce(GoSystemsController.rigidbody.velocity.x, GoSystemsController.rigidbody.velocity.y + GoSystems.initialJumpVelocity * force, GoSystemsController.rigidbody.velocity.z);
                        IsGround = false;
                        Physics.gravity = new Vector3(0, Gravity, 0);
                    }
                }
            }
        }
        public void UnLockStopWall()
        {
            Debug.Log("Stop Wall");
            IsLockMove = false;
            animatorControler.SetBool("StopWall", false);

        }
        public void Dircection(Transform MyPositin, Vector3 PointDirection, float speed = 5, bool Move = true, bool lookAt = false)
        {
            Vector3 direction = PointDirection - MyPositin.position;
            var pos = new Vector3(direction.x, direction.y, direction.z);
            Debug.DrawRay(MyPositin.position, direction, Color.red);
            direction.Normalize();
            if (Move == true)
            {
                MyPositin.Translate(direction * Time.deltaTime * speed, Space.World);
            }
            if (lookAt == true)
            {
                MyPositin.LookAt(PointDirection);
            }
        }
        public void FixRotation(Transform sorce, Transform target, float time,Vector3 offset)
        {
            Vector3 relativePos = target.position - sorce.position;
            relativePos = new Vector3(relativePos.x + offset.x, relativePos.y + offset.y, relativePos.z + offset.z);
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Quaternion current = sorce.localRotation;
            sorce.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime
                * time);
        }
        public bool ChackInToByObject(GameObject Go_Object)
        {
            foreach (Collider collider in GoTrigger.xcol)
            {
                if (collider.transform.gameObject == Go_Object)
                {
                    IntoObject = true;
                    Debug.Log("we find it");
                }
            }
            return IntoObject;
        }
        public bool ChackInToByTag(string yourObjectTag)
        {
            foreach (Collider collider in GoTrigger.xcol)
            {
                if (collider.transform.tag == yourObjectTag)
                {
                    IntoTag = true;
                }
            }
            return IntoTag;
        }
        public Vector3 FixPosition(Vector3 target, Vector3 source, float OffsetX = 0, float OffsetY = 0, float OffsetZ = 0)
        {
            var FixPos = new Vector3(target.x + OffsetX, target.y + OffsetY, target.z + OffsetZ);
            source = Vector3.Lerp(source, FixPos, 8 * Time.deltaTime);
            return source;
        }
        public void FixPosFood(float offSet,Vector3 offsetOragenFoodposition, ref LayerMask layer)
        {
            if (IsFoods == true)
            {
                if (animatorControler)
                {
                    animatorControler.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    animatorControler.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                    animatorControler.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    animatorControler.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                     RaycastHit FHit;
                     RaycastHit Lhit;
                     Ray ray = new Ray(animatorControler.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up+offsetOragenFoodposition, Vector3.down);
                    if (Physics.Raycast(ray, out Lhit, 3f, layer))
                    {
                            Vector3 footPodition = Lhit.point;
                            footPodition.y += offSet;
                            animatorControler.SetIKPosition(AvatarIKGoal.LeftFoot, footPodition);
                            animatorControler.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(animatorControler.transform.forward, hit.normal));
                            var v = animatorControler.GetIKHintPosition(AvatarIKHint.LeftKnee);  
                    }
                    ray = new Ray(animatorControler.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up+ offsetOragenFoodposition, Vector3.down);
                    if (Physics.Raycast(ray, out FHit, 3f, layer))
                    {
                            Vector3 footPodition = FHit.point;
                            footPodition.y += offSet;
                            animatorControler.SetIKPosition(AvatarIKGoal.RightFoot, footPodition);
                            animatorControler.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(animatorControler.transform.forward, hit.normal));
                        
                    }
                }
            }
        }
        public void FixPosLegs(Vector3 offsetL, Vector3 offsetR)
        {
            if (IsLags == true)
            {
                animatorControler.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 1);
                animatorControler.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 1);
                Transform orginalPosition = animatorControler.transform;
                Vector3 L = FixLegIk(orginalPosition, offsetL);
                Vector3 R = FixLegIk(orginalPosition, offsetR);
                animatorControler.SetIKHintPosition(AvatarIKHint.LeftKnee, L);
                animatorControler.SetIKHintPosition(AvatarIKHint.RightKnee, R);
            }
        }
        private Vector3 FixLegIk(Transform orginalPosition, Vector3 offset)
        {
            var point = new Vector3(orginalPosition.position.x + offset.x, orginalPosition.position.y + offset.y, orginalPosition.position.z + offset.z);
            var fixDir = point - orginalPosition.position;
            fixDir = fixDir.normalized;
            var relativeAngle = Vector3.Angle(orginalPosition.forward, fixDir);
            var localRelativePosition = orginalPosition.TransformPoint(-fixDir);
            return localRelativePosition;

        }
        private  float Velocity()
        {
            float F = z + x;
            if (x > 0 && z > 0)
            {
                velocity = x + z;
                if (velocity > 1)
                {
                    velocity = 1;
                    return velocity;
                }
                else
                {
                    return velocity;
                }
            }
            else if (x < 0 && z < 0)
            {
                velocity= x + z;
                if (-velocity > 1)
                {
                    velocity = 1;
                    return velocity;
                }
                else
                {
                    return -velocity/2;
                }
               
            }
            else if (x > 0 && z < 0)
            {
                if (x == 1)
                {
                    velocity = x;
                    return velocity;
                }
                else
                {
                    return 1;
                }
            }
            else if (x < 0 && z > 0)
            {
                if (z == 1)
                {
                    velocity = z;
                    return velocity;
                }
                else
                {
                    return 1;
                }
            }
            else if (x == 0 && z > 0)
            {
               
                return z;
            }
            else if (x > 0 && z == 0)
            {

                return x;
            }
            else if (x < 0 && z == 0)
            {

                return -x;
            }
            else if (z < 0 && x == 0)
            {

                return -z;
            }
            else
            {
                return 0;
            }
        }
        public class ReadOnlyDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property,GUIContent label)
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }

            public override void OnGUI(Rect position,
                                       SerializedProperty property,
                                       GUIContent label)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label, true);
                GUI.enabled = true;
            }
        }
    }
    [ExecuteInEditMode]
    public class DrowMithods
    {
        public static void GoDrow(Vector3 Position, Vector3 Size,string Color= "#FF8E28")
        {
            Color color;
            ColorUtility.TryParseHtmlString(Color, out color);
            color = new Color(color.r, color.g, color.b, 0.5f);
            Gizmos.color = color;
            Gizmos.DrawCube(Position, Size);

        }

    }
}
