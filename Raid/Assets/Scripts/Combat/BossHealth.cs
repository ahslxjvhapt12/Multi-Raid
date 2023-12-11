using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BossHealth : NetworkBehaviour
{
    NetworkVariable<float> health = new NetworkVariable<float>();

    public override void OnNetworkSpawn()
    {
        health.Value = 5000;
    }

    public void TakeDamage(float damage)
    {
        health.Value -= damage;
    }
}
