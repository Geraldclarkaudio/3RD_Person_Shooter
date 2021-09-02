using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private Enemy enemyAI;

    void Start()
    {
        enemyAI = GetComponentInParent<Enemy>();

        if(enemyAI == null)
        {
            Debug.LogError("enemy AI is null");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyAI.StartAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyAI.StopAttack();
        }

    }
}
