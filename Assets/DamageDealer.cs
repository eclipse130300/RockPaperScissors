using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<IDamagable>() != null)
        {
            var damagable = collision.gameObject.GetComponent<IDamagable>();
            if(damagable.isPlayer)
            {
                damagable.TakeDamage(damage);
            }
        }
    }
}
