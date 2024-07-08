using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racer_Ctrl : MonoBehaviour
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
        if (!IsAttack) RacerMoveAndRotation();
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
        else if ((Input.GetKeyUp(KeyCode.LeftShift) || (h == 0 && v == 0)) && IsSprint)
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
    private void RacerMoveAndRotation()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        r = Input.GetAxisRaw("Mouse X");
        Vector3 moveDir = Vector3.right * h + Vector3.forward * v;
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        {
            animator.SetFloat("PosX", h, 0.01f, Time.deltaTime);
            animator.SetFloat("PosY", v, 0.01f, Time.deltaTime);
        }
        tr.Rotate(Vector3.up * r * turnSpeed * Time.deltaTime);
    }
}
