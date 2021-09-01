using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{   [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int minHealth;
    [SerializeField]
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth < minHealth)
        {
            Destroy(this.gameObject);
        }
    }

}
