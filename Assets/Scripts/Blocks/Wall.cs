
using System;
using UnityEngine;

namespace Blocks
{
    public class Wall : BoxBase
    {
        protected override void Start()
        {
            id = 1;
        }

        protected override void TryActivate(BoxBase touching)
        {
            Debug.Log("Bumped into a wall!");
        }

        protected override void OnMouseDown()
        {
            
        }
    }
}