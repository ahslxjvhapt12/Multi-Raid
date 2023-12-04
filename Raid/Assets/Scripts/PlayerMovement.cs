using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] GameObject serverShuriken;
    [SerializeField] GameObject clientShuriken;

    [Header("Settings")]
    [SerializeField] private float _movementSpeed = 4f; //이동속도 
    [SerializeField] private float _throwCooltime;
    [SerializeField] private float throwSpeed = 10f;
    [SerializeField] private float damage = 10f;

    private float _lastThrowTime;
    private Vector2 movementInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //여기는 과제로 제시
        if (!IsOwner) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        if (Input.GetMouseButtonDown(0))
        {
            if (_lastThrowTime + _throwCooltime > Time.time) return;
            _lastThrowTime = Time.time;


            Vector3 pos = transform.position;
            Vector3 direction = Vector2.right;

            SpawnDummyKnife(pos, direction);
            //클라꺼 쏘고
            SpawnKnifeServerRpc(pos, direction);
        }

        movementInput = new Vector2(h, v).normalized;
    }

    [ServerRpc]
    private void SpawnKnifeServerRpc(Vector3 pos, Vector3 dir)
    {
        var instance = Instantiate(serverShuriken, pos, Quaternion.identity);
        instance.transform.right = dir;

        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), instance.GetComponent<Collider2D>());

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
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), instance.GetComponent<Collider2D>());

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
}
