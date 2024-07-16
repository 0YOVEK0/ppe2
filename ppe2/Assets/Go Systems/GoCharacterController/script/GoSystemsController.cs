using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace GoSystem
{
    namespace Control
    {
        public class GoSystemsController
        {
            static GameObject other { get; set; }
            public static GameObject Charcter
            {
                get
                {
                    try
                    {
                        return GoSystems.Player.gameObject;
                    }
                    catch
                    {
                        return GameObject.FindWithTag("Player");
                    }
                }
            }
            public static Rigidbody rigidbody
            {
                get
                {
                    Rigidbody rb = Charcter.GetComponent<Rigidbody>();
                    return rb;
                }
            }
            private static GoCharacterController MoveCharcter
            {
                get
                {
                    GoCharacterController move = Charcter.GetComponent<GoCharacterController>();
                    return move;
                }


            }
            public static Animator animator
            {
                get => Charcter.GetComponent<Animator>();
            }
            public static List<Transform> bones = new List<Transform>();
            public static PhysicMaterial PhysicMaterialfly
            {
                get
                {
                    return (PhysicMaterial)Resources.Load("JumpMateral");
                }
            }
            public static PhysicMaterial PhysicMaterialMove
            {
                get
                {
                    return (PhysicMaterial)Resources.Load("MoveMateral");
                }
            }
            public static Collider[] IsTrigger()
            {
                Collider[] col = Physics.OverlapCapsule(Charcter.transform.position, bones[10].position, 1f);
                return col;
            }
            public static bool IsCheck(LayerMask LayerCheck = default)
            {
                return Physics.CheckCapsule(Charcter.transform.position, bones[10].position, 0.7f, LayerCheck);
            }
            public static void GoUseGravety(bool value)
            {
                Charcter.GetComponent<Rigidbody>().useGravity = value;
                Charcter.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            public static void GoisKinematic(bool value)
            {
                Charcter.GetComponent<Rigidbody>().isKinematic = value;
            }
            public static bool CheckIfPlayerNearWall()
            {
                Debug.DrawRay(bones[10].transform.position, GoSystems.UpdateDir(), Color.blue);
                return Physics.Raycast(bones[10].transform.position, GoSystems.UpdateDir(), 1f);
            }
            public static void stop()
            {
                Charcter.GetComponent<Collider>().material = PhysicMaterialfly;
            }
            public static void Move()
            {
                Charcter.GetComponent<Collider>().material = PhysicMaterialMove;
            }
            public static void Getbone()
            {
                if (bones.Count < 1)
                {
                    var anim = GoSystems.animatorControler;
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.Hips));//0
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftUpperLeg));//1
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftLowerLeg));//2
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.RightUpperLeg));//3
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.RightLowerLeg));//4
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftUpperArm));//5
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftLowerArm));//6
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.RightUpperArm));//7
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.RightLowerArm));//8
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.Chest));//9
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.Head));//10
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftFoot));//11
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.RightFoot));//12
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.LeftHand));//13
                    bones.Add(anim.GetBoneTransform(HumanBodyBones.RightHand));//14

                }
            }
            public static void ClearBone()
            {
                bones.Clear();
            }
        }
        public class GoTrigger
        {
            public static Collider[] xcol;
            static int Index;
            static int NewIndex;
            public static GameObject other { get; private set; }
            static Boolean LookEnter;
            static Boolean LookExit;
            public static void uplay()
            {
                xcol = GoSystemsController.IsTrigger();
                Index = xcol.Length;
                other = xcol[0].gameObject;
            }
            static bool ItsStay;
           public static GameObject CheckTriggerStay()
            {
                Collider[] colliders = Physics.OverlapSphere(GoSystemsController.Charcter.transform.position, 1f);
                GameObject TargetStay = null;
                foreach (Collider collider in colliders)
                {
                    if (collider != null && collider.gameObject != GoSystemsController.Charcter)
                    {
                        TargetStay = collider.gameObject;
                    }
                }
                return TargetStay;
            }

            public static bool GoEnter(GameObject gameObject = null)
            {
                if (Index != NewIndex)
                {
                    if (NewIndex < Index)
                    {
                        NewIndex = Index;
                        LookEnter = true;
                        return true;
                    }
                }
                else
                {
                    LookEnter = false;
                    return false;
                }
                LookEnter = false;

                return false;
            }
            public static bool GoExit()
            {
                if (Index != NewIndex)
                {
                    if (NewIndex > Index)
                    {
                        NewIndex = Index;
                        LookExit = true;

                        return true;
                    }
                }
                else
                {
                    LookExit = false;
                    return false;
                }
                LookExit = false;
                return false;
            }
            private static GameObject IsObject;
            public static GameObject getObjectEniter(bool scriptOrLayer, LayerMask layerName, Type script)
            {
                if (scriptOrLayer == false)
                {
                    foreach (Collider collider in xcol)
                    {
                        if ((layerName.value & (1 << collider.transform.gameObject.layer)) > 0)
                        {
                            IsObject = collider.transform.gameObject;
                            return IsObject;
                        }
                    }
                }
                else
                {
                    foreach (Collider collider in xcol)
                    {
                        if (collider.transform.GetComponent(script) != null)
                        {
                            IsObject = collider.transform.gameObject;
                            return IsObject;
                        }
                    }
                }
                return IsObject;
            }
        }
        [InitializeOnLoad]
        class HierarchyBehaviour
        {
            static Texture2D texture;
            static List<int> markedObjects;
            static HierarchyBehaviour()
            {
                texture = AssetDatabase.LoadAssetAtPath("Assets/Go Systems/GoCharacterController/script/Editor/Icon.png", typeof(Texture2D)) as Texture2D;
                EditorApplication.update += UpdateCB;
                EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
            }

            static void UpdateCB()
            {
                GameObject[] go = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];

                markedObjects = new List<int>();
                foreach (GameObject g in go)
                {
                    if (g.GetComponent<GoCharacterController>() != null)
                        markedObjects.Add(g.GetInstanceID());
                }

            }
          
            static void HierarchyItemCB(int instanceID, Rect selectionRect)
            {
                try
                {
                    if (markedObjects.Contains(instanceID))
                    {
                        GUI.DrawTexture(new Rect(selectionRect.xMax - 16, selectionRect.yMin, 22, 22), texture);
                    }
                }
                catch
                {

                }
            }
       
        }
    }

}