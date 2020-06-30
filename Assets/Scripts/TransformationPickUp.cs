using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationPickUp : MonoBehaviour, IPickUpable
{
    private bool isPicked = false;

    public bool IsPicked { get => isPicked; set => isPicked = value; }

    public void PickUp(GameObject pickUper)
    {
        pickUper.GetComponent<PlayerTransformator>().AddTransformation();
        Debug.Log("I PICK UP");
        Destroy(gameObject);
        //PlayAudio
    }
}
