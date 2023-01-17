using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class HelpHierarchyColor : EditorWindow
    {
        [MenuItem("HierarchyColor/Colors")]
        private static void ShowWindow()
        {
            var window = GetWindow<HelpHierarchyColor>();
            window.titleContent = new GUIContent("HierarchyColors");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Code and Colors: ", EditorStyles.boldLabel);
            GUILayout.Label("* :  Yellow");
            GUILayout.Label("- :  Magenta");
            GUILayout.Label("% :  Cyan");
            GUILayout.Label("+ :  Green");
            GUILayout.Label("^ :  White");
            GUILayout.Label("& :  Blue");
            GUILayout.Label("# :  Red");
            GUILayout.Label("= :  Black Purple");
        }
    }
}