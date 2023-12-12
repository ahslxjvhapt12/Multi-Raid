using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// ¿õ¾ð ¼ö³úºÎ
public class UNGONBRAIN : NetworkBehaviour
{
    public UNGONARM[] arms = new UNGONARM[2];
    Rigidbody2D _rigid;

    public override void OnNetworkSpawn()
    {
        _rigid = GetComponent<Rigidbody2D>();
        //StartCoroutine(Pattern(arms[0]));
        StartCoroutine(Pattern(arms[1]));
    }

    public override void OnNetworkDespawn()
    {
        StopAllCoroutines();
    }

    private IEnumerator Pattern(UNGONARM arm)
    {
        YieldInstruction s = new WaitForSeconds(0.7f);

        while (true)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    Debug.Log(1);
                    yield return StartCoroutine(arm.Dash());
                    break;
                case 1:
                    Debug.Log(2);
                    yield return StartCoroutine(arm.ReturnAttack());
                    break;
                case 2:
                    Debug.Log(3);
                    yield return StartCoroutine(arm.ShootBullet());
                    break;
            }
            yield return s;
        }
    }
}