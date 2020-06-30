using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUpable
{
    bool IsPicked { get; set; }

    void PickUp(GameObject pickUper);
}
