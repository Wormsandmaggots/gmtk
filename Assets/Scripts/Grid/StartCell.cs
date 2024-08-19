using System.Collections.Generic;

namespace Grid
{
    public class StartCell : Cell
    {
        public void StartResolve()
        {
            BlockResolver.instance.AddBlockToResolve(AssociatedBox);
        }

        public override CellType GetCellType()
        {
            return CellType.Start;
        }

        public override void InvokeCellTypeRelatedMethods()
        {
            StartResolve();
        }
    }
}