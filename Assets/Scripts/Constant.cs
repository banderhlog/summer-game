using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constant {
    class BaffCard {
        static public int StrengthBonusHealthPoint = 50;//+хп максимум с захилом
        static public int StrengthBonusHeal        = 25;//захил сверху
    }
    class DurationEffect {//Можно писать только целое или половинчатое (7.5 например), малых значений времени стоит избегать наверно
        static public float    LowPoisonDuration = 8f;
        static public float MediumPoisonDuration = 8f;
        static public float   HardPoisonDuration = 12f;

        static public float    LowFireDuration = 5f;
        static public float MediumFireDuration = 10f;
        static public float   HardFireDuration = 15f;

        static public float    LowWaterDuration = 2f;
        static public float MediumWaterDuration = 3f;
        static public float   HardWaterDuration = 3f;

        static public float    LowStunDuration = 0.75f;
        static public float MediumStunDuration = 1.25f;
        static public float   HardStunDuration = 1.75f;
    }
    class DamageEffect {
        static public int    LowPoisonDamage = 2;
        static public int MediumPoisonDamage = 3;
        static public int   HardPoisonDamage = 3;

        static public int    LowFireDamage = 3;
        static public int MediumFireDamage = 4;
        static public int   HardFireDamage = 5;

        static public int    LowWaterDamage = 7 ;
        static public int MediumWaterDamage = 14;
        static public int   HardWaterDamage = 21;

        static public int    LowStunDamage = 20;
        static public int MediumStunDamage = 40;
        static public int   HardStunDamage = 60;

        static public int    LowBleedingDamage = 1;
        static public int MediumBleedingDamage = 1;
        static public int   HardBleedingDamage = 1;
    }
    class KatanaDamage {
        static public int RegularAttack = 12;
        static public int SharpAttack   = 22;
        static public int Swipe         = 35;
        static public int MSwipe        = 45;
        static public float AverageDPS() {
            float result;
            result = RegularAttack * (1f / KatanaCooldown.RegularAttackCooldown) + SharpAttack * (1f / KatanaCooldown.SharpAttackCooldown);
            //средний урон от оружия в секунду
            return result;
        }
    }
    class CooldownWeapon {

    }
    class DamageWeapon {

    }
    class KatanaCooldown {
        static public float RegularAttackCooldown = 1.2f;
        static public float SharpAttackCooldown   = 2f;
        static public float Swipe                 = 2.7f;
        static public float MSwipe                = 4f;
    }
    class WizardCasterSStaffOfMagicDamage {
        static public int NameAttack1 = 8;
        static public int NameAttack2 = 15;
        static public float AverageDPS() {
            float result;
            result = NameAttack1 * (1f / KatanaCooldown.RegularAttackCooldown) + NameAttack2 * (1f / KatanaCooldown.SharpAttackCooldown);
            //средний урон от оружия в секунду
            return result;
        }
    }
    class WizardCasterSStaffOfMagicCooldown {
        static public float RegularAttackCooldown = 0.2f;
        static public float SharpAttackCooldown   = 1f;
    }
}