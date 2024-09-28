using System;
using System.Collections;
using Blocks.Helpers;
using DefaultNamespace;
using UnityEngine;

namespace Blocks
{
    public class Knob : MonoBehaviour
    {
        private static float delay = 0.5f;
        
        private Scaler relatedScaler;
        private ExtrudingBox relatedBox;

        private void Start()
        {
            relatedBox = GetComponentInParent<ExtrudingBox>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!BlockResolver.isResolving || 
                relatedBox.ForceStop1) return;
            
            LayerMask layer = other.gameObject.layer;
            if (layer == LayerMask.NameToLayer("BlockBase"))
            {
                Debug.Log("WITH BLOCK COLLISION");
                
                relatedScaler.ShouldExtrude = false;
                var box = other.GetComponentInParent<BoxBase>();
                
                if (box != null)
                {
                    box.Execute(relatedBox);
                }
            }

            if (layer == LayerMask.NameToLayer("Knob"))
            {
                relatedScaler.ShouldExtrude = false;
                Debug.Log("KNOB COLLISION");
            }

            if (layer == LayerMask.NameToLayer("Line"))
            {
                relatedScaler.ShouldExtrude = false;
                Debug.Log("LINE COLLISION");
            }

            if (layer == LayerMask.NameToLayer("Wall"))
            {                
                relatedScaler.ShouldExtrude = false;
                Debug.Log("WALL COLLISION");
            }
        }

        private void Update()
        {
            if (!BlockResolver.isResolving) return;
        }

        private IEnumerator DelayScaleTurnOff()
        {
            yield return new WaitForSeconds(delay);

            relatedScaler.ShouldExtrude = false;
        }

        public Scaler RelatedScaler
        {
            get => relatedScaler;
            set => relatedScaler = value;
        }
    }
}