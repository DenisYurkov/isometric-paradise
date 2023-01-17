using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [InitializeOnLoad]
    public class HierarchyColor
    {
        static HierarchyColor() => EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindow;

        private static void HandleHierarchyWindow(int instanceID, Rect selectRect)
        {
            var instance = EditorUtility.InstanceIDToObject(instanceID);

            if (instance != null)
            {
                char start = instance.ToString()[0];
                string colorText;
                
                switch (start)
                {
                    case '*':
                        colorText = "purple";
                    
                        EditorGUI.DrawRect(selectRect, Color.yellow);
                        RenderText(selectRect, colorText, instance);
                        break;
                    
                    case '-':
                        colorText = "black";
                
                        EditorGUI.DrawRect(selectRect, Color.magenta);
                        RenderText(selectRect, colorText, instance);
                        break;
                    
                    case '%': 
                        colorText = "#4C5270";
                
                        EditorGUI.DrawRect(selectRect, Color.cyan);
                        RenderText(selectRect, colorText, instance);
                        break;
                    
                    case '+':
                        colorText = "black";
                
                        EditorGUI.DrawRect(selectRect, Color.green);
                        RenderText(selectRect, colorText, instance);
                        break;
                    
                    case '^':
                        colorText = "black";
                
                        EditorGUI.DrawRect(selectRect, Color.white);
                        RenderText(selectRect, colorText, instance);
                        break;
                    
                    case '&':
                        colorText = "white";
                
                        EditorGUI.DrawRect(selectRect, Color.blue);
                        RenderText(selectRect, colorText, instance);
                        break;
                    
                    case '#':
                        colorText = "white";
                
                        EditorGUI.DrawRect(selectRect, Color.red);
                        RenderText(selectRect, colorText, instance);
                        break;
                    case '=':
                        colorText = "#F7FFE6";

                        EditorGUI.DrawRect(selectRect, new Color(0.35f, 0.26f, 0.46f));
                        RenderText(selectRect, colorText, instance);
                        break;
                }
            }
        }

        private static void RenderText(Rect selectRect, string colorText, Object instance)
        {
            var guiStyle = new GUIStyle
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter
            };

            var text = $"<b><size=13><color={colorText}>" + instance.name.Remove(0, 1) + "</color></size></b>";
            EditorGUI.LabelField(selectRect, text, guiStyle);
        }
    }
}