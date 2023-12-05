using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ¿õ¾ð ÁÂÆÄ
public abstract class UNGONARM : MonoBehaviour
{
    public abstract IEnumerator Idle();
    public abstract IEnumerator Dash();
    public abstract IEnumerator ShootBullet();
    public abstract IEnumerator ReturnAttack();
}
