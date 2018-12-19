using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBody : MonoBehaviour {

    private float health;
    protected float maxHealth;
    private float attack;
    protected bool invulnerable;

    protected float Attack
    {
        get {
            return attack;
        }

        set {
            attack = value;
        }
    }

    protected float Health
    {
        get {
            return health;
        }

        set {
            health = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    protected virtual void RecieveDamage(float inDmg) {
        Health = Health - inDmg;
    }

    protected virtual void DealDamage(CombatBody target) {
        target.RecieveDamage(Attack);
    }
}
