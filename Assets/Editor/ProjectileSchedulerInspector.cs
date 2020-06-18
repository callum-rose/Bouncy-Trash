using UnityEngine;
using UnityEditor;
using BalsamicBits.BouncyTrash.Game.Core;
using Utils;
using BalsamicBits.BouncyTrash.Game.Projectile.Scheduler;

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

            FrameBuffer[] buffers = (target as ProjectileSchedule).Buffers;

            float frameWidth = EditorGUIUtility.currentViewWidth / buffers[0].Count;

            for (int b = 0; b < buffers.Length; b++)
            {
                FrameBuffer buffer = buffers[b];
                for (int i = 0; i < buffer.Count; i++)
                {
                    Frame frame = buffer[i];

                    Color colour;
                    if (frame.Position == null)
                    {
                        // no bounce data here
                        colour = GUI.backgroundColor;
                    }
                    else
                    {
                        colour = GetColour(frame.Position.Value, frame.DebugStorey.Value);
                    }

                    Rect tileRect = new Rect(i * frameWidth, (GameDimensions.StoreyCount - b - 1) * spacingPerStorey, frameWidth, spacingPerStorey);
                    EditorGUI.DrawRect(tileRect, colour);
                    EditorGUI.LabelField(tileRect, frame.Position?.ToString() ?? "", new GUIStyle { fontStyle = FontStyle.Bold });
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
            return new HSVColor((float)storey / GameDimensions.StoreyCount, 0.8f, (float)position / GameDimensions.StoreyCount).ToColor();
        }

        private Color GetInvColour(int position, int storey)
        {
            storey %= GameDimensions.StoreyCount;
            return new HSVColor(storey / 3f, 0.8f, position / 3f).ToColor();
        }

        #endregion
    }
}