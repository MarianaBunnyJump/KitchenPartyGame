using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform IconTemplate;

    private void Awake()
    {
        IconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += plateKitchenObject_OnIngredientAdded;
    }

    private void plateKitchenObject_OnIngredientAdded(object sender,PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if(child == IconTemplate) continue;
            Destroy(child.gameObject);
        }
        
        foreach (var kitchenObjectSo in plateKitchenObject.GetKitchenObjectSoList())
        {
            var icon =Instantiate(IconTemplate, transform);
            icon.gameObject.SetActive(true);
            icon.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSo);
        }
    }
    
}
