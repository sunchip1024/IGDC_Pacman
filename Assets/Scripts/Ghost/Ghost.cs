using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    
    protected float chaseDistance;
    protected float attackDistance = 1;

    protected Transform playerTr;
    protected Transform ghostTr;

    protected void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ghostTr = GetComponent<Transform>();
    }

    protected void Act()
    {
        float distance = (playerTr.position - ghostTr.position).magnitude;

        if (distance <= attackDistance) Attack(playerTr.gameObject);
        if (distance <= chaseDistance)  Chase(playerTr.position);
        else                            Patrol();
    }

    /// <summary>
    /// Patrol Coroutine을 실행시키기 위한 함수
    /// </summary>
    protected abstract void Patrol();

    private void Chase(Vector3 target)
    {
        Vector3 movedir = target - ghostTr.position;
        ghostTr.Translate(movedir.normalized * Time.deltaTime * speed, Space.Self);
    }

    private void Attack(GameObject target)
    {
        target.GetComponent<Player>().getDamage();
    }
}
