using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int EnemyHealthPoints = 100;

    private void Start()
    {
        
    }

    public void DealDamage(int AmountOfDamage)
    {
        EnemyHealthPoints -= AmountOfDamage;

        if (EnemyHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
        else
        {

        }
    }

    internal void TakeDamage(int pelletDamage)
    {
        throw new NotImplementedException();
    }
}
