// ------------------------------------------------------------------------------
//  File: HelpAttribute.cs
//  Author: Ran
//  Description: Attribute to show a help box in the inspector.
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
    public enum HelpMessageType
    {
        Info,
        Warning,
        Error
    }

    /// <summary>
    /// Show a help box in the inspector with the specified message and type.
    /// </summary>
    public class HelpAttribute : PropertyAttribute
    {
        public string Message { get; private set; }
        public HelpMessageType MessageType { get; private set; }

        public HelpAttribute(string message, HelpMessageType messageType = HelpMessageType.Info)
        {
            Message = message;
            MessageType = messageType;
        }
    }

    [CustomPropertyDrawer(typeof(HelpAttribute))]
    public class HelpDrawer : DecoratorDrawer
    {
        // Used for top and bottom padding between the text and the HelpBox border.
        private const int paddingHeight = 10;

        // Used to add some margin between the HelpBox and the property below.
        private const int marginHeight = 4;

        // Custom added height for drawing text area which has the MultilineAttribute.
        private float addedHeight = 0;

        public override float GetHeight()
        {
            return base.GetHeight() + addedHeight + paddingHeight + marginHeight;
        }

        public override void OnGUI(Rect position)
        {
            HelpAttribute helpAttribute = (HelpAttribute)attribute;

            // Calculate the height of the HelpBox using the GUIStyle on the current skin and the inspector
            // window's currentViewWidth.
            GUIContent content = new(helpAttribute.Message);
            GUIStyle style = GUI.skin.GetStyle("helpbox");

            float height = style.CalcHeight(content, EditorGUIUtility.currentViewWidth);

            position.y += paddingHeight + marginHeight;

            // Render the HelpBox in the Unity inspector UI.
            EditorGUI.HelpBox(new Rect(position.x, position.y, position.width, height + paddingHeight), helpAttribute.Message, (MessageType)helpAttribute.MessageType);

            addedHeight = height + marginHeight;
        }
    }
}
#endif
