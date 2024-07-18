using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

namespace GoSystem
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(GoSystemsBehaviour),true)]
    public class GCustomMonoBehaviourEditor : Editor
    {
         private string[] propertiesToExclude = new string[] { "m_Script" };
     //   public string[] propertiesToExclude = new string[] { "m_Script", "VECTOR3" };
        public GBehaviourAttributeAttribute headerAttribute;
        public Texture2D icon;
        public GUIStyle fontStyle;
        public GUISkin skin;
        public bool showProperties;
        private void OnEnable()
        {
            var targetObject = serializedObject.targetObject;
            var hasAttributeHeader = targetObject.GetType().IsDefined(typeof(GBehaviourAttributeAttribute), true);
            if (hasAttributeHeader)
            {
                var attributes = Attribute.GetCustomAttributes(targetObject.GetType(), typeof(GBehaviourAttributeAttribute), true);
                if (attributes.Length > 0)
                    headerAttribute = (GBehaviourAttributeAttribute)attributes[0];
              
            }

            if (headerAttribute!=null && headerAttribute.icon!="")
            {
                icon = Resources.Load(headerAttribute.icon) as Texture2D;
            }
            
        }
        public override void OnInspectorGUI()
        {
            CheckEditorProperties();
          
            serializedObject.Update();  
            GUILayout.BeginVertical(skin.window);
          
            DrawTittleHeader();
            DrawOpenCloseButton();
            DrawGUI();
           
            GUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();

          

        }

        protected virtual void DrawGUI()
        {
            if (!showProperties) return;
            //  base.OnInspectorGUI();
            //   DrawDefaultInspector();
            DrawPropertiesExcluding(serializedObject, "m_Script");
            serializedObject.ApplyModifiedProperties();
            //   base.GetHashCode()=false;

        }

        protected virtual void DrawOpenCloseButton()
        {
            showProperties = true;
           
            if (headerAttribute != null && headerAttribute.useOpenClose)
            {
                SerializedProperty isOpen = serializedObject.FindProperty("isOpen");
                bool value = isOpen.boolValue;
                EditorGUI.BeginChangeCheck();
               

                value = GUILayout.Toggle(value, value ? "Close" : "Open", skin.button);
                if(EditorGUI.EndChangeCheck())
                {
                    isOpen.boolValue = value;
                    serializedObject.ApplyModifiedProperties();
                  

                }
                showProperties = isOpen.boolValue;
                
            }
        }

        protected virtual void DrawTittleHeader()
        {
            if (headerAttribute == null) return;

            GUILayout.BeginHorizontal();
            if (icon) GUILayout.Label(icon, GUIStyle.none, GUILayout.Height(70), GUILayout.Width(70));
            GUILayout.Box(headerAttribute.tittle, fontStyle);
            GUILayout.EndHorizontal();
       
        }

        protected virtual void CheckEditorProperties()
        {
            if(skin==null)
            {
                skin = Resources.Load("GUISkin") as GUISkin;
                fontStyle = skin.label;
            }
            if (fontStyle == null)
            {
                fontStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
                fontStyle.imagePosition = ImagePosition.ImageLeft;
                fontStyle.alignment = TextAnchor.UpperLeft;
                fontStyle.fontSize = 23;
                fontStyle.fontStyle = FontStyle.Bold;
                fontStyle.wordWrap = true;
                fontStyle.clipping =  TextClipping.Clip;
              
            }
        }
    }
}