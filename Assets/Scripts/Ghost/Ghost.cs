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

    protected Rigidbody ghostRigid;
    protected CommandHandler handler;

    private void Awake()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ghostTr = GetComponent<Transform>();

        ghostRigid = GetComponent<Rigidbody>();
        handler = new CommandHandler(ghostRigid);
    }

    private void FixedUpdate()
    {
        Act();
    }

    protected void Act()
    {
        if (GameManager.IsGameEnd()) return;

        float distance = (playerTr.position - ghostTr.position).magnitude;

        if (distance <= attackDistance)     Attack(playerTr.gameObject);
        else if (distance <= chaseDistance) Chase(playerTr.position);
        else                                Patrol();
    }

    private void Patrol()
    {
        handler.Act();
    }

    private void Chase(Vector3 target)
    {
        Vector3 movedir = target - ghostTr.position;
        ghostTr.Translate(movedir.normalized * Time.deltaTime * speed, Space.Self);
    }

    private void Attack(GameObject target)
    {
        target.GetComponent<Player>().getDamage();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("WALL")) return;
        if (handler.GetNowCommand() != Command.COMMAND.FORWARD) return;
        handler.Next();
        Debug.LogFormat("[ {0} ] OnTriggerStay!", gameObject.name);
    }
}
