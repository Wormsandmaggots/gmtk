using System;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

namespace Blocks.Helpers
{
    public class Scaler : MonoBehaviour
    {
        private static float length = 0.5f;
        private static DropdownList<Vector3> Directions()
        {
            return new DropdownList<Vector3>()
            {
                { "Right",   Vector3.right },
                { "Back",    Vector3.back },
                { "Left", Vector3.left },
                { "Forward",    Vector3.forward }
            };
        }

        private Vector3[] dir = { Vector3.right, Vector3.back, Vector3.left, Vector3.forward };
    
        [Dropdown("Directions")]
        [SerializeField] private Vector3 direction;
        [SerializeField] private Transform endPart;
        [SerializeField] private Vector3 endPartOffset = new Vector3(0, -1, 0);
        private bool shouldExtrude = true;
        private int dirIterator = 0;
        private bool queuedForTunrOff = false;

        private void Start()
        {
            for (int i = 0; i < dir.Length; i++)
            {
                if (direction == dir[i])
                    dirIterator = i;
            }

            endPart.GetComponent<Knob>().RelatedScaler = this;
        }

        public void Scale(Vector3 newScale)
        {
            if (!shouldExtrude) return;
            
            transform.localScale = newScale;
            
            endPart.position = transform.GetChild(1).position + Quaternion.Euler(transform.parent.rotation.eulerAngles) * endPartOffset;
        }

        public RaycastHit CheckBlockCollision()
        {
            RaycastHit hit;

            Vector3 rayDir = Quaternion.Euler(transform.parent.rotation.eulerAngles).eulerAngles;

            rayDir.y /= 90;
            rayDir.y += dirIterator;
            rayDir.y = (int)rayDir.y % 4;
            
            rayDir = dir[(int)rayDir.y];
            
            Physics.Raycast(endPart.position, rayDir, out hit,
                length / 2, Settings.instance.blockLayer);
            
            return hit;
        }

        public RaycastHit CheckKnobCollision()
        {
            RaycastHit hit;

            Vector3 rayDir = Quaternion.Euler(transform.parent.rotation.eulerAngles).eulerAngles;

            rayDir.y /= 90;
            rayDir.y += dirIterator;
            rayDir.y = (int)rayDir.y % 4;
            
            rayDir = dir[(int)rayDir.y];
            
            Physics.Raycast(endPart.position + rayDir / 2, rayDir, out hit,
                length, Settings.instance.knobLayer);
            
            return hit;
        }
        
        public RaycastHit CheckLineCollision()
        {
            RaycastHit hit;

            Vector3 rayDir = Quaternion.Euler(transform.parent.rotation.eulerAngles).eulerAngles;

            rayDir.y /= 90;
            rayDir.y += dirIterator;
            rayDir.y = (int)rayDir.y % 4;
            
            rayDir = dir[(int)rayDir.y];
            
            Physics.Raycast(endPart.position + rayDir / 2, rayDir, out hit,
                length / 2, Settings.instance.lineLayer);
            
            return hit;
        }

        public Vector3 Direction => direction;

        public bool ShouldExtrude
        {
            get => shouldExtrude;
            set => shouldExtrude = value;
        }

        public bool QueuedForTunrOff
        {
            get => queuedForTunrOff;
            set => queuedForTunrOff = value;
        }
    }
}