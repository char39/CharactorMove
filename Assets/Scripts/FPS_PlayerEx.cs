using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_PlayerEx : MonoBehaviour
{
    #region Vars
    [Header("상하좌우 이동")]
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float turnSpeed = 90f;
    [SerializeField]
    private Transform tr;
    [SerializeField]
    private float h = 0f, v = 0f;
    [Header("Mouse Rotation")]
    public float xSensitivity = 700f;
    public float ySensitivity = 700f;
    public float yMinLimit = -45f;
    public float yMaxLimit = 45f;
    public float xMinLimit = -360f;
    public float xMaxLimit = 360f;
    [SerializeField]
    private float xRot = 0f, yRot = 0f;
    private float jumpForce = 5.0f;
    private bool isJump = false;
    #endregion
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        PlayerMove();
        PlayerRotation();
        PlayerJump();
    }

    private void PlayerMove()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 Normalized = (h * Vector3.right + v * Vector3.forward).normalized;
        
        if (Input.GetKey(KeyCode.LeftShift))
            tr.Translate(Normalized * runSpeed * Time.deltaTime);
        else
            tr.Translate(Normalized * moveSpeed * Time.deltaTime);
    }
    private void PlayerRotation()
    {
        xRot += Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        yRot += Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, yMinLimit, yMaxLimit);
        tr.eulerAngles = new Vector3(yRot, xRot, 0f);
    }
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        isJump = false;
    }
}
