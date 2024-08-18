namespace Grid
{
    public class StartCell : Cell
    {
        private void StartResolve()
        {
            BlockResolver.instance.AddBlockToResolve(AssociatedBox);
        }
    }
}