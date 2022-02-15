using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int speed = 5;

    private Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized * Time.deltaTime * speed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Ghost":
                Debug.LogFormat("GameOver");
                this.speed = 0;
                break;

            case "Coin":
                GameManager.gameManager.RaiseScore(1);
                break;
        }
        
        other.gameObject.SetActive(false);
    }
}
