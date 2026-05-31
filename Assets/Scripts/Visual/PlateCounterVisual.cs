using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlateCounterVisual : MonoBehaviour
    {
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private Transform plateVisualPrefab;
        [SerializeField] private PlateCounter plateCounter;

        private List<GameObject> plateVisualGoList = new List<GameObject>();

        private void Start()
        {
            plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
            plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
        }
        
        private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
        {
            var plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
            
            float plateOffsetY = .1f;
            plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGoList.Count, 0);
            
            plateVisualGoList.Add(plateVisualTransform.gameObject);
        }
        
        private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e)
        {
            GameObject plateGameObject = plateVisualGoList[plateVisualGoList.Count - 1];
            plateVisualGoList.Remove(plateGameObject);
            Destroy(plateGameObject);
        }
    }
}