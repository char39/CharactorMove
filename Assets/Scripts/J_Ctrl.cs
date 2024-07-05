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


    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal"); // GetAxisRaw는 -1, 0, 1 값만 반환
        v = Input.GetAxis("Vertical");   // GetAxis는    -1 ~ 1 값 사이 반환
        r = Input.GetAxisRaw("Mouse X");
        tr.Translate(Vector3.right * h * moveSpeed * Time.deltaTime);
        {
            animator.SetFloat("PosX", h, 0.01f, Time.deltaTime);
        }
        tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        {
            animator.SetFloat("PosY", v, 0.01f, Time.deltaTime);
        }
        tr.Rotate(Vector3.up * r * turnSpeed * Time.deltaTime);
        

    }
}
