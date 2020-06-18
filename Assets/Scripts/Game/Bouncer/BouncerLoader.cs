using BalsamicBits.BouncyTrash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalsamicBits.BouncyTrash.Game.Bouncer
{
    public class BouncerLoader : AssetLoader<BouncerData>
    {
        public BouncerLoader() : base(ResourcePaths.Bouncers)
        {
        }
    }
}
