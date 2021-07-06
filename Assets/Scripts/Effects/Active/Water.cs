using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
    static public void LowWater() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject GameUI = GameObject.Find("GameUI/HealthBar");
        if (Player != null) {
            var Health        = Player.GetComponent<Health>();
            var EffectPrinter = GameUI.GetComponent<GameUI>();
            Health.PeriodicDamage(Constant.DurationEffect.LowWaterDuration, Constant.DamageEffect.LowWaterDamage);
            EffectPrinter.AddEffect(ActiveEffect.Effect.Water, Constant.DurationEffect.LowWaterDuration);
        }
        else print("Player cannot be found (LowWater)");
    }
    static public void MediumWater() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject GameUI = GameObject.Find("GameUI/HealthBar");
        if (Player != null) {
            var Health        = Player.GetComponent<Health>();
            var EffectPrinter = GameUI.GetComponent<GameUI>();
            Health.PeriodicDamage(Constant.DurationEffect.MediumWaterDuration, Constant.DamageEffect.MediumWaterDamage);
            EffectPrinter.AddEffect(ActiveEffect.Effect.Water, Constant.DurationEffect.MediumWaterDuration);
        }
        else print("Player cannot be found (MediumWater)");
    }
    static public void HardWater() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject GameUI = GameObject.Find("GameUI/HealthBar");
        if (Player != null) {
            var Health        = Player.GetComponent<Health>();
            var EffectPrinter = GameUI.GetComponent<GameUI>();
            Health.PeriodicDamage(Constant.DurationEffect.HardWaterDuration, Constant.DamageEffect.HardWaterDamage);
            EffectPrinter.AddEffect(ActiveEffect.Effect.Water, Constant.DurationEffect.HardWaterDuration);
        }
        else print("Player cannot be found (HardWater)");
    }
}
