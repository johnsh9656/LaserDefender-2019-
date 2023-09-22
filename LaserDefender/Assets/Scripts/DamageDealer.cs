using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 1;
    [SerializeField] bool indestructable = false;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (!indestructable)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }

}