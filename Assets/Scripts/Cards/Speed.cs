using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour {
    static public void SpeedBaff() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
            Player.GetComponent<MovingRig>().BonusSpeed *= 1.5f;
        else print("Player cannot be found (Strength)");
    } 
}
