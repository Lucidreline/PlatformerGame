using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [System.Serializable]
    public class EnemyStats {
        public int Health = 100;
    }

    public EnemyStats Stats = new EnemyStats();

    public void DamageEnemy(int damage) {
        Stats.Health -= damage;
        if (Stats.Health <= 0) {
            GameMaster.KillEnemy(this);
        }
    }
}
