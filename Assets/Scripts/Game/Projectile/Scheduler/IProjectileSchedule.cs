namespace BalsamicBits.BouncyTrash.Game.Projectile.Scheduler
{
    public interface IProjectileSchedule
    {
        event SpawnEvent Spawn;

        void Begin();
        void End();
        void Cancel(int id);
    }
}