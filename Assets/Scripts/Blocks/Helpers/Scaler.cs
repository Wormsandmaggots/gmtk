using NaughtyAttributes;
using UnityEngine;

namespace Blocks.Helpers
{
    public class Scaler : MonoBehaviour
    {
        private static DropdownList<Vector3> Directions()
        {
            return new DropdownList<Vector3>()
            {
                { "Right",   Vector3.right },
                { "Left",    Vector3.left },
                { "Forward", Vector3.forward },
                { "Back",    Vector3.back }
            };
        }
    
        [Dropdown("Directions")]
        [SerializeField] private Vector3 direction;

        public Vector3 Direction => direction;
    }
}