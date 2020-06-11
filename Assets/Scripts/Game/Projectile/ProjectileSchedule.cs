using Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using static BalsamicBits.BouncyTrash.Game.Core.GameDimensions;
using static BalsamicBits.BouncyTrash.Game.Core.GameTimings;
using BalsamicBits.BouncyTrash.Extensions;
using BalsamicBits.BouncyTrash.Game.Core;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    public class ProjectileSchedule : MonoBehaviour, IProjectileSchedule
    {
        public event SpawnEvent Spawn;

        public FrameBuffer Buffer => _frameBuffer;

        private const int _framesPerSmallBounce = 2;
        private float FrameDuration => SmallestBounceDuration / _framesPerSmallBounce;

        private FrameBuffer _frameBuffer;

        private int _currentId = 0;

        private Coroutine _spawnRoutine;

        #region Unity

        private void Awake()
        {
            int capacity = _framesPerSmallBounce * PositionCount * StoreyCount + 1;
            _frameBuffer = new FrameBuffer(capacity);
        }

        #endregion

        #region API

        public void Begin()
        {
            _spawnRoutine = StartCoroutine(SpawnNextRoutine());
        }

        public void End()
        {
            this.StopCoroutineChecked(_spawnRoutine);
        }

        public void Cancel(int id)
        {
            _frameBuffer.ResetFrames(id);
        }

        #endregion

        #region Routines

        private IEnumerator SpawnNextRoutine()
        {
            yield return GetWaiterForNextFrame();

            IEnumerator<int> nextStorey = GetNextStorey();

            int test = 0;

            while (true)
            {
                if (!nextStorey.MoveNext())
                {
                    yield break;
                }

                int storey = nextStorey.Current;

                int[] potentialFrameIndicies = GetPotentialFrameIndices(storey);
                if (CanInsertIntoBuffer(potentialFrameIndicies))
                {
                    InsertFramesIntoBufferFor(potentialFrameIndicies, storey, out int id);

                    Spawn?.Invoke(storey, id);
                }

                _frameBuffer.Shift();

                test++;

                yield return GetWaiterForNextFrame();
            }
        }

        private IEnumerator<int> GetNextStorey()
        {
            int nextStorey = 1;
            while (true)
            {
                yield return Random.Range(1, StoreyCount + 1);

                nextStorey++;
                if (nextStorey > 3)
                {
                    nextStorey = 1;
                }
            }
        }

        #endregion

        #region Methods

        private void InsertFramesIntoBufferFor(int[] newFrameIndicies, int storey, out int idUsed)
        {
            for (int i = 0; i < newFrameIndicies.Length; i++)
            {
                int frameIndex = newFrameIndicies[i];
                _frameBuffer[frameIndex].SetPosition(i + 1);
                _frameBuffer[frameIndex].AddId(_currentId, storey);
            }

            idUsed = _currentId;

            _currentId++;
        }

        private bool CanInsertIntoBuffer(int[] frameIndicies)
        {
            for (int position = 0; position < frameIndicies.Length; position++)
            {
                int frameIndex = frameIndicies[position];

                Frame frame = _frameBuffer[frameIndex];

                if (frame.Position.HasValue)
                {
                    if (frame.Position != position)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int[] GetPotentialFrameIndices(int storey)
        {
            int[] frameIndices = new int[PositionCount];

            int framesBetweenBounces = _framesPerSmallBounce * storey;

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

        #endregion
    }
}