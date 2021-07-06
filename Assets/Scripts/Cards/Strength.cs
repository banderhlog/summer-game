using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Strength : MonoBehaviour {
    static public void StrengthBaff() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject GameUI = GameObject.Find("GameUI/HealthBar");
        if (Player != null)  {
            var Health    = Player.GetComponent<Health>();
            var HealthBar = GameUI.GetComponent<GameUI>();
            Health.AddHealthPoint(Constant.BaffCard.StrengthBonusHealthPoint);//увеличили максимальное хп и полечили
            Health.Heal(          Constant.BaffCard.StrengthBonusHeal);       //и сверху еще захилили
            HealthBar.ChangeMaxHealth(Health.HealthPoint);//поменяли соотношение в хп UI
            HealthBar.ChangeSizes();//отрисовали новое хп
        }
        else print("Player cannot be found (Strength)");
    }
}
