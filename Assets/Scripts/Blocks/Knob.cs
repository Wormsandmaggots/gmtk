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
        private void OnTriggerEnter(Collider other)
        {
            // if (other.gameObject.layer == LayerMask.NameToLayer("Block"))
            // {
            //     if (BlockResolver.instance.IsResolving)
            //     {
            //         if (other.TryGetComponent(out BoxBase box))
            //         {
            //             box.Execute(transform.parent.parent.GetComponent<BoxBase>());
            //             StartCoroutine(DelayScaleTurnOff());
            //             return;
            //         }
            //
            //         if (other.TryGetComponent(out Knob knob))
            //         {
            //             relatedScaler.ShouldExtrude = false;
            //         }
            //     }
            // }
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