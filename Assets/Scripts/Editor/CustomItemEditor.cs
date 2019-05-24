using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class CustomItemEditor : Editor
{
    private Item item;
    private uint confirmedHeight;
    private uint confirmedWidth;

    private void OnEnable()
    {
        item = (Item)target;
        confirmedHeight = item.height;
        confirmedWidth = item.width;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Confirm Size"))
        {
            if (item.height > 0 && item.width > 0)
            {
                confirmedHeight = item.height;
                confirmedWidth = item.width;
                item.shapeArray = new bool[confirmedHeight * confirmedWidth];
                for (int y = 0; y < confirmedHeight; y++)
                {
                    for (int x = 0; x < confirmedWidth; x++)
                    {
                        item.shapeArray[y * confirmedWidth + x] = false;
                    }
                }
                EditorUtility.SetDirty(target);
            }
        }
        if (GUILayout.Button("Fill"))
        {
            if (confirmedHeight > 0 && confirmedWidth > 0)
            {
                for (int i = 0; i < confirmedHeight * confirmedWidth; i++)
                {
                    item.shapeArray[i] = true;
                    EditorUtility.SetDirty(target);
                }
            }
        }
        if (GUILayout.Button("Clear"))
        {
            if (confirmedHeight > 0 && confirmedWidth > 0)
            {
                for (int i = 0; i < confirmedHeight * confirmedWidth; i++)
                {
                    item.shapeArray[i] = false;
                    EditorUtility.SetDirty(target);
                }
            }
        }
        if (GUILayout.Button("Erase"))
        {
            confirmedHeight = 0;
            confirmedWidth = 0;
            item.width = 0;
            item.height = 0;
            item.shapeArray = new bool[0];
            EditorUtility.SetDirty(target);
        }
        GUILayout.EndHorizontal();

        if (item.shapeArray != null && item.shapeArray.Length > 0)
        {
            GUILayout.Label("Check boxes to draw the shape of the item.");
            for (int y = (int)confirmedHeight - 1; y >= 0; y--)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < confirmedWidth; x++)
                {
                    bool create = EditorGUILayout.Toggle(item.shapeArray[y * confirmedWidth + x], GUILayout.Width(20));
                    item.shapeArray[y * confirmedWidth + x] = create;

                }
                EditorUtility.SetDirty(target);
                GUILayout.EndHorizontal();
            }

            if(GUILayout.Button("Save")){
                AssetDatabase.SaveAssets();
            }
        }

    }
}
