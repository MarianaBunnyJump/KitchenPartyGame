using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSo;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }
    
    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlayerSound(audioClipRefsSo.trash, trashCounter.transform.position, 1);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlayerSound(audioClipRefsSo.objectDrop, baseCounter.transform.position);
    }
    
    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlayerSound(audioClipRefsSo.objectPickup, Player.Instance.transform.position, 1);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlayerSound(audioClipRefsSo.chop, cuttingCounter.transform.position, 1);
    }
    
   
    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlayerSound(audioClipRefsSo.deliverySuccess, deliveryCounter.transform.position, 1);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlayerSound(audioClipRefsSo.deliveryFail, deliveryCounter.transform.position, 1);
    }
    
    private void PlayerSound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlayFootstepSound(Vector3 position, float volume = 1f)
    {
        PlayerSound(audioClipRefsSo.footstep, position, volume);
    }
    
}


























