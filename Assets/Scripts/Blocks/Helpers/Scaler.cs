using System;
using DefaultNamespace;
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
            
            endPart.position = transform.GetChild(1).position + Quaternion.Euler(transform.parent.rotation.eulerAngles) * endPartOffset;
        }

        public RaycastHit CheckBlockCollision()
        {
            RaycastHit hit;

            Vector3 dir = Quaternion.Euler(transform.parent.rotation.eulerAngles).eulerAngles;

            if (dir.magnitude == 0)
                dir = direction;
            
            Physics.Raycast(endPart.position, dir,
                out hit, Single.PositiveInfinity, Settings.instance.blockLayer | Settings.instance.cellLayer);
            
            Debug.DrawRay(endPart.position, Quaternion.Euler(transform.parent.rotation.eulerAngles).eulerAngles * 1000, Color.white);

            return hit;
        }

        public Vector3 Direction => direction;
    }
}