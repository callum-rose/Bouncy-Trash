using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Core
{
    public class AssetLoader<T> where T : Object
	{
        private readonly string _path;

        public AssetLoader(string path)
        {
            _path = path;
        }

		#region API

        public IEnumerable<T> Load()
        {
            var ret = Resources.LoadAll<T>(_path);
            return ret;
        }

		#endregion	
	}
}