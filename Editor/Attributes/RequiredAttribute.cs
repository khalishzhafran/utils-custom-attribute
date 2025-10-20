// ------------------------------------------------------------------------------
//  File: RequiredAttribute.cs
//  Author: Ran
//  Description: Attribute to mark a field as required
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
    /// Attribute to mark a field as required
    /// </summary>
    public class RequiredAttribute : PropertyAttribute
    {
        public string Message { get; private set; }

        public RequiredAttribute()
        {
            Message = "This field is required";
        }

        public RequiredAttribute(string message)
        {
            Message = message;
        }
    }

    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredDrawer : PropertyDrawer
    {
        // Used for top and bottom padding between the text and the HelpBox border.
        private const int paddingHeight = 10;

        // Used to add some margin between the HelpBox and the property below.
        private const int marginHeight = 4;

        // Custom added height for drawing text area which has the MultilineAttribute.
        private float addedHeight = 0;

        // Height of the HelpBox
        private float boxHeight = 0;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsPropertyValueNullOrEmpty(property))
            {
                return base.GetPropertyHeight(property, label) + addedHeight + paddingHeight + marginHeight;
            }

            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Store the original y position of the property, so we can reset it after drawing the property.
            float positionY = position.y;

            // If the property is null or empty, display a HelpBox above the property.
            if (IsPropertyValueNullOrEmpty(property))
            {
                RequiredAttribute requiredAttribute = attribute as RequiredAttribute;

                // Calculate the height of the HelpBox using the GUIStyle on the current skin and the inspector
                // window's currentViewWidth.
                GUIContent content = new(requiredAttribute.Message);
                GUIStyle style = GUI.skin.GetStyle("helpbox");

                boxHeight = style.CalcHeight(content, EditorGUIUtility.currentViewWidth);

                Rect helpBoxPosition = new(position.x, position.y + marginHeight * 2, position.width, boxHeight + paddingHeight);

                // Render the HelpBox in the Unity inspector UI.
                EditorGUI.HelpBox(helpBoxPosition, requiredAttribute.Message, MessageType.Error);

                // Add the height of the HelpBox and some margin to the y position.
                addedHeight = boxHeight + marginHeight * 3;

                // Set the y position to the added height, so the property is drawn below the HelpBox.
                position.y += boxHeight + paddingHeight + marginHeight * 4;
            }
            else
            {
                // Reset the added height if the property is not null or empty.
                position.y = positionY;
            }

            // Draw the property.
            EditorGUI.PropertyField(position, property, label, true);
        }

        private bool IsPropertyValueNullOrEmpty(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.String:
                    return string.IsNullOrEmpty(property.stringValue);
                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue == null;
                case SerializedPropertyType.Generic:
                    // Add more cases as needed for other property types
                    break;
            }
            return false;
        }
    }
}
#endif
