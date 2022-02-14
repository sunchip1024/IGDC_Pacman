using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : MonoBehaviour
{
    [SerializeField]
    protected int speed;

    private Transform playerTr;
    private Transform ghostTr;

    public void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ghostTr = GetComponent<Transform>();
    }

    public void Update()
    {
        ChaseTarget(playerTr.position); 
    }

    private void ChaseTarget(Vector3 target)
    {
        Vector3 movedir = target - ghostTr.position;
        ghostTr.Translate(movedir.normalized * Time.deltaTime * speed, Space.Self);
    }
}
