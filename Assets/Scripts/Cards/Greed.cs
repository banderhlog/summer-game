using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greed : MonoBehaviour{
    static public void GreedBaff() {
        GameObject Player = GameObject.Find("HeroKatana");
        if (Player != null)
            Player.GetComponent<WeaponInterface>().BonusAttackDamage *= 2;
        else print("Player cannot be found (Greed)");
    }
}
