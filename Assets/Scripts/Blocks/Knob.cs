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

        private void Update()
        {
            if (!BlockResolver.isResolving) return;
            //
            // RaycastHit hit;
            //
            // // Vector3 rayDir = Quaternion.Euler(transform.parent.rotation.eulerAngles).eulerAngles;
            // //
            // // rayDir.y /= 90;
            // // rayDir.y += dirIterator;
            // // rayDir.y = (int)rayDir.y % 4;
            // //
            // // rayDir = dir[(int)rayDir.y];
            //
            // Physics.Raycast(transform.position, transform.forward, out hit,
            //     1f, Settings.instance.blockLayer);
            //
            // //Debug.Log(endPart.position);
            // //Debug.DrawRay(endPart.position, rayDir * 10, Color.blue);
            // //Debug.DrawLine(endPart.position, endPart.position + rayDir * length, Color.blue);
            //
            // if (hit.collider != null)
            // {
            //     Debug.Log("DUPA");
            //     hit.transform.GetComponent<BoxBase>().Execute(null);
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