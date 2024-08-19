using System;
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
        [SerializeField] private Transform endPart;
        [SerializeField] private Vector3 endPartOffset = new Vector3(0, -1, 0);

        public void Scale(Vector3 newScale)
        {
            transform.localScale = newScale;
            
            endPart.position = transform.GetChild(1).position + endPartOffset;
        }

        public Vector3 Direction => direction;
    }
}