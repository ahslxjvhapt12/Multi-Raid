using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{

    NetworkVariable<float> health = new NetworkVariable<float>();

    public override void OnNetworkSpawn()
    {
        health.Value = 100;
    }

    public void TakeDamage(float damage)
    {
        health.Value -= damage;
    }
}
