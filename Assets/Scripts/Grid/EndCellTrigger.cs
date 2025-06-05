using System;
using System.Collections;
using System.Collections.Generic;
using Blocks;
using Blocks.Helpers;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.VFX;

namespace Grid
{
    public class EndCellTrigger : MonoBehaviour
    {
        public static List<EndCellTrigger> EndCells = new List<EndCellTrigger>();
        private EndCell Cell;
        
        private void Start()
        {
            Cell = GetComponentInParent<EndCell>();
            EndCells.Add(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Knob"))
            {
                if (BlockResolver.instance.IsResolving)
                {
                    Knob s = other.GetComponent<Knob>();
                    StartCoroutine(DelayTurnOffScaling(s.RelatedScaler));
                    AudioManager.instance.Play("dotkEnd");
                    Cell.SetFlagActive(true);
                    Vector3 dest = transform.position;
                    dest.z -= 0.2f;
                    
                    float dist = s.transform.position.z - Cell.transform.position.z;
                    if (Math.Abs(dist) > 0.001f)
                        dest.z += dist / 5;
                    Cell.SetParticleColor(s.GetColor());
                    //dest.z -= s.RelatedScaler.Direction.x;
                    Cell.MoveFlag(dest);
                }
            }
        }
        
        private IEnumerator DelayTurnOffScaling(Scaler scaler)
        {
            scaler.QueuedForTunrOff = true;
            
            yield return new WaitForSeconds(0.1f);

            scaler.ShouldExtrude = false;
            
            EndCells.Remove(this);
                    
            if(EndCells.Count < 1)
                WinScreen.instance.ShowWinScreen();
        }

        public void Reset()
        {
            Cell.Reset();
        }
    }
}