using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnContactBoss : MonoBehaviour
{
    float _damage = 0;

    public void SetDamage(float knifeDamage)
    {
        _damage = knifeDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"{collision.gameObject.name} is hit");

        if (collision.attachedRigidbody == null) return;

        if (collision.attachedRigidbody.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            health.TakeDamage(_damage);
        }
    }
}
