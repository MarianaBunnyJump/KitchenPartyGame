using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] private GameObject particleSystem;
        [SerializeField] private GameObject stoveOnGameObject;
        [SerializeField] private StoveCounter stoveCounter;

        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        }

        private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
        {
            bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
            stoveOnGameObject.SetActive(showVisual);
            particleSystem.SetActive(showVisual);
        }
    }
}