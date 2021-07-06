using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour {
    public GameObject[] Bots;
    private void OnTriggerEnter(Collider Creature) {
        if (Creature.gameObject.tag == "Player") {
            for (int i = 0; i < Bots.Length; ++i)
                Bots[i].GetComponent<EnemyManager>().StartFight();
            Destroy(this.gameObject);
        }

    }
}
