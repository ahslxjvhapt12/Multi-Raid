using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] GameObject serverShuriken;
    [SerializeField] GameObject clientShuriken;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] TextMeshPro _ammoTxt;

    [Header("Settings")]
    [SerializeField] private float _movementSpeed = 4f; //이동속도 
    [SerializeField] private float _throwCooltime;
    [SerializeField] private float _reloadCooltime;
    [SerializeField] private float throwSpeed = 10f;
    [SerializeField] private float damage = 10f;

    [SerializeField] private int maxAmmo;
    [SerializeField] private int curAmmo;
    private float _lastThrowTime;
    private bool _isReloading = false;
    private Vector2 movementInput;

    Camera _mainCam;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _mainCam = Camera.main;
        _ammoTxt.text = $"Ammo : {curAmmo}/{maxAmmo}";
    }

    private void Update()
    {
        //여기는 과제로 제시
        if (!IsOwner) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (!_isReloading)
        {
            if (Input.GetMouseButton(0))
            {
                if (_lastThrowTime + _throwCooltime > Time.time) return;
                curAmmo--;
                _ammoTxt.text = $"Ammo : {curAmmo}/{maxAmmo}";

                _lastThrowTime = Time.time;

                Vector3 pos = transform.position;
                Vector2 direction = (_mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position);

                direction.Normalize();

                SpawnDummyKnife(pos, direction);
                //클라꺼 쏘고
                SpawnKnifeServerRpc(pos, direction);

                if (curAmmo <= 0)
                {
                    _isReloading = true;
                    StartCoroutine(Reload());
                }
            }
        }

        movementInput = new Vector2(h, v).normalized;
    }

    [ServerRpc]
    private void SpawnKnifeServerRpc(Vector3 pos, Vector3 dir)
    {
        var instance = Instantiate(serverShuriken, pos, Quaternion.identity);
        instance.transform.right = dir;

        Physics2D.IgnoreCollision(playerCollider, instance.GetComponent<Collider2D>());

        if (instance.TryGetComponent<Rigidbody2D>(out Rigidbody2D _rigid))
        {
            _rigid.velocity = dir * throwSpeed;
        }

        if (instance.TryGetComponent<DealDamageOnContact>(out DealDamageOnContact contact))
        {
            contact.SetDamage(damage);
        }

        SpawnDummyClientRPC(pos, dir);
    }

    [ClientRpc]
    private void SpawnDummyClientRPC(Vector3 pos, Vector3 dir)
    {
        if (IsOwner) return;

        SpawnDummyKnife(pos, dir);
    }

    private void SpawnDummyKnife(Vector3 pos, Vector3 dir)
    {
        var instance = Instantiate(clientShuriken, pos, Quaternion.identity);
        instance.transform.right = dir;
        Physics2D.IgnoreCollision(playerCollider, instance.GetComponent<Collider2D>());

        if (instance.TryGetComponent<Rigidbody2D>(out Rigidbody2D _rigid))
        {
            _rigid.velocity = dir * throwSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return; //오너가 아니면 리턴

        _rigidbody.velocity = movementInput * _movementSpeed;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadCooltime);
        curAmmo = maxAmmo;
        _ammoTxt.text = $"Ammo : {curAmmo}/{maxAmmo}";
        _isReloading = false;
    }
}
