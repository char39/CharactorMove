using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player : MonoBehaviour
{
    [Header("상하좌우 이동")]
    public float moveSpeed = 5f;    // 이동 속도 변수 선언
    public float runSpeed = 10f;    // 달리기 속도 변수 선언
    public float turnSpeed = 90f;   // 회전 속도 변수 선언
    [SerializeField]                    // private 변수를 인스펙터 창에 노출시키기 위해 선언
    private Transform tr;           // Transform 컴포넌트를 저장할 변수 선언
    [SerializeField]
    private float h = 0f, v = 0f;   // 키 입력값을 저장할 멤버 필드 변수 선언
    [Header("Mouse Rotation")]
    public float xSensitivity = 700f;   // 마우스 X축 회전속도 감도 변수 선언
    public float ySensitivity = 700f;   // 마우스 Y축 회전속도 감도 변수 선언
    public float yMinLimit = -45f;      // 캐릭터 X축 회전 최소각도 변수 선언
    public float yMaxLimit = 45f;       // 캐릭터 X축 회전 최대각도 변수 선언
    public float xMinLimit = -360f;     // 캐릭터 Y축 회전 최소각도 변수 선언
    public float xMaxLimit = 360f;      // 캐릭터 Y축 회전 최대각도 변수 선언
    [SerializeField]
    private float yRot = 0f, xRot = 0f;    // 마우스 회전값을 저장할 변수 선언
    [SerializeField]
    private float jumpForce = 5.5f;
    private bool isJump = false;
    

    void Start()
    {
        tr = GetComponent<Transform>();  // Transform 컴포넌트를 가져와 FPStr에 저장
    }

    void Update()
    {
        PlayerMove();
        PlayerRotation();
        PlayerJump();
    }

    private void PlayerRotation()
    {
        xRot += Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        yRot += Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, yMinLimit, yMaxLimit); // 시야 위아래 움직이는 각도 제한(캐릭터는 x축으로 회전, 마우스는 y축으로 이동)
        tr.eulerAngles = new Vector3(-yRot, xRot, 0f);   // eulerAngle : 회전값을 저장하는 변수
    }
    private void PlayerMove()
    {
        h = Input.GetAxis("Horizontal");    // 키보드의 좌우 키 입력값을 받아와 변수 h에 저장
                                            // A키를 누르면 -1, D키를 누르면 1, 아무 키도 누르지 않으면 0
        v = Input.GetAxis("Vertical");      // 키보드의 상하 키 입력값을 받아와 변수 v에 저장
                                            // W키를 누르면 1, S키를 누르면 -1, 아무 키도 누르지 않으면 0
        Vector3 Normalized = h * Vector3.right.normalized + v * Vector3.forward.normalized;
                                            // 정규화 벡터를 이용하여 어느방향이든 이동 속도를 일정하게 만듦
        if (Input.GetKey(KeyCode.LeftShift))                            // Shift키를 누르면
            tr.Translate(Normalized * runSpeed * Time.deltaTime);       // 달리기 속도로 이동
        else
            tr.Translate(Normalized * moveSpeed * Time.deltaTime);
        
    }
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        isJump = false;
    }
}
