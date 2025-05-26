using System;
using System.Collections;
using Blocks.Helpers;
using DefaultNamespace;
using Grid;
using UnityEngine;
using UnityEngine.VFX;

namespace Blocks
{
    public class Knob : MonoBehaviour, IGetColor
    {
        private static float delay = 0.5f;
        
        private VisualEffect bumpEffect;
        
        private Scaler relatedScaler;
        private ExtrudingBox relatedBox;
        private Color color;
        private Color bumpedColor;

        private void Start()
        {
            color = GetComponent<MeshRenderer>().material.GetColor("_Color");
            relatedBox = GetComponentInParent<ExtrudingBox>();
            bumpEffect = GetComponentInChildren<VisualEffect>();
            
            if (bumpEffect != null)
            {
                bumpEffect.SetVector4("OriginColor", color);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!BlockResolver.isResolving || 
                relatedBox.ForceStop1 || 
                !relatedScaler.ShouldExtrude) return;
            
            LayerMask layer = other.gameObject.layer;

            if (layer == LayerMask.NameToLayer("Cell"))
            {
                var cell = other.GetComponentInParent<Cell>();

                if (cell &&
                    relatedBox.OverCell &&
                    cell.GetFloor() > relatedBox.OverCell.GetFloor())
                {
                    relatedScaler.ShouldExtrude = false;
                    //Debug.Log("CELL COLLISION");  
                }
            }
            
            if (layer == LayerMask.NameToLayer("BlockBase"))
            {
                relatedScaler.ShouldExtrude = false;
                var box = other.GetComponentInParent<BoxBase>();
                
                if (box != null)
                {
                    box.Execute(relatedBox);
                    
                    bumpedColor = box.GetColor();
                    if (bumpEffect != null)
                    {
                        bumpEffect.SetVector4("BumpedColor", bumpedColor);
                        bumpEffect.Play();
                    }
                    
                    relatedBox.OnBumpEffect();
                }
                //Debug.Log("WITH BLOCK COLLISION");
            }

            if (layer == LayerMask.NameToLayer("Knob"))
            {
                relatedScaler.ShouldExtrude = false;
                var box = other.GetComponentInParent<BoxBase>();
                
                if (box != null)
                {
                    box.Execute(relatedBox);
                    
                    bumpedColor = box.GetColor();
                    if (bumpEffect != null)
                    {
                        bumpEffect.SetVector4("BumpedColor", bumpedColor);
                        bumpEffect.Play();
                    }
                    
                    relatedBox.OnBumpEffect();
                }
                //Debug.Log("KNOB COLLISION");
            }

            if (layer == LayerMask.NameToLayer("Line"))
            {
                relatedScaler.ShouldExtrude = false;
                var box = other.GetComponentInParent<BoxBase>();
                
                bumpedColor = box.GetColor();
                if (bumpEffect != null)
                {
                    bumpEffect.SetVector4("BumpedColor", bumpedColor);
                    bumpEffect.Play();
                }
                
                relatedBox.OnBumpEffect();
                //Debug.Log("LINE COLLISION");
            }

            if (layer == LayerMask.NameToLayer("Wall"))
            {                
                relatedScaler.ShouldExtrude = false;
                //Debug.Log("WALL COLLISION");
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

        public Color GetColor()
        {
            return color;
        }
    }
}