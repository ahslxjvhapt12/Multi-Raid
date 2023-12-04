using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnContact : MonoBehaviour
{
    float _damage = 0;

    public void SetDamage(float knifeDamage)
    {
        _damage = knifeDamage;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} is hit");

        if (collision.attachedRigidbody == null) return;

        if (collision.attachedRigidbody.TryGetComponent<BossHealth>(out BossHealth bossHealth))
        {
            bossHealth.TakeDamage(_damage);
        }
    }
}
