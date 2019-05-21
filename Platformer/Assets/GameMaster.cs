using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    //put static so you wont have to do a 'find object of type Game Master'
    public static void KillPlayer(Player player) {
        Destroy(player.gameObject);
    }
}
