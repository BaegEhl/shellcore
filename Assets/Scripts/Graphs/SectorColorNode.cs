﻿using NodeEditorFramework.Utilities;
using UnityEngine;

namespace NodeEditorFramework.Standard
{
    [Node(false, "Actions/Set Sector Color", typeof(SectorCanvas), typeof(QuestCanvas))]
    public class SectorColorNode : Node
    {
        public override string GetName
        {
            get { return "SectorColorNode"; }
        }

        public override string Title
        {
            get { return "Set Sector Color"; }
        }

        public override Vector2 DefaultSize
        {
            get { return new Vector2(200, 120); }
        }

        [ConnectionKnob("Output", Direction.Out, "TaskFlow", NodeSide.Right)]
        public ConnectionKnob output;

        [ConnectionKnob("Input", Direction.In, "TaskFlow", NodeSide.Left)]
        public ConnectionKnob input;

        public Color color;

        public override void NodeGUI()
        {
            GUILayout.BeginHorizontal();
            input.DisplayLayout();
            output.DisplayLayout();
            GUILayout.EndHorizontal();

            GUILayout.Label("Color:");
            GUILayout.BeginHorizontal();
            var r = RTEditorGUI.FloatField(color.r);
            if (color.r < 0 || color.r > 1)
            {
                r = RTEditorGUI.FloatField(color.r = 0);
                Debug.LogWarning("Can't register this numbers!");
            }
            var g = RTEditorGUI.FloatField(color.g);
            if (color.g < 0 || color.g > 1)
            {
                g = RTEditorGUI.FloatField(color.g = 0);
                Debug.LogWarning("Can't register this numbers!");
            }
            var b = RTEditorGUI.FloatField(color.b);
            if (color.b < 0 || color.b > 1)
            {
                b = RTEditorGUI.FloatField(color.b = 0);
                Debug.LogWarning("Can't register this numbers!");
            }
            color = new Color(r, g, b);
            GUILayout.EndHorizontal();
        }

        public override int Traverse()
        {
            BackgroundScript.instance.setColor(color);
            LandPlatformGenerator.Instance.SetColor(color + new Color(0.5F, 0.5F, 0.5F));
            SectorManager.instance.overrideProperties.backgroundColor = color;

            return 0;
        }
    }
}
