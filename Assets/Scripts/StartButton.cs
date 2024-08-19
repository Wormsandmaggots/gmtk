using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StartButton : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Debug.Log("START");
            BlockResolver.instance.ResolveRound();
        }

        private void OnMouseOver()
        {
            
        }
    }
}