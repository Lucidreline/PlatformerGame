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

        
        public int damage = 40;

        public void Init() {
            curHealth = maxHealth;
        }

    }

    public EnemyStats Stats = new EnemyStats();

    public Transform deathParticles;
    [SerializeField] float fireEffectRate = .2f;
    [SerializeField] Transform firePrefab;
    bool onFire = false;
    public float shakeAmmount = 0.1f;
    public float shakeLength = 0.1f;


    [Header("Optional: ")]
    [SerializeField] private StatusIndicator statusIndicator;

    [SerializeField] float healthRatio;


    void Start() {
        Stats.Init();
        if(statusIndicator != null) {
            statusIndicator.setHealth(Stats.curHealth, Stats.maxHealth);
        }
        if(deathParticles == null) {
            Debug.LogError("No Death particles referenced in this enemy");
        }
    }

    void Update() {
        healthRatio = (float)Stats.curHealth / Stats.maxHealth;
        if(healthRatio <= .25 && !onFire) {
            StartCoroutine(Fire());
            onFire = true;
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
    void OnCollisionEnter2D(Collision2D colliderInfo) {
        Player _player = colliderInfo.collider.GetComponent<Player>();
        if(_player != null) {
            _player.DamagePlayer(Stats.damage);
            DamageEnemy(99999999);
            Debug.Log("found " + _player.name + " And did " + Stats.damage);
        }
    }
    IEnumerator Fire() {
        yield return new WaitForSeconds(fireEffectRate);
        Transform fireClone = Instantiate(firePrefab, transform.position, Quaternion.identity);
        Destroy(fireClone.gameObject, 2);
        StartCoroutine(Fire());
    }
}
