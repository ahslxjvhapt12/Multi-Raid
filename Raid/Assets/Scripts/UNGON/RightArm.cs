using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

// ¿õ¾ð ¿ìÆÄ
public class RightArm : UNGONARM
{

    Vector2 originPos;
    Rigidbody2D _rigid;

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
        PlayerMovement player = GetNearPlayer();

        Vector2 dir = player.transform.position - transform.position;
        transform.up = dir;

        yield return new WaitForSeconds(0.3f);

        transform.DOMove(player.transform.position, 0.5f).OnComplete(() =>
        {
            ReturnPosition();
        });
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator ShootBullet()
    {
        yield return null;
        Debug.Log("RightArmShoot");
    }

    public void ReturnPosition()
    {
        transform.rotation = Quaternion.identity;
        transform.DOMove(originPos, 0.2f);
    }
}
