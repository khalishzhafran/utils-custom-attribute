// ------------------------------------------------------------------------------
//  File: HideIFAttribute.cs
//  Author: Ran
//  Description: Attribute to hide a field in the inspector based on a condition.
//  Created: 2025
//  
//  Copyright (c) 2025 Ran.
//  This script is part of the ran.utilities namespace.
//  Permission is granted to use, modify, and distribute this file freely
//  for both personal and commercial projects, provided that this notice
//  remains intact.
// ------------------------------------------------------------------------------

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ran.utilities.attribute
{
    /// <summary>
    /// Hide a field in the inspector if the condition is met.
    /// </summary>
    public class HideIFAttribute : PropertyAttribute
    {
        public string ConditionalFieldName { get; private set; }

        public HideIFAttribute(string conditionalFieldName)
        {
            ConditionalFieldName = conditionalFieldName;
        }
    }

    [CustomPropertyDrawer(typeof(HideIFAttribute))]
    public class HideIFDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            HideIFAttribute hideIf = attribute as HideIFAttribute;
            bool enabled = GetConditionalField(property, hideIf.ConditionalFieldName);

            return enabled ? 0 : EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            HideIFAttribute hideIf = attribute as HideIFAttribute;
            bool enabled = GetConditionalField(property, hideIf.ConditionalFieldName);

            if (!enabled) EditorGUI.PropertyField(position, property, label, true);
        }

        private bool GetConditionalField(SerializedProperty property, string fieldName)
        {
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(fieldName);
            if (sourcePropertyValue == null)
            {
                Debug.LogError($"Property {fieldName} not found");
                return false;
            }

            return sourcePropertyValue.boolValue;
        }
    }
}
#endif
