namespace BalsamicBits.BouncyTrash.Game.Projectile.Path
{
    internal interface IPathFactory
    {
        IPath CreateInstance(int storey);
    }
}