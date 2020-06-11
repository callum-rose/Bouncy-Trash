using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BalsamicBits.BouncyTrash.Game.Projectile;
using BalsamicBits.BouncyTrash.Game.Core;
using Utils;
using System.Linq;

namespace BalsamicBits.BouncyTrash
{
    [CustomEditor(typeof(ProjectileSchedule))]
    internal class ProjectileSchedulerInspector : Editor
    {

        #region Unity

        public override void OnInspectorGUI()
        {
            if (!Application.isPlaying)
            {
                DrawDefaultInspector();
                return;
            }

            const float spacingPerStorey = 30;

            EditorGUILayout.Space(GameDimensions.StoreyCount * spacingPerStorey);

            FrameBuffer buffer = (target as ProjectileSchedule).Buffer;

            float frameWidth = EditorGUIUtility.currentViewWidth / buffer.Count;

            for (int i = 0; i < buffer.Count; i++)
            {
                Frame frame = buffer[i];
                for (int s = 1; s <= GameDimensions.StoreyCount; s++)
                {
                    Color colour;
                    if (!frame.Datas.Exists(d => d.DebugStorey == s))
                    {
                        // no bounce data here
                        colour = GUI.backgroundColor;
                    }
                    else
                    {
                        if (!frame.Position.HasValue)
                        {
                            colour = GUI.backgroundColor;
                        }
                        else
                        {
                            Frame.Data data = frame.Datas.FirstOrDefault(d => d.DebugStorey == s);
                            colour = GetColour(frame.Position.Value, data.DebugStorey);
                        }
                    }

                    EditorGUI.DrawRect(new Rect(i * frameWidth, (GameDimensions.StoreyCount - s) * spacingPerStorey, frameWidth, spacingPerStorey), colour);
                }
            }
        }

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        #endregion

        #region Methods

        private Color GetColourForId(int id)
        {
            float hue = id % 15 / 15f;

            HSVColor colour = new HSVColor(hue, 1, 1);

            return colour.ToColor();
        }

        private Color GetColour(int position, int storey)
        {
            return new HSVColor(storey / 3f, 0.8f, position / 3f).ToColor();
        }

        #endregion
    }
}