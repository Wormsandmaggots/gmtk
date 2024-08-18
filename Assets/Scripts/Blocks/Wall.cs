
using System;
using UnityEngine;

namespace Blocks
{
    public class Wall : BoxBase
    {
        private void Start()
        {
            id = 1;
        }

        public override void TryActivate(BoxBase touching)
        {
            Debug.Log("Bumped into a wall!");
        }
    }
}