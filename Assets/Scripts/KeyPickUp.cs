using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour, IPickUpable
{
    public KeyData key;
    private UIGameController gameController;
    private bool isPicked;

    public bool IsPicked { get => isPicked; set => isPicked = value; }
    //TODO implement?

    private void Awake()
    {
        gameController = FindObjectOfType<UIGameController>();
    }

    public void PickUp(GameObject pickUper)
    {
        pickUper.GetComponent<Inventory>().AddKey(key);
        gameController.KeyPicked(key);
        Destroy(gameObject);
        //PlayAudio
    }
}
