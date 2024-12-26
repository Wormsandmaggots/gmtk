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
        [SerializeField] private VisualEffect effect;
        
        private void Start()
        {
            EndCells.Add(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Knob"))
            {
                if (BlockResolver.instance.IsResolving)
                {
                    StartCoroutine(DelayTurnOffScaling(other.GetComponent<Knob>().RelatedScaler));
                    AudioManager.instance.Play("dotkEnd");
                    if(effect != null)
                        effect.Play();
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
    }
}