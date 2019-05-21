using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats {
        public int Health = 100;
    }

    public PlayerStats playerStats = new PlayerStats();

    [SerializeField] int fallBoundry = -20;

    void Update() {
        if(transform.position.y <= fallBoundry) {
            DamagePlayer(999999999);
        }
    }

    public void DamagePlayer(int damage) {
        playerStats.Health -= damage;
        if(playerStats.Health <= 0) {
            GameMaster.KillPlayer(this);
        }
    }
    
}
