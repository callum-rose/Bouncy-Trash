using BalsamicBits.BouncyTrash.Core;
using BalsamicBits.BouncyTrash.Game.Bouncer;
using System;
using System.Linq;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Bouncer
{
    internal class BouncerFactory
	{
        private readonly Transform _bouncerContainer;
        private readonly BouncerLoader _loader; 

        public BouncerFactory(Transform bouncerContainer)
        {
            _bouncerContainer = bouncerContainer;
            _loader = new BouncerLoader();
        }

		#region API

        public IBouncer GetBouncer(Guid id)
        {
            var datas = _loader.Load();

            BouncerData data = datas.FirstOrDefault(d => d.Id.Equals(id));

            if (data == null)
            {
                throw new Exception($"Could not find asset with id {id}");
            }

            Bouncer bouncer = BouncerBuilder.BuildBouncerGameObject(data, _bouncerContainer);

            IBouncerMovementController movement = bouncer.gameObject.AddComponent<KeyboardBouncerMovementController>();
            bouncer.SetMovementController(movement);

            return bouncer;
        }

        public IBouncer GetDefaultBouncer()
        {
            var datas = _loader.Load();

            BouncerData data = datas.FirstOrDefault(d => d.IsTheDefaultBouncer);

            if (data == null)
            {
                throw new Exception($"Could not find asset with default bouncer flag. This should've been caught by unit test?");
            }

            return BouncerBuilder.BuildBouncerGameObject(data, _bouncerContainer);
        }

		#endregion	

		#region Methods



		#endregion	
	}
}