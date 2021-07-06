﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UniqueEffect : MonoBehaviour {
    /*
     Депрессия
Скорость передвижения и наносимый урон падают на 20%, получаемый урон возрастает на 40%. Можно развеять, иначе спадает спустя 3 комнаты (любые).

Режим бога
Об этой награде будет сказано в начале паркур-комнаты! Открывается, если пройти комнату с ловушками (паркур лвл) без получения урона. Увеличивает наносимый урон на 40%, скорость передвижения на 20%, дает невосприимчивость к отрицательным эффектам, дает 1 ед регенерации жизни в секунду, открывает божественный стиль трезубца, финальный босс заменяется на “нашу копию”.

Кровь из золота
Каждая подобранная монета восстанавливает жизни 1:1
Каждые 30 заработанных монет увеличивают максимум жизни на 15
Каждые 50 заработанных монет увеличивают наносимый урон на 5%
Каждое выпитый эликсир золота восстанавливает 30% жизни, увеличивает сопротивление эффектам на 2%, дает временное ускорение (по всем параметрам, на раунд против врагов) на 2*кол-во выпитых эликсиров золота%
При этом игрок замедляется навсегда на (кол-во выпитых эликсиров золота)%, если на нем нем эффекта с ускорением
Каждый 2-й выпитый эликсир снимает действие всех эффектов

Смертельная болезнь
Если игрок не будет убивать врагов в течении 4-х минут, то он потеряет 50% максимального здоровья, после этого эффект возобновиться, но на убийство будет даваться 2 минуты, если игрок не успеет, то он умрет. Не действует на боссах (отсчет замораживается). Эффект можно развеять зельем очищения.

Перерождение
Если игрок умирает, то он теряет все полученные эффекты, снимаются все отрицательные эффекты, кроме яда и огня, игрок “появляется” на месте смерти с полным здоровьем и тем же оружием, оттолкнув от себя всех противников на 2 своих роста. Сопровождается эффектом ударной волны света

     */
    public enum Effect : int {
        Depression = 0,
        GodMode,
        BloodOfGold,
        DeadlyDisease,
        Rebirth
    }
    public Effect BaffEffectName;
    private UnityEvent Baff = new UnityEvent();
    private UnityAction BaffAction;
    private bool Cooldown = false;
    private float CooldownTimer;
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    public void StrenghtenEffect()
    {
        
    }
    private void OnTriggerEnter(Collider Creature)
    {
        if (!Cooldown)
        {
            Cooldown = true;
            StrenghtenEffect();
            Baff.Invoke();
            StartCoroutine(WaitCooldownAvoid(CooldownTimer));
        }
    }
    IEnumerator WaitCooldownAvoid(float CooldownTime)
    {
        yield return new WaitForSeconds(CooldownTime);
        Cooldown = false;
    }
}
