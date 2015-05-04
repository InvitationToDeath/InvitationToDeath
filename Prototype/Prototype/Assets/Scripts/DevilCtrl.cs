using UnityEngine;
using System.Collections;

public class DevilCtrl : MonoBehaviour
{

    //혈흔 효과 프리팹.
    public GameObject bloodEffect;
    //악마 추격 속도.
    public float monsterSpeed = 3;
    //악마 생명 변수.
    public int devilHp = 100;
    //추적 사정거리.
    public float traceDist = 10.0f;
    //공격 사정거리.
    public float attackDist = 2.0f;
    //몬스터의 사망 여부.
    public bool isDie = false;


    //몬스터의 상태 정보가 있는 Enumerable 변수 선언.
    public enum MonsterState { idle, trace, attack, die, gothit };
    //몬스터의 현재 상태 정보를 저장할 Enum변수.
    public MonsterState monsterState = MonsterState.idle;

    //속도 향상을 위해 각종 컴포넌트를 변수에 할당.
    private Transform monsterTr;
    private Transform playerTr;

    //애니메이터 컴포넌트에 접근하기 위한 변수 선언.
    private Animator _animator;

    void Start()
    {
        //몬스터의 Transform 할당.
        monsterTr = this.gameObject.GetComponent<Transform>();
        //추적 대상인 Player의 Transform 할당.
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //Animator 컴포넌트 할당.
        _animator = this.gameObject.GetComponent<Animator>();

        //일정한 간격으로 몬스터의 행동 상태를 체크하는 코루틴 함수 실행.
        StartCoroutine(this.CheckMonsterState());
        //몬스터의 상태에 따라 동작하는 루틴을 실행하는 코루틴 함수 실행.
        StartCoroutine(this.MonsterAction());
    }
    void MoveUpdate()
    {
        transform.rotation = Quaternion.LookRotation(
    new Vector3(
    playerTr.position.x, monsterTr.position.y, playerTr.position.z)
                - transform.position);

        monsterTr.Translate(Vector3.forward * monsterSpeed * Time.deltaTime);

        if(monsterTr.position.y < playerTr.position.y)
            monsterTr.Translate(Vector3.up * monsterSpeed * Time.deltaTime);
        else if(playerTr.position.y < monsterTr.position.y)
            monsterTr.Translate(-Vector3.up * monsterSpeed * Time.deltaTime);
    }

    /*
     * 일정한 간격으로 몬스터의 할동 상태를 체크하고 monsterState값 변경.
     */

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);
            //몬스터의 플레이어 사이의 거리 측정.
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (dist <= attackDist) //공격 범위 이내로 들어왔는지 확인.
                monsterState = MonsterState.attack;
            else if (dist <= traceDist) //추적거리 범위 이내로 들어왔는지 확인.
                monsterState = MonsterState.trace;
            else //몬스터의 상태를 idle 모드로 설정.
                monsterState = MonsterState.idle;
        }
    }

    /*
     * 몬스터 상태값에 따라 적절한 동작을 수행하는 함수.
     */

    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            switch (monsterState)
            {
                //fly 상태.
                case MonsterState.idle:
                    //추적 중지.

                    //Animator의 IsAttack 변수를 false로 설정.
                    _animator.SetBool("IsAttack", false);

                    //Animator의 IsTrace 변수를 false로 설정.
                    _animator.SetBool("IsTrace", false);

                    break;

                //추적 상태.
                case MonsterState.trace:
                    //추적 시작.

                    //Animator의 IsAttack 변수를 false로 설정.
                    _animator.SetBool("IsAttack", false);

                    //Animator의 IsTrace 변수를 true로 설정.
                    _animator.SetBool("IsTrace", true);
                    MoveUpdate();

                    break;

                //공격 상태.
                case MonsterState.attack:
                    //추적 중지.

                    //IsAttack을 true로 설정해 attack State로 전이.
                    _animator.SetBool("IsAttack", true);

                    break;
            }

            yield return null;
        }
    }

    void OnTriggerEnter(Collider coll)
    {

        //충돌한 게임오브젝트의 태그값 비교.
        if (coll.collider.tag == "BULLET")
        {
            //혈흔 효과 코루틴 함수 호출.
            StartCoroutine(this.CreateBloodEffect(coll.transform.position));

            //맞은 총알의 Damage를 추출해 몬스터 hp 차감.
            devilHp -= coll.gameObject.GetComponent<BulletFire>().damage;
            if (devilHp <= 0)
            {
                MonsterDie();
            }

            //충돌한 게임 오브젝트 삭제.
            Destroy(coll.gameObject);

            //IsHit Trigger를 발생시키면 Any State에서 gothit로 전이됨.
            _animator.SetTrigger("IsHit");
        }
    }

    //몬스터 사망 시 처리 루틴.
    void MonsterDie()
    {
        //모든 코루틴을 정지시킨다.
        StopAllCoroutines();

        isDie = true;
        monsterState = MonsterState.die;

        _animator.SetTrigger("IsDie");

        //몬스터에 추가된 Collider를 비활성화.
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

        //일단 죽으면 사라지도록 처리.
        Destroy(gameObject);

        //데미지 입히는 양 팔의 콜라이더 비활성화 코드 같음.
        //foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        //{
        //    coll.enabled = false;
        //}

        //GameUI의 스코어 누적과 스코어 표시 함수 호출.
        //gameUI.DispScore(50);

        //몬스터 오브젝트 풀로 환원시키는 코루틴 함수 호출.
        //StartCoroutine(this.PushObjectPool());

    }

    IEnumerator CreateBloodEffect(Vector3 pos)
    {
        //혈흔 효과 발생.
        GameObject _blood1 = (GameObject)Instantiate(bloodEffect, pos, Quaternion.identity);
        Destroy(_blood1, 2.0f);

        yield return null;
    }
}
