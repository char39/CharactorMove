using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 움직이기 위해 transform 컴포넌트를 추가
// 2. Animator 컴포넌트를 추가
// 3. 이동 속도와 회전 속도, 키보드 입력값을 받을 변수 선언
public class J_Ctrl : MonoBehaviour
{
    [SerializeField] private Transform tr;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private float turnSpeed = 150.0f;
    float h = 0.0f, v = 0.0f, r = 0.0f;
    bool IsSprint = false;
    bool IsAttack = false;


    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (!IsAttack) J_MoveAndRotation();
        if (!IsAttack) Sprint();
        if (!IsAttack) Jump();
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && h == 0 && v == 0 && !IsAttack)
        {
            animator.SetTrigger("IdleAttackTrigger");
            IsAttack = true;
            Invoke("AttackEnd", 1.9f);
            // if (animator.GetCurrentAnimatorStateInfo(0).IsName("Standing Melee Attack Downward"))
            // {
            //     if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.9f)
            //     AttackEnd();
            // }
        }
    }
    private void AttackEnd()
    {
        IsAttack = false;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && h == 0 && v == 0)
        {
            animator.SetTrigger("IdleJumpTrigger");
        }
        if (Input.GetKeyDown(KeyCode.Space) && v > 0.1f)
        {
            animator.SetTrigger("RunningJumpTrigger");
        }
    }
    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && !IsSprint)
        {
            moveSpeed = 14.0f;
            animator.SetBool("IsSprint", true);
            IsSprint = true;
        }
        else if ((Input.GetKeyUp(KeyCode.LeftShift) || (h == 0 && v == 0))&& IsSprint)
        {
            moveSpeed = 7.0f;
            animator.SetBool("IsSprint", false);
            IsSprint = false;
        }
        else if (v <= 0f && IsSprint)
        {
            moveSpeed = 7.0f;
            animator.SetBool("IsSprint", false);
            IsSprint = false;
        }
    }
    private void J_MoveAndRotation()
    {
        h = Input.GetAxisRaw("Horizontal"); // GetAxisRaw는 -1, 0, 1 값만 반환
        v = Input.GetAxisRaw("Vertical");   // GetAxis는    -1 ~ 1 값 사이 반환
        r = Input.GetAxisRaw("Mouse X");
        #region (이동 관련)정규화와 최적화가 안된 코드
        // tr.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);
        // {
        //     animator.SetFloat("PosX", h, 0.01f, Time.deltaTime);
        // }
        // tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        // {
        //     animator.SetFloat("PosY", v, 0.01f, Time.deltaTime);
        // }
        #endregion
        #region (이동 관련)정규화와 최적화가 된 코드
        Vector3 moveDir = Vector3.right * h + Vector3.forward * v;
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        {
            animator.SetFloat("PosX", h, 0.01f, Time.deltaTime);
            animator.SetFloat("PosY", v, 0.01f, Time.deltaTime);
        }
        #endregion
        tr.Rotate(Vector3.up * r * turnSpeed * Time.deltaTime);
    }
}
