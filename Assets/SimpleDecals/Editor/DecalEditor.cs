﻿using UnityEngine;
using UnityEditor;
using kTools.Decals;

namespace kTools.DecalsEditor
{
    [CustomEditor(typeof(Decal))]
    public class DecalEditor : Editor
    {
        // -------------------------------------------------- //
        //                    EDITOR STYLES                   //
        // -------------------------------------------------- //

        internal class Styles
        {
            public static GUIContent propertiesText = EditorGUIUtility.TrTextContent("Properties");
            public static GUIContent decalDataText = EditorGUIUtility.TrTextContent("Decal Data");
            public static GUIContent toolsText = EditorGUIUtility.TrTextContent("Tools");
        }

        // -------------------------------------------------- //
        //                   PRIVATE FIELDS                   //
        // -------------------------------------------------- //

        Decal m_ActualTarget;

        // -------------------------------------------------- //
        //                   PUBLIC METHODS                   //
        // -------------------------------------------------- //

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProperties();
            DrawTools();
            serializedObject.ApplyModifiedProperties();
        }

        // -------------------------------------------------- //
        //                   PRIVATE METHODS                  //
        // -------------------------------------------------- //

        private void OnEnable()
        {
            m_ActualTarget = (Decal)target;
        }

        // Draw Properties fields section
        private void DrawProperties()
        {
            EditorGUILayout.LabelField(Styles.propertiesText, EditorStyles.boldLabel);
            EditorGUI.BeginChangeCheck();
            var decalData = (DecalData)EditorGUILayout.ObjectField(m_ActualTarget.decalData, typeof(DecalData), false);
            if (EditorGUI.EndChangeCheck())
            {
                m_ActualTarget.SetDecalData(decalData);
            }
            EditorGUILayout.Space();
        }

        // Draw Tools fields section
        private void DrawTools()
        {
            EditorGUILayout.LabelField(Styles.toolsText, EditorStyles.boldLabel);
            if(GUILayout.Button("Orientate to nearest face"))
            {
                OnClickOrientateToNearestFace();
            }
            if(GUILayout.Button("Snap to nearest face"))
            {
                OnClickSnapToNearestFace();
            }
            EditorGUILayout.Space();
        }

        // Called when "Orientate to nearest face" button is clicked
        private void OnClickOrientateToNearestFace()
        {
            Vector3 directionVector = DecalUtil.GetDirectionToNearestFace(m_ActualTarget);
            m_ActualTarget.SetDecalTransform(m_ActualTarget.transform.position, directionVector, m_ActualTarget.transform.lossyScale);
            m_ActualTarget.SetDecalData(m_ActualTarget.decalData);
        }

        // Called when "Snap to nearest face" button is clicked
        private void OnClickSnapToNearestFace()
        {
            Vector3 position;
            Vector3 directionVector = DecalUtil.GetDirectionToNearestFace(m_ActualTarget, out position);
            m_ActualTarget.SetDecalTransform(position, directionVector, m_ActualTarget.transform.lossyScale);
            m_ActualTarget.SetDecalData(m_ActualTarget.decalData);
        }
    }
}