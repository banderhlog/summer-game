using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
public class EnemyManager : MonoBehaviour {
    public  float         FearfulFactor;
    private GameObject    Player;
    private NavMeshAgent  NavMesh;
    private Health        HealthBot;
    private Move          MoveScript;
    private Vector3       Direction;
    private bool          FightStarted = false;
    private bool          AvoidReady   = true;
    private BehaviorModel BehaviorModelNow;
    private EnemyWeapon   Weapon;
    private GameObject   _PlayerWeapon;
    public enum BehaviorModel : int {
        Fearful = 0,
        Calm,
        Aggressive
    }
    private void Start() {
        Weapon         = GetComponent<EnemyWeapon>();
        NavMesh        = GetComponent<NavMeshAgent>();
        HealthBot      = GetComponent<Health>();
        MoveScript     = GetComponent<Move>();
        StartCoroutine(DefineBehavior());
    }
    private void Awake() {
        Player       = GameObject.FindGameObjectWithTag("Player");
       _PlayerWeapon = GameObject.Find("Player/HeroCamera/HeroKatana");
    }
    private void FixedUpdate() {
        if (FightStarted) {
            Direction = Player.transform.position - transform.position;
            MoveScript.RotationToward(Direction);
            switch ((int)BehaviorModelNow) {
                case (int)BehaviorModel.Fearful:
                    FearfulMode();
                    break;
                case (int)BehaviorModel.Calm://не используется пока что
                    CalmMode();
                    break;
                case (int)BehaviorModel.Aggressive:
                    AggressiveMode();
                    break;
                default:
                    print("Error in  EnemyManager");
                    break;
            }
        }
    }
    public void FearfulMode() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
            if (AvoidReady && (Direction.magnitude <= 5f)) {
                Weapon.Attack(15);
                MoveScript.AvoidAttack(Direction);
                AvoidReady = false;
                StartCoroutine(WaitCooldownAvoid(Random.Range(3f, 5f)));
            }
            if (Direction.magnitude <= 4f)
                Weapon.Attack(10);
        }
        if (Direction.magnitude <= 8f) 
              MoveScript.MovingToward(-Direction, 2f);
        if (Direction.magnitude > 8f) {
            Weapon.Attack(20);
            MoveScript.MovingToward(Direction);
        }
    }
    public void CalmMode() {
        MoveScript.RotationToward(Direction);
          MoveScript.MovingToward(Direction);
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && AvoidReady) {
            MoveScript.AvoidAttack(Direction);
            AvoidReady = false;
            StartCoroutine(WaitCooldownAvoid(Random.Range(6f, 10f)));
        }
    }
    public void AggressiveMode() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
            if (AvoidReady && (Direction.magnitude <= 5f)) {
                Weapon.Attack(12);
                MoveScript.AvoidAttack(-Direction);//напрыгнет на нас
                AvoidReady = false;
                StartCoroutine(WaitCooldownAvoid(Random.Range(6f, 10f)));
            }
        }
        if (Direction.magnitude <= 3.2f)
            Weapon.Attack(12);
        if (Direction.magnitude > 5.5f)
            MoveScript.MovingToward(Direction, 2f);
        else 
            MoveScript.MovingToward(Direction);
    }
    public void StartFight() {
        FightStarted = true;
    }
    private float[] Weight   = new float[5];
    private float[] Parametr = new float[5];
    private BehaviorModel DefineBehaviorModel() {
        float  result = 0f;
        Weight[0] = (float)1 / 5;//жизни игрока
        Weight[1] = (float)1 / 5;//на сколько игрок сильнее
        Weight[2] = (float)1 / 5;//жизни бота
        Weight[3] = (float)1 / 5;//на сколько игрок быстрее
        Weight[4] = (float)1 / 5;//пугливость бота
        DefineParametr0();
        DefineParametr1();
        DefineParametr2();
        DefineParametr3();
        Parametr[4] = FearfulFactor;
        for (int i = 0; i < 5; ++i)
            result += Weight[i] * Parametr[i];
        //if (0f    <= result && result < 0.33f) return BehaviorModel.Fearful;
        //if (0.33f <= result && result < 0.66f) return BehaviorModel.Calm;
        //if (0.66f <= result && result <= 1f)   return BehaviorModel.Aggressive;
        //print(result);
        if (0f   <= result && result < 0.4f) return BehaviorModel.Fearful;//временная затычка
        if (0.4f <= result && result <= 1f)  return BehaviorModel.Aggressive;
        return BehaviorModel.Aggressive;
        //return BehaviorModel.Calm;//Если вдруг никуда не попало, а такое маловероятно...
    }
    private void DefineParametr0() {
        Parametr[0] = 1f - Player.GetComponent<Health>().PercentOfLife();
    }
    private void DefineParametr1() {
        WeaponInterface.Weapon PlayerWeapon = _PlayerWeapon.GetComponent<WeaponInterface>().HeroWeapon;
        switch ((int)PlayerWeapon) {
            case (int)WeaponInterface.Weapon.FireKatana:
                Parametr[1] = Constant.KatanaDamage.AverageDPS();
                break;
            case (int)WeaponInterface.Weapon.WizardCasterSStaffOfMagic:
                Parametr[1] = Constant.WizardCasterSStaffOfMagicDamage.AverageDPS();
                break;
            default:
                print("Error in  DefineParametr1");
                break;
        }
        if ((float)3 / 2 * Parametr[1] <= Weapon.AverageDPS)//урон бота в 1.5 раза выше
            Parametr[1] = 1f;
        if ((float)3 / 2 * Parametr[1] > Weapon.AverageDPS && (float)2 / 3 * Parametr[1] > Weapon.AverageDPS)//урон примерно равен
            Parametr[1] = 0.5f;
        if ((float)2 / 3 * Parametr[1] <= Weapon.AverageDPS)//урона игрока в 1.5 раза выше
            Parametr[1] = 0f;
    }
    private void DefineParametr2() {
        Parametr[2] = HealthBot.PercentOfLife();
    }
    private void DefineParametr3() {
        //посчитать скорости (потом)
        Parametr[3] = 1f;
    }
    IEnumerator DefineBehavior() {
        while (true) {
            yield return new WaitForSeconds(2.5f);//обновляем модель поведения каждые 2.5 секунды
            BehaviorModelNow = DefineBehaviorModel();
        }
    }
    IEnumerator WaitCooldownAvoid(float CooldownTime) {
        yield return new WaitForSeconds(CooldownTime);
        AvoidReady = true;
    }
}