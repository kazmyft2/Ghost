#pragma warning disable 0414
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;//プレイヤーのアニメーション呼び出し
    public GameObject sword;//剣のコライダーを管理
    public GameObject attackEffectPrefab;
    public float runSpeed;//Runボタンを押している間の倍率
    public int jumpPower;//Jump力
    public AudioSource audioSource;
    public AudioClip[] se;

    Rigidbody rb;

    bool upNow = false;         //前進
    bool backNow = false;       //後退
    bool leftNow = false;       //左移動
    bool rightNow = false;      //右移動
    bool attackNow = false;     //攻撃
    bool runNow = false;        //走る
    bool jumpNow = false;       //ジャンプ
    bool walkSe = false;        //trueの時に足音を鳴らす

    public void UpPush() { upNow = true; Walk(true); }
    public void UpDown() { upNow = false; Walk(false); }

    public void ULPush() { upNow = true; leftNow = true; Walk(true); }
    public void ULDown() { upNow = false; leftNow = false; Walk(false); }

    public void URPush() { upNow = true; rightNow = true; Walk(true); }
    public void URDown() { upNow = false; rightNow = false; Walk(false); }

    public void BackPush() { backNow = true; Walk(true); }
    public void BackDown() { backNow = false; Walk(false); }

    public void BLPush() { backNow = true; leftNow = true; Walk(true); }
    public void BLDown() { backNow = false; leftNow = false; Walk(false); }

    public void BRPush() { backNow = true; rightNow = true; Walk(true); }
    public void BRDown() { backNow = false; rightNow = false; Walk(false); }

    public void LeftPush() { leftNow = true; Walk(true); }
    public void LeftDown() { leftNow = false; Walk(false); }

    public void RightPush() { rightNow = true; Walk(true); }
    public void RightDown() { rightNow = false; Walk(false); }

    public void AttackPush() { attackNow = true;  animator.SetBool("Attack", true); }
    public void AttackDown() { attackNow = false; animator.SetBool("Attack", false); }

    public void RunPush() { runNow = true; audioSource.PlayOneShot(se[2]); }
    public void RunDown() { runNow = false; audioSource.Stop(); }

    public void JumpPush() 
    {
        if(!jumpNow) jumpNow = true; animator.SetBool("Jump", true); //ジャンプ中は再度ジャンプしないために
    }

    void Walk(bool onOff)//前後左右歩いている時のアニメーションと足音のOnOff
    { 
        if (onOff) 
        {
            animator.SetBool("Walk", true);
            audioSource.PlayOneShot(se[1]);
            walkSe = true;
        }
        else if (!onOff) 
        {
            animator.SetBool("Walk", false);
            audioSource.Stop(); walkSe = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upNow) MoveToUp();
        if (backNow) MoveToBack();
        if (leftNow) MoveToLeft();
        if (rightNow) MoveToRight();
        if (runNow) Run();
        if (!runNow) NoRun();
        if (jumpNow) Jump();

        if (Input.GetKeyDown("w")) { upNow = true; Walk(true); }
        if (Input.GetKeyUp("w")) { upNow = false; Walk(false); }
        if (Input.GetKeyDown("s")) { backNow = true; Walk(true); }
        if (Input.GetKeyUp("s")) { backNow = false; Walk(false); }
        if (Input.GetKeyDown("a")) { leftNow = true; Walk(true); }
        if (Input.GetKeyUp("a")) { leftNow = false; Walk(false); }
        if (Input.GetKeyDown("d")) { rightNow = true; Walk(true); }
        if (Input.GetKeyUp("d")) { rightNow = false; Walk(false); }

        if (!audioSource.isPlaying)//足音のループ用
        {
            if (walkSe) audioSource.PlayOneShot(se[1]);//歩いていたら
            if (runNow) audioSource.PlayOneShot(se[2]);//走っていたら
        }
    }

    public void Stan()
    {
        animator.SetBool("Block", true);
        audioSource.PlayOneShot(se[4]);//ダメージ音再生
        StartCoroutine(StanInterval());//一瞬動けなくする
    }

    IEnumerator StanInterval()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Block", false);
    }

    public void Jump()
    {
        animator.SetBool("Jump", true);
        transform.Translate(0, 20f * Time.deltaTime, 0);
        StartCoroutine(JumpInterval());
    }

    IEnumerator JumpInterval()
    {
        yield return new WaitForSeconds(0.8f);
        jumpNow = false;
        animator.SetBool("Jump", false);
    }

    public void Run()
    {
        animator.SetBool("Run", true);
        runSpeed = 1.1f;
        if(!backNow){
            transform.Translate(0, 0, 12.0f * Time.deltaTime * runSpeed);
        }
    }

    public void NoRun()
    {
        animator.SetBool("Run", false);
        runSpeed = 1f;
    }

    public void MoveToLeft()
    {
        transform.Rotate(0, -70.0f * Time.deltaTime * runSpeed, 0);
        transform.Translate(0, 0, 1f * Time.deltaTime * runSpeed);
    }

    public void MoveToRight()
    {
        transform.Rotate(0, 70.0f * Time.deltaTime * runSpeed, 0);
        transform.Translate(0, 0, 1f * Time.deltaTime * runSpeed);
    }

    public void MoveToUp()
    {
        transform.Translate(0, 0, 12.0f * Time.deltaTime * runSpeed);
    }

    public void MoveToBack()
    {
        transform.Translate(0, 0, -9.0f * Time.deltaTime);
    }

    public void BeginSlash()
    {
        sword.GetComponent<SwordAttack>().AttackStart();
        upNow = false;
    }

    public void EndSlash()
    {
        sword.GetComponent<SwordAttack>().AttackEnd();
    }

    public void AttackSound()
    {
        audioSource.PlayOneShot(se[0]);
        AttackEffect();
    }

    public void JumpSound()
    {
        audioSource.PlayOneShot(se[3]);
    }

    void AttackEffect()
    {
        Vector3 tmp = GameObject.Find("MainPlayer").transform.position;

        Instantiate(
            attackEffectPrefab,
            new Vector3(tmp.x, tmp.y + 1.0f, tmp.z),
            Quaternion.identity);
    }
}