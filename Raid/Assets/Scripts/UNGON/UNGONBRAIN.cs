using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// ¿õ¾ð ¼ö³úºÎ
public class UNGONBRAIN : NetworkBehaviour
{
    public UNGONARM[] arms = new UNGONARM[2];

    public override void OnNetworkSpawn()
    {
        StartCoroutine(Pattern(arms[0]));
        StartCoroutine(Pattern(arms[1]));
    }

    public override void OnNetworkDespawn()
    {
        StopAllCoroutines();
    }

    private IEnumerator Pattern(UNGONARM arm)
    {
        YieldInstruction s = new WaitForSeconds(1f);

        while (true)
        {
            yield return s;
            //arm.Idle();
            //arm.Dash();
            //arm.ShootBullet();
            //arm.ReturnAttack();

        }
    }
}