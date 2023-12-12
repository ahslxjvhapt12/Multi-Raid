using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : NetworkBehaviour
{
    NetworkVariable<float> health = new NetworkVariable<float>();
    [SerializeField] Slider hpBar;

    public override void OnNetworkSpawn()
    {
        health.Value = 5000;
    }

    public void TakeDamage(float damage)
    {
        health.Value -= damage;
        hpBar.value = health.Value / 5000;
    }
}
