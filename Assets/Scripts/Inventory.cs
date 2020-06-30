using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<KeyData> keys = new List<KeyData>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IPickUpable>() != null)
        {
            var pickUpable = collision.gameObject.GetComponent<IPickUpable>();

            if (pickUpable.IsPicked == true) return;

            pickUpable.PickUp(gameObject);
            pickUpable.IsPicked = true;
        }
    }

    public void AddKey(KeyData key)
    {
        keys.Add(key);
    }
}

public enum KEY_TYPE
{ 
  ROCK,
  SCISSORS,
  PAPER
}

