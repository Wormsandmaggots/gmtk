using UnityEngine;

namespace Grid
{
    public class Cell : MonoBehaviour
    {
        private BoxBase associatedBox;

        public BoxBase AssociatedBox
        {
            get => associatedBox;
            set => associatedBox = value;
        }

        //when block gets on grid cell
        public virtual void OnBlockOccupy(BoxBase box)
        {
            AssociatedBox = box;
            Debug.Log("OCCUPIED");
        }
    }
}