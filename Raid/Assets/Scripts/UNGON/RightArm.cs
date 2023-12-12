using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Unity.Netcode;

// ¿õ¾ð ¿ìÆÄ
public class RightArm : UNGONARM
{

    Vector2 originPos;
    Rigidbody2D _rigid;

    [SerializeField] private GameObject serverBullet;
    [SerializeField] private GameObject clientBullet;
    [SerializeField] Collider2D IgnoreCollider;

    private void Awake()
    {
        _rigid = GetComponentInParent<Rigidbody2D>();
        originPos = transform.position;
    }

    public override PlayerMovement GetNearPlayer()
    {
        List<PlayerMovement> arr = FindObjectsOfType<PlayerMovement>().ToList();
        float min = float.MaxValue;
        int idx = 0;
        for (int i = 0; i < arr.Count; i++)
        {
            float cur = Vector2.Distance(transform.position, arr[i].transform.position);
            if (cur < min)
            {
                min = cur;
                idx = i;
            }
        }
        return arr[idx];
    }

    public override IEnumerator Dash()
    {
        PlayerMovement player = GetNearPlayer();

        Vector2 dir = player.transform.position - transform.position;
        transform.up = dir;

        yield return new WaitForSeconds(1f);

        _rigid.velocity = Vector3.zero;
        _rigid.AddForce(dir.normalized * 50, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2f);

        ReturnPosition();

        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator ReturnAttack()
    {
        for (int i = 0; i < 3; i++)
        {

            PlayerMovement player = GetNearPlayer();

            Vector2 dir = player.transform.position - transform.position;
            transform.up = dir;

            yield return new WaitForSeconds(0.3f);

            transform.DOMove(player.transform.position, 0.5f).OnComplete(() =>
            {
                ReturnPosition();
            });
            yield return new WaitForSeconds(0.55f);
        }
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator ShootBullet()
    {
        PlayerMovement player = GetNearPlayer();
        YieldInstruction wait = new WaitForSeconds(0.3f);
        for (int i = 0; i < 5; i++)
        {
            transform.up = player.transform.position - transform.position;
            Shoot(player);
            yield return wait;
        }
    }

    private void Shoot(PlayerMovement player)
    {
        Vector3 pos = transform.position;
        Vector2 direction = (player.transform.position - transform.position);

        direction.Normalize();

        SpawnDummyKnife(pos, direction);
        //Å¬¶ó²¨ ½î°í
        SpawnKnifeServerRpc(pos, direction);

    }

    [ServerRpc]
    private void SpawnKnifeServerRpc(Vector3 pos, Vector3 dir)
    {
        var instance = Instantiate(serverBullet, pos, Quaternion.identity);
        instance.transform.right = dir;

        //Physics2D.IgnoreCollision(IgnoreCollider, instance.GetComponent<Collider2D>());

        if (instance.TryGetComponent<Rigidbody2D>(out Rigidbody2D _rigid))
        {
            _rigid.velocity = dir * 10;
        }

        if (instance.TryGetComponent<DealDamageOnContact>(out DealDamageOnContact contact))
        {
            contact.SetDamage(10);
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
        var instance = Instantiate(clientBullet, pos, Quaternion.identity);
        instance.transform.right = dir;
        //Physics2D.IgnoreCollision(IgnoreCollider, instance.GetComponent<Collider2D>());

        if (instance.TryGetComponent<Rigidbody2D>(out Rigidbody2D _rigid))
        {
            _rigid.velocity = dir * 10;
        }
    }

    public void ReturnPosition()
    {
        _rigid.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
        transform.DOMove(originPos, 0.5f);
    }
}
