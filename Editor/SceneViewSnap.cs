using System;
using UnityEditor;
using UnityEngine;

namespace Meangpu
{
    [InitializeOnLoad]
    public static class SceneViewSnap
    {
        // learn from [Mastering Scene View Snap Rotation in Unity - YouTube](https://www.youtube.com/watch?v=bmmt7iZnEdU)
        static bool isPerformSnap;

        static SceneViewSnap()
        {
            SceneView.duringSceneGui += OnSceneViewDuringSceneGUI;
        }

        private static void OnSceneViewDuringSceneGUI(SceneView view)
        {
            Event e = Event.current;
            if (e?.isMouse != true) return;

            // alt shift then drag
            if (e.alt && e.shift && e.button == 0 && e.type == EventType.MouseDrag)
            {
                if (!isPerformSnap)
                {
                    isPerformSnap = true;

                    Vector3 rotation = view.rotation.eulerAngles;
                    rotation.x = Mathf.Round(rotation.x / 90) * 90;
                    rotation.y = Mathf.Round(rotation.y / 90) * 90;
                    rotation.z = Mathf.Round(rotation.z / 90) * 90;
                    if (Mathf.Approximately(Quaternion.Angle(view.rotation, Quaternion.Euler(rotation)), 0))
                    {
                        // vertical > horizontal
                        if (MathF.Abs(e.delta.x) > Mathf.Abs(e.delta.y))
                            view.rotation *= Quaternion.Euler(0f, e.delta.x < 0 ? -90 : 90, 0);
                        else
                            view.rotation *= Quaternion.Euler(e.delta.y < 0 ? -90 : 90, 0, 0);

                        if (rotation.x != 0 || rotation.z != 0)
                        {
                            Vector3 forward = view.rotation * Vector3.forward;
                            view.rotation = Quaternion.LookRotation(forward, Vector3.up);
                        }
                    }
                    else
                    {
                        view.rotation = Quaternion.Euler(rotation);
                    }
                }
                e.Use();
            }
            else if (isPerformSnap)
            {
                isPerformSnap = false;
            }
        }
    }
}