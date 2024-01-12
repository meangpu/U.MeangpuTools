// learn from [The QUICKEST window on Unity - YouTube](https://www.youtube.com/watch?v=tTf8UUOxRnc)
// is create alt+b to quick select function

// using UnityEditor;
// using UnityEditor.ShortcutManagement;
// using UnityEngine;

// namespace Meangpu
// {
//     public class QuickAltAction : EditorWindow
//     {
//         GUIContent[] _content = null;
//         GUIStyle _contentStyle = null;
//         bool _selectionMade = true;
//         int _selectionIndex = 0;

//         [Shortcut("QuickAltAction", KeyCode.B, ShortcutModifiers.Alt)]
//         static void ShowSceneViewBookmarksPopUpWindow()
//         {
//             QuickAltAction dialog = CreateWindow<QuickAltAction>();
//             dialog.titleContent = new GUIContent("QuickAltAction");

//             Vector2 screenSize = new(Screen.currentResolution.width, Screen.currentResolution.height);
//             Vector2 dialogSize = screenSize * .3f;
//             dialog.position = new((screenSize - dialogSize) * .5f, dialogSize);

//             dialog.ShowModalUtility();
//         }

//         private void OnEnable()
//         {
//             _content = new GUIContent[5];
//             for (int i = 0, length = _content.Length; i < length; i++)
//             {
//                 _content[i] = new($"Content {i}");
//             }
//         }

//         private void OnGUI()
//         {
//             if (Event.current != null && Event.current.modifiers == EventModifiers.Alt)
//             {
//                 if (Event.current.keyCode == KeyCode.B && !_selectionMade)
//                 {
//                     _selectionMade = true;
//                     _selectionIndex++;
//                     if (_selectionIndex >= _content.Length) _selectionIndex = 0;

//                     Repaint();
//                 }

//                 if (Event.current.type == EventType.KeyUp)
//                 {
//                     _selectionMade = false;
//                 }
//             }
//             else
//             {
//                 SelectContent(_selectionIndex);
//             }

//             _contentStyle ??= new GUIStyle(GUI.skin.button)
//             {
//                 fixedHeight = 128,
//                 fixedWidth = 128,
//                 fontSize = 14
//             };

//             int selectionIndex = GUILayout.SelectionGrid(_selectionIndex, _content, _content.Length, _contentStyle);
//             if (selectionIndex != _selectionIndex) SelectContent(selectionIndex);
//         }

//         void SelectContent(int index)
//         {
//             Debug.Log(_content[index].text);
//             Close();
//             GUIUtility.ExitGUI();
//         }
//     }
// }