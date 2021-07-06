using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    [SerializeField]
    private Image HealthBarEdge;
    [SerializeField]
    private Image HealthBarDefault;
    [SerializeField]
    private GameObject[] EffectsObject;//Зависит от порядка объектов canvas!!!
    /*  
    public enum Effect : int {
        Fire,
        Poison,
        Water } 
    Порядок картинок и цветов идет в соответствии с этой структурой
    */
    [SerializeField]
    private Sprite[] EffectsImage;
    [SerializeField]
    private  Color[] EffectsColor;
    private   bool[] EffectsWorks;
    private  float[] EffectsDuration;

    private Health HPManager;
    private Image  HealthBar;
    private int    MaxHealth;
    private int    OldMaxHealth;
    private int    CountBaff;
    private Color  Hide;
    void Start() {
        HPManager    = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        MaxHealth    = HPManager.HealthPoint;
        OldMaxHealth = MaxHealth;
        HealthBar    = GetComponent<Image>();
        CountBaff    = 0;//индекс ячейки, которая пуска и куда можно "нарисовать" эффект
        Hide         = new Color(0, 0, 0, 0);
        EffectsWorks    = new  bool[EffectsObject.Length];
        EffectsDuration = new float[EffectsObject.Length];
        for (int i = 0; i < EffectsWorks.Length; ++i)
            EffectsWorks[i] = false;//на нас нет ни 1 эффекта
    }
    private void Update() {
        HealthBar.fillAmount = (float)HPManager.HealthPoint / MaxHealth;
    }
    public void ChangeMaxHealth(int NewMaxHealth) {
        OldMaxHealth = MaxHealth;
        MaxHealth    = NewMaxHealth;
    }
    public void ChangeSize(float SizeMultiplying, Image IM) {//необходимо ставить пивот в (0,0)!!! Программно надо бы проставить такое
        Vector3 Change = IM.rectTransform.transform.localScale;
        if (SizeMultiplying >= 1f)
             Change.x *= SizeMultiplying;
        else Change.x *= (1f + SizeMultiplying);
        if (Change.x >= 17f) Change.x = 17f;
        IM.rectTransform.transform.localScale = Change;
    }   
    public void ChangeSizes() {
        float SizeMultiplying = (float)MaxHealth/OldMaxHealth;
        ChangeSize(SizeMultiplying, HealthBarEdge);//лишняя черная полоса справа
        ChangeSize(SizeMultiplying, HealthBarDefault);
        ChangeSize(SizeMultiplying, HealthBar);
    }
    public void AddEffect(ActiveEffect.Effect NewEffect, float DurationEffect) {
        if (EffectsWorks[(int)NewEffect])//если эффект уже на герое, обновить его
            EffectsDuration[(int)NewEffect] = DurationEffect;
        else {//Если эффект новый
            EffectsDuration[CountBaff] = DurationEffect;
            EffectsObject[CountBaff].GetComponent<Image>().sprite = EffectsImage[(int)NewEffect];//поставить на новое место картинку эффекта
            EffectsObject[CountBaff].GetComponent<Image>().color  = EffectsColor[(int)NewEffect];//и цвет
             EffectsWorks[(int)NewEffect] = true;//теперь эффект работает
            StartCoroutine(DeleteEffect(CountBaff, (int)NewEffect));//ставим на таймер его снятие в соответствующем слоте
            CountBaff++;//обновить индекс самого правого эффекта (+1)
        }
    }
    IEnumerator DeleteEffect(int index, int EffectName) {//index отвечает за ячейку, из которой бафф надо убрать, а EffectName за индекс наложенного эффекта
        while (EffectsWorks[EffectName]) {
            EffectsDuration[index] -= Time.deltaTime;//снимаем время, прошедшнее с последнего фрейма
            if (EffectsDuration[index] <= 0) {//если действие эффекта закончилось
                for (int i = index; i < CountBaff - 1; ++i) {
                    EffectsObject[i].GetComponent<Image>().sprite = EffectsObject[i + 1].GetComponent<Image>().sprite;
                    EffectsObject[i].GetComponent<Image>().color  = EffectsObject[i + 1].GetComponent<Image>().color;
                }
                EffectsObject[CountBaff - 1].GetComponent<Image>().color = Hide;//последний эффект спрятать
                EffectsWorks[EffectName] = false;//эффект закончил работу
                CountBaff--;//обновить текущий последний эффект
            }
            yield return new WaitForEndOfFrame();
        }
    }
}