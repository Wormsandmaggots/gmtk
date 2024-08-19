using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StartButton : MonoBehaviour
    {
        private void OnMouseDown()
        {
            BlockResolver.instance.ResolveRound();
        }

        private void OnMouseOver()
        {
            
        }
    }
}