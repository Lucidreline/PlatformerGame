using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    [SerializeField] GameObject      respawnPanel;
    [SerializeField] TextMeshProUGUI countdownText;

    void Awake() {
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    [SerializeField] Transform playerPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] Transform spawnPrefab;
    [SerializeField] int       spawnDelay = 3;
    int countdownNum;

    

    public IEnumerator RespawnPlayer() {
        StartCoroutine(spawnCountDown());
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform cloneParticles = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(cloneParticles.gameObject, 10f); 
    }

    IEnumerator spawnCountDown() {
        respawnPanel.SetActive(true);
        for(countdownNum = spawnDelay; countdownNum > 0; countdownNum--) {
            countdownText.text = countdownNum.ToString();
            yield return new WaitForSeconds(1);
        }
        respawnPanel.SetActive(false); 
    }

    //put static so you wont have to do a 'find object of type Game Master'
    public static void KillPlayer(Player player) {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer()); //not exactly sure why i needed a 'gm.'
    }

    public static void KillEnemy(Enemy enemy) {
        Destroy(enemy.gameObject);
    }
}
