using System;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

namespace DefaultNamespace
{
    public class SelectCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter baseCounter;
        [SerializeField] private List<GameObject> visualGameObjectArray;

        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
        {
            //传入的等于自己
            if (baseCounter == e.selectedCounterOne)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            foreach (var item in visualGameObjectArray)
            {
                item.SetActive(true);
            }
        }

        private void Hide()
        {
            foreach (var item in visualGameObjectArray)
            {
                item.SetActive(false);
            }
        }
    }
}