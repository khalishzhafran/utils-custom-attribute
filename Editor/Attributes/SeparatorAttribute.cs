// Author   : Litsch

#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace ran.utilities.attribute
{
    /// <summary>
    /// A custom attribute to separate fields in the inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class SeparatorAttribute : PropertyAttribute
    {
        public string SeparatorName { get; private set; }

        public float SeparatorHeight { get; private set; } = 2;
        public float SeparatorSpacing { get; private set; } = 20;

        public SeparatorAttribute(string separatorName)
        {
            SeparatorName = separatorName;
        }

        public SeparatorAttribute(string separatorName, float separatorHeight)
        {
            SeparatorName = separatorName;
            SeparatorHeight = separatorHeight;
        }

        public SeparatorAttribute(string separatorName, float separatorHeight, float separatorSpacing)
        {
            SeparatorName = separatorName;
            SeparatorHeight = separatorHeight;
            SeparatorSpacing = separatorSpacing;
        }
    }

    [CustomPropertyDrawer(typeof(SeparatorAttribute))]
    public class SeparatorDrawer : DecoratorDrawer
    {
        // Create a separator line in the inspector.
        // Center the separator name in the middle of the line.
        // The line will be drawn from the left to the right of the inspector.
        public override void OnGUI(Rect position)
        {
            SeparatorAttribute separatorAttribute = attribute as SeparatorAttribute;

            GUIStyle style = new(GUI.skin.box);
            style.normal.textColor = Color.cyan;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;

            GUIContent separatorContent = new GUIContent(separatorAttribute.SeparatorName);
            float separatorNameWidth = GUI.skin.label.CalcSize(separatorContent).x;

            Rect leftLine = new(position.xMin, position.yMin + separatorAttribute.SeparatorSpacing + separatorAttribute.SeparatorHeight / 2f, position.width / 2f - separatorNameWidth, 1);

            Rect rightLine = new(position.xMin + position.width / 2f + separatorNameWidth, position.yMin + separatorAttribute.SeparatorSpacing + separatorAttribute.SeparatorHeight / 2f, position.width / 2f - separatorNameWidth, 1);

            EditorGUI.DrawRect(leftLine, Color.white);

            float textY = position.yMin + separatorAttribute.SeparatorSpacing + separatorAttribute.SeparatorHeight / 2f - EditorGUIUtility.singleLineHeight / 2f;
            Rect textRect = new(position.xMin, textY, position.width, EditorGUIUtility.singleLineHeight + 2f);

            EditorGUI.DrawRect(rightLine, Color.white);

            EditorGUI.LabelField(textRect, separatorAttribute.SeparatorName, style);
        }

        public override float GetHeight()
        {
            SeparatorAttribute separatorAttribute = attribute as SeparatorAttribute;

            return separatorAttribute.SeparatorHeight + separatorAttribute.SeparatorSpacing * 2;
        }
    }
}
#endif
