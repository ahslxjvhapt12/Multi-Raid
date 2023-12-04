using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimation : NetworkBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    private readonly int XHash = Animator.StringToHash("X");
    private readonly int YHash = Animator.StringToHash("Y");

    private NetworkVariable<float> x;
    private NetworkVariable<float> y;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        x = new NetworkVariable<float>();
        y = new NetworkVariable<float>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            x.OnValueChanged += HandleXChanged;
            y.OnValueChanged += HandleYChanged;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner)
        {
            x.OnValueChanged -= HandleXChanged;
            y.OnValueChanged -= HandleYChanged;
        }
    }
    
    private void HandleXChanged(float previousValue, float newValue)
    {
        animator.SetFloat(XHash, newValue);
    }

    private void HandleYChanged(float previousValue, float newValue)
    {
        animator.SetFloat(YHash, newValue);
    }
}
