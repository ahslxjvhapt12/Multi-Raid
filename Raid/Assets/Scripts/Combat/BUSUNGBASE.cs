using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUSUNGBASE : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 4;
    private float _currentLifeTime;

    private void Update()
    {
        _currentLifeTime += Time.deltaTime;
        if (_currentLifeTime > _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
