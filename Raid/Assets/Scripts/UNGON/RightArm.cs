using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RightArm : UNGONARM
{
    public override IEnumerator Dash()
    {
        yield return null;
        Debug.Log("dash");
    }

    public override IEnumerator Idle()
    {
        yield return null;
        Debug.Log("idle");
    }

    public override IEnumerator ReturnAttack()
    {
        yield return null;
        Debug.Log("return");
    }

    public override IEnumerator ShootBullet()
    {
        yield return null;
        Debug.Log("shoot");
    }
}
