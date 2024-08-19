using System;
using UnityEngine;

namespace Blocks
{
    public class WinBlock : BoxBase
    {
        protected override void Start() 
        {
            id = 3;
        }

        public override void Execute(BoxBase previous)
        {
            Debug.Log("win");
        }
    }
}