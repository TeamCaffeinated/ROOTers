using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEndComponent : MonoBehaviour
{
    // TODO better use PlayerMoveIN and PlayerMoveOUT method in router
    // each router keeps a list with its own player, we'll might need anyway it for visualization

    List<PlayerControllerComponent> registeredPlayers = new List<PlayerControllerComponent>();

    public void registerPlayer(PlayerControllerComponent player) {
        registeredPlayers.Add(player);
    }
    public void removePlayer(PlayerControllerComponent player) {
        registeredPlayers.Remove(player);
    }

    public void onRoundEnd() {
        var routerToPlayers = new DefaultDictionary<RouterComponent, List<PlayerControllerComponent>>();  
        foreach (var player in registeredPlayers) {
            RouterComponent currentRounter = player.getCurrentRouter();
            routerToPlayers[currentRounter].Add(player);
        }

        Debug.Log("Round end");

        foreach (KeyValuePair<RouterComponent, List<PlayerControllerComponent>> entry in routerToPlayers) {
            float sumHealthAndBonus = entry.Key.getBonus();
            foreach(PlayerControllerComponent player in entry.Value) {
                sumHealthAndBonus += player.health;
                Debug.Log(player.name + " entered " + entry.Key.name + " with health " + player.health);
            }

            float newHealth = sumHealthAndBonus / entry.Value.Count;
            
            Debug.Log(newHealth);
            
            foreach(PlayerControllerComponent player in entry.Value) {
                player.health = newHealth;
                Debug.Log(player.name + " will exit " + entry.Key.name + " with health " + player.health);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
