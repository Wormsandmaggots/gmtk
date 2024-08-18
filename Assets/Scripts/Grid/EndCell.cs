using UnityEngine;

namespace Grid
{
    public class EndCell : Cell
    {
        public override void OnBlockOccupy(BoxBase box)
        {
            Debug.Log("Win");
        }
    }
}