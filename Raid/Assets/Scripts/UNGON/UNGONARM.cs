using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class UNGONARM : NetworkBehaviour
{
    public abstract PlayerMovement GetNearPlayer();
    public abstract IEnumerator Dash();
    public abstract IEnumerator ShootBullet();
    public abstract IEnumerator ReturnAttack();
}
