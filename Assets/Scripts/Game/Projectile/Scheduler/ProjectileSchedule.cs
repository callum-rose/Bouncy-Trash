using System.Collections;
using UnityEngine;
using System.Text;
using static BalsamicBits.BouncyTrash.Game.Core.GameDimensions;
using static BalsamicBits.BouncyTrash.Game.Core.GameTimings;
using BalsamicBits.BouncyTrash.Extensions;
using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine.Assertions;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Scheduler
{
    public class ProjectileSchedule : MonoBehaviour, IProjectileSchedule
    {
        [SerializeField] private float timeScaleTest;

        public event SpawnEvent Spawn;

        public FrameBuffer[] Buffers { get; private set; }

        private IStoreySelector _storeySelector = new RandomStoreySelector();

        private int _currentId = 0;

        private Coroutine _spawnRoutine;

        #region Unity

        private void Awake()
        {
            InitBuffers();
        }

        #endregion

        #region API

        public void Begin()
        {
            GameTimings.TimeScale = timeScaleTest;

            _spawnRoutine = StartCoroutine(SpawnNextRoutine());
        }

        public void End()
        {
            this.StopCoroutineChecked(_spawnRoutine);
        }

        public void Cancel(int id)
        {
            foreach (var fb in Buffers)
            {
                fb.ResetFrames(id);
            }
        }

        #endregion

        #region Routines

        private IEnumerator SpawnNextRoutine()
        {
            // wait for a frame so GetWaiterForNextFrame doesn't return wait for time 0
            yield return null;

            while (true)
            {
                yield return GetWaiterForNextFrame();

                int storey = _storeySelector.GetNext();

                int[] potentialFrameIndicies = GetPotentialFrameIndices(storey);
                if (CanInsertIntoBuffer(potentialFrameIndicies))
                {
                    InsertFramesIntoBufferFor(potentialFrameIndicies, storey, out int id);

                    Spawn?.Invoke(storey, id);
                }

                //PrintBuffer();

                foreach (var fb in Buffers)
                {
                    fb.Shift();
                }
            }
        }

        #endregion

        #region Methods

        private void InsertFramesIntoBufferFor(int[] newBounceFrameIndicies, int storey, out int idUsed)
        {
            FrameBuffer bufferToUse = Buffers[storey - 1];
            for (int position = 1; position <= newBounceFrameIndicies.Length; position++)
            {
                int frameIndex = newBounceFrameIndicies[position - 1];

                bufferToUse[frameIndex] = new Frame(_currentId, position, storey);
            }

            idUsed = _currentId;

            _currentId++;
        }

        private bool CanInsertIntoBuffer(int[] frameIndicies)
        {
            for (int i = 0; i < frameIndicies.Length; i++)
            {
                int frameIndex = frameIndicies[i];
                int bouncePosition = i + 1;

                // check this frame in each buffer
                for (int b = 0; b < Buffers.Length; b++)
                {
                    FrameBuffer buffer = Buffers[b];

                    Frame frame = buffer[frameIndex];

                    if (frame.Id == null)
                    {
                        // is empty, move on
                        continue;
                    }

                    // frame has bounce

                    Assert.IsTrue(frame.Id.HasValue);
                    Assert.IsTrue(frame.Position.HasValue);
                    Assert.IsTrue(frame.DebugStorey.HasValue);

                    if (frame.Position.Value != bouncePosition)
                    {
                        // bounce at this frame is at a different position so can't insert
                        return false;
                    }
                }
            }

            // no clashes found

            return true;
        }

        private int[] GetPotentialFrameIndices(int storey)
        {
            int[] frameIndices = new int[PositionCount];

            int framesBetweenBounces = FramesPerSmallBounce * storey;

            for (int i = 0; i < PositionCount; i++)
            {
                frameIndices[i] = framesBetweenBounces * (i + 1);
            }

            return frameIndices;
        }

        private WaitForGameTime GetWaiterForNextFrame()
        {
            float time = Mathf.Ceil(GameTimings.Time / FrameDuration) * FrameDuration;
            return new WaitForGameTime(time);
        }

        private void InitBuffers()
        {
            Buffers = new FrameBuffer[StoreyCount];
            int capacity = FramesPerSmallBounce * PositionCount * StoreyCount + 1;
            for (int i = 0; i < StoreyCount; i++)
            {
                Buffers[i] = new FrameBuffer(capacity);
            }
        }

        private void PrintBuffer()
        {
            StringBuilder sb = new StringBuilder();
            for (int b = Buffers.Length - 1; b >= 0; b--)
            {
                sb.Append(Buffers[b].ToString());
                sb.AppendLine();
            }

            UnityEngine.Debug.Log(sb.ToString());
        }

        #endregion
    }
}