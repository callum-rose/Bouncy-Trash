namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    public interface IProjectileSchedule
    {
        event SpawnEvent Spawn;

        void Begin();
        void End();
        void Cancel(int id);
    }

    public delegate void SpawnEvent(int storey, int id);
}