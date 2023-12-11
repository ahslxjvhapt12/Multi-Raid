using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

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

        Vector2 dir = transform.position - player.transform.position;
        _rigid.AddForce(dir.normalized * 100, ForceMode2D.Force);
        ReturnPosition();
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator ReturnAttack()
    {
        PlayerMovement player = GetNearPlayer();

        transform.DOMove(player.transform.position, Vector2.Distance(transform.position, player.transform.position) * 0.4f);
        ReturnPosition();
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator ShootBullet()
    {
        yield return null;
        Debug.Log("LeftArmshoot");
    }

    public void ReturnPosition()
    {
        transform.DOMove(originPos, Vector2.Distance(transform.position, originPos) * 0.4f);
    }
}
