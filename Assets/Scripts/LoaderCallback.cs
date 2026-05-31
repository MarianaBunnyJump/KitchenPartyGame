using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class LoaderCallback : MonoBehaviour
    {
        private bool isFirstUpdate = true;

        private void Update()
        {
            if (isFirstUpdate)
                isFirstUpdate = false;
            
            Loader.LoaderCallBack();
        }
    }
}