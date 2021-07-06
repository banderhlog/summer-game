using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInterface : MonoBehaviour {
    public enum Weapon : int {
        FireKatana,
        WizardCasterSStaffOfMagic
    }
    public enum AttackMode : int {
        DefenseMode,
        AttackMode
    }
    public enum CooldownAttackName : int {
        FirstAttack = 0,
        MFirstAttack,
        SecondAttack,
        MSecondAttack,
        SpecialStrike1,
        SpecialStrike2,
        Ultimate,
        FirstAttack_MFirstAttack,
        MFirstAttack_MSecondAttack,
    }
    public  Weapon   HeroWeapon;//определяем тип оружия
    public  int      BonusAttackDamage = 1;//ВРЕМЕННАЯ ХРЕНЬ, ЕЕ НУЖНО БУДЕТ СПИЛИТЬ НАХУЙ БЛЯТЬ
    [SerializeField]
    private Collider WeaponCollider;//коллайдер для регистрации попадания по врагу
    private Animator WeaponAnimator;//ставим сюда аниматор произвольного оружия
    private int      AttackDamage;
    private bool     TheHeroAttack;
    private bool[]   CooldownAttackEnd = new bool[9];//9 типов атак, как в коде идут – такой порядок индексов
    private GameObject Pentagram;
    private void Start() {
        WeaponAnimator           = GetComponent<Animator>();
        WeaponCollider.isTrigger = true; //оружие физически не активно
        TheHeroAttack            = false;//герой не атакует
        Pentagram                = Resources.Load<GameObject>("PENTAG");
        for (int i = 0; i < 9; ++i)
            CooldownAttackEnd[i] = true;
    }
    private void Update() { //0 LKM, 1 PKM, 2 Wheel
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetMouseButtonDown(0) && CooldownAttackEnd[(int)CooldownAttackName.MFirstAttack]) {
                MFirstAttack(HeroWeapon, Constant.KatanaDamage.SharpAttack);
                CooldownAttackEnd[(int)CooldownAttackName.MFirstAttack] = false;
            }
            if (Input.GetMouseButtonDown(1) && CooldownAttackEnd[(int)CooldownAttackName.MSecondAttack]) {
                MSecondAttack(HeroWeapon, Constant.KatanaDamage.MSwipe);
                CooldownAttackEnd[(int)CooldownAttackName.MSecondAttack] = false;
            }
        }
        else {
            if (Input.GetMouseButtonDown(0) && CooldownAttackEnd[(int)CooldownAttackName.FirstAttack]) {
                FirstAttack(HeroWeapon, Constant.KatanaDamage.RegularAttack);
                CooldownAttackEnd[(int)CooldownAttackName.FirstAttack] = false;
            }
            if (Input.GetMouseButtonDown(1) && CooldownAttackEnd[(int)CooldownAttackName.SecondAttack]) {
                SecondAttack(HeroWeapon, Constant.KatanaDamage.Swipe);
                CooldownAttackEnd[(int)CooldownAttackName.SecondAttack] = false;
            }
        }
    }

    private void FirstAttack(Weapon TypeWeapon, int Damage) {
        TheHeroAttack = true;
        switch ((int)TypeWeapon) {
            case (int)Weapon.FireKatana:
                AttackDamage = Damage * BonusAttackDamage;
                WeaponAnimator.SetTrigger("RegAK");
                StartCoroutine(WaitCooldown(Constant.KatanaCooldown.RegularAttackCooldown, (int)CooldownAttackName.FirstAttack));
                break;
            default:
                print("Error in  First_Attack");
                break;
        }
        StartCoroutine(WaitEndAnimation());
    }
    private void MFirstAttack(Weapon TypeWeapon, int Damage) {
        TheHeroAttack = true;
        switch ((int)TypeWeapon) {
            case (int)Weapon.FireKatana:
                AttackDamage = Damage * BonusAttackDamage;
                WeaponAnimator.SetTrigger("SharpAK");
                GameObject.FindGameObjectWithTag("Player").GetComponent<MovingRig>().DashDirection(true);//+рывок вперед, true = мы вошли в дэш из анимации = дэш только вперед
                StartCoroutine(WaitCooldown(Constant.KatanaCooldown.SharpAttackCooldown, (int)CooldownAttackName.MFirstAttack));
                break;
            default:
                print("Error in  MFirstAttack");
                break;
        }
        StartCoroutine(WaitEndAnimation());
    }
    private void SecondAttack(Weapon TypeWeapon, int Damage) {
        TheHeroAttack = true;
        switch ((int)TypeWeapon) {
            case (int)Weapon.FireKatana:
                AttackDamage = Damage;
                WeaponAnimator.SetTrigger("Swipe");
                StartCoroutine(WaitCooldown(Constant.KatanaCooldown.Swipe, (int)CooldownAttackName.SecondAttack));
                break;
            default:
                print("Error in  SecondAttack");
                break;
        }
        StartCoroutine(WaitEndAnimation());
    }
    private void MSecondAttack(Weapon TypeWeapon, int Damage) {
        TheHeroAttack = true;
        switch ((int)TypeWeapon) {
            case (int)Weapon.FireKatana:
                AttackDamage = Damage;
                WeaponAnimator.SetTrigger("MSwipe");
                GameObject.FindGameObjectWithTag("Player").GetComponent<MovingRig>().DashDirection(true, false);//НИХУЯ НЕ ДЕЛАТЬ, УБРАТЬ ЭТОТ ДИКИЙ КРИНЖ
                RaycastHit hit; Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity);
                Vector3 RealPosition =       transform.position;
                        RealPosition.y = hit.transform.position.y;
                Destroy(Instantiate(Pentagram, RealPosition, Quaternion.identity), 10f);
                StartCoroutine(WaitCooldown(Constant.KatanaCooldown.MSwipe, (int)CooldownAttackName.MSecondAttack));
                break;
            default:
                print("Error in  MSecondAttack");
                break;
        }
        StartCoroutine(WaitEndAnimation());
    }
    private void SpecialStrike1(Weapon TypeWeapon, int Damage) {
        TheHeroAttack = true;
        StartCoroutine(WaitEndAnimation());
    }
    private void SpecialStrike2(Weapon TypeWeapon, int Damage) {
        TheHeroAttack = true;
        StartCoroutine(WaitEndAnimation());
    }
    private void Ultimate(Weapon TypeWeapon, int Damage) {
        TheHeroAttack = true;
        StartCoroutine(WaitEndAnimation());
    }
    private void FirstAttack_MFirstAttack(Weapon TypeWeapon, int Damage) {
        TheHeroAttack = true;
        StartCoroutine(WaitEndAnimation());
    }
    private void MFirstAttack_MSecondAttack(Weapon TypeWeapon, int Damage) {
        TheHeroAttack = true;
        StartCoroutine(WaitEndAnimation());
    }
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Enemy" && TheHeroAttack)
            collision.gameObject.GetComponent<Health>().DealDamage(AttackDamage);
    }
    float wait = 3f;
    //AnimatorStateInfo CurrentStateInfo;
    IEnumerator WaitEndAnimation() {
        //AnimatorClipInfo[] CurrentAnimation = WeaponAnimator.GetCurrentAnimatorClipInfo(0);
        //if(CurrentAnimation == null) Debug.Log("Not Anim!");
        //else
        //{
        //AnimationClip clip = CurrentAnimation[0].clip;
        //wait = clip.length / WeaponAnimator.GetCurrentAnimatorStateInfo(0).speed;
        //}
        yield return new WaitForSeconds(wait);
        //Debug.Log("LENGTH=" + wait);
        TheHeroAttack = false;
    }
    IEnumerator WaitCooldown(float Cooldown, int index) {
        yield return new WaitForSeconds(Cooldown);
        CooldownAttackEnd[index] = true;
    }
}