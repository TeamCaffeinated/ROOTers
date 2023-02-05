using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEndComponent : MonoBehaviour
{
    List<PlayerControllerComponent> registeredPlayers;

    public void registerPlayer(PlayerControllerComponent player) {
        registeredPlayers.Add(player);
    }

    public void onRoundEnd() {
        var routerToPlayers = new DefaultDictionary<RouterComponent, List<PlayerControllerComponent>>();  
        foreach (var player in registeredPlayers) {
            RouterComponent currentRounter = player.getCurrentRouter();
            routerToPlayers[currentRounter].Add(player);
        }

        foreach (KeyValuePair<RouterComponent, List<PlayerControllerComponent>> entry in routerToPlayers) {
            float sumHealthAndBonus = entry.Key.getBonus();
            foreach(PlayerControllerComponent player in entry.Value) {
                sumHealthAndBonus += player.health;
            }

            float newHealth = sumHealthAndBonus / entry.Value.Count;
            foreach(PlayerControllerComponent player in entry.Value) {
                player.health = newHealth;
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
