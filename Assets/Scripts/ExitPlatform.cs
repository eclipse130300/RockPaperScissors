using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPlatform : MonoBehaviour
{
    public List<KeyData> keysReqired;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Inventory>())
        {
            var inventory = collision.gameObject.GetComponent<Inventory>();

            foreach (KeyData key in keysReqired)
            {
                if (!inventory.keys.Contains(key))
                {
                    Debug.Log("CANNOT FIND KEY...break");
                    return;
                }
            }
            //Go to next level


            var UIcontroller = FindObjectOfType<UIGameController>();
            UIcontroller.FinishGame();
            Debug.Log("GO TO NEXT LEVEL");
        }
    }
}
