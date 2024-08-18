using UnityEngine;

namespace Blocks
{
    public class StartBlock : BoxBase
    {
        [SerializeField] private BoxBase associatedBlock;
        
        public override void Execute()
        {
            associatedBlock.Execute();
        }
    }
}