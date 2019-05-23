using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [System.Serializable]
    public class EnemyStats {
        public int maxHealth = 100;
        private int _curHealth;


        public int curHealth {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init() {
            curHealth = maxHealth;
        }

    }

    public EnemyStats Stats = new EnemyStats();
    [Header("Optional: ")]
    [SerializeField] private StatusIndicator statusIndicator;

    void Start() {
        Stats.Init();
        if(statusIndicator != null) {
            statusIndicator.setHealth(Stats.curHealth, Stats.maxHealth);
        }
    }

    

    public void DamageEnemy(int damage) {
        Stats.curHealth -= damage;
        if (Stats.curHealth <= 0) {
            GameMaster.KillEnemy(this);
        }
        if (statusIndicator != null) {
            statusIndicator.setHealth(Stats.curHealth, Stats.maxHealth);
        }
    }
}
