using System;
using UnityEngine;
using BalsamicBits.BouncyTrash.Game.Projectile.Path;
using BalsamicBits.BouncyTrash.Game.Debug;
using BalsamicBits.BouncyTrash.Game.Core;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class ProjectileMover : IHasPosition, IDisposable
    {
        public IPathFactory PathFactory { get; set; }

        public int CurrentPosition => GameDimensions.GetPositionIndexFrom(_pathTraverser.CurrentPosition);

        public event Action<ProjectileMover> ReachedPathEnd;
        public event Action<ProjectileMover> ReachedDeadZone;

        private readonly Transform _projectileTransform;
        private readonly int _storey;

        private PathTraverser _pathTraverser;

        private int _pathNum = -1;

#if UNITY_EDITOR
        private DebugPathDrawer _pathDrawer;
#endif

        public ProjectileMover(Transform projectileTransform, IPathFactory pathFactory, int storey)
        {
            _projectileTransform = projectileTransform;

            PathFactory = pathFactory;

            _storey = storey;
        }

        public void Dispose()
        {
#if UNITY_EDITOR
            if (_pathDrawer == null)
            {
                return;
            }

            UnityEngine.Object.Destroy(_pathDrawer.gameObject);
#endif
        }


        #region API

        public void SetNextPath()
        {
            _pathNum++;

            IPath newPath = PathFactory.CreateInstance(_storey);
            if (_pathNum == 0)
            {
                newPath = newPath
                    .GetStartAboveOrigin()
                    .GetFirstHalfFlattened(_storey);
            }
            else
            {
                // follow on from previous
                IPath previousPath = _pathTraverser.Path;
                newPath = newPath.GetFollowOnFrom(previousPath);
            }

            // get the time the last path was over by to add to the next
            float carryOverTime = _pathTraverser?.CarryOverTime ?? 0f;

            _pathTraverser = new PathTraverser(newPath, GameTimings.GetProjectileDuration(_storey));
            _pathTraverser.Advance(carryOverTime);

            // set position to start
            SetTransformPositionToTraverser();

#if UNITY_EDITOR
            if (_pathDrawer != null)
            {
                UnityEngine.Object.Destroy(_pathDrawer.gameObject);
            }

            _pathDrawer = new GameObject("Path Drawer").AddComponent<DebugPathDrawer>();
            _pathDrawer.SetPath(newPath);
#endif
        }

        public void Tick(float deltaTime)
        {
            _pathTraverser.Advance(deltaTime);

            if (_pathTraverser.Progress >= 1f)
            {
                ReachedPathEnd?.Invoke(this);
            }

            SetTransformPositionToTraverser();

            if (GameDimensions.IsInDestroyZone(_pathTraverser.CurrentPosition))
            {
                ReachedDeadZone.Invoke(this);
            }
        }

        #endregion

        #region Methods

        private void SetTransformPositionToTraverser()
        {
            _projectileTransform.localPosition = _pathTraverser.CurrentPosition;
        }

        #endregion
    }
}