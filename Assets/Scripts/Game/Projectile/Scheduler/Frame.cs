using BalsamicBits.BouncyTrash.Game.Core;
using System;
using System.Text;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Scheduler
{
    public struct Frame
    {
        public Frame(int id, int position, int storey)
        {
            if (position < 1 || position > GameDimensions.PositionCount)
            {
                throw new ArgumentOutOfRangeException($"Position can't be {position}, must be between 1 and {GameDimensions.PositionCount}");
            }

            Position = position;

            Id = id;

            if (storey < 1 || storey > GameDimensions.StoreyCount)
            {
                throw new ArgumentOutOfRangeException($"Storey can't be {storey}, must be between 1 and {GameDimensions.StoreyCount}");
            }

            DebugStorey = storey;
        }

        public int? Position { get; private set; }
        public int? Id { get; set; }
        public int? DebugStorey { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Pos:");
            sb.Append(Position);

            sb.Append(", Id:");
            sb.Append(Id);

            sb.Append(", Storey:");
            sb.Append(DebugStorey);

            return sb.ToString();
        }
    }
}