using BalsamicBits.BouncyTrash.Game.Core;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    public class Frame : IResettable
    {
        public Frame()
        {
            Datas = new List<Data>(GameDimensions.StoreyCount);
        }

        public int? Position { get; private set; }
        public List<Data> Datas { get; }

        public void SetPosition(int position)
        {
            if (Position.HasValue)
            {
                if (position != Position.Value)
                {
                    throw new InvalidOperationException("Position can't be changed");
                }

                return;
            }

            if (position < 1 || position > GameDimensions.PositionCount)
            {
                throw new ArgumentOutOfRangeException($"Position must be between 1 and {GameDimensions.PositionCount}");
            }

            Position = position;
        }

        public void AddId(int id, int storey)
        {
            if (Datas.Count >= GameDimensions.StoreyCount)
            {
                throw new InvalidOperationException($"List of ids is already at capacity");
            }

            Data newData = new Data
            {
                Id = id,
                DebugStorey = storey
            };

            Datas.Add(newData);
        }

        public void RemoveId(int id)
        {
            Datas.RemoveAll(d => d.Id == id);
        }

        public void Reset()
        {
            Position = null;
            Datas.Clear();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Pos:");
            sb.Append(Position);
            sb.Append(": ");
            foreach (Data d in Datas)
            {
                sb.Append($"Id:{d.Id}, S:{d.DebugStorey}");
            }

            return sb.ToString();
        }

        public struct Data
        {
            public int Id { get; set; }
            public int DebugStorey { get; set; }
        }
    }
}