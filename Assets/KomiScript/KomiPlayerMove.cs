using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アタッチされているGameObjectにRigidbodyを必須とする
[RequireComponent(typeof(Rigidbody))]
public class KomiPlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    [Header("プレイヤー設定")]
    [SerializeField]
    private float moveSpeed = 5f;

    [Header("詳細設定")]
    [SerializeField, Tooltip("回転の補間時間")]
    private float rotationLerpTime = 0.1f;
    [SerializeField, Tooltip("カメラを向く際の補間時間")]
    private float lookAtCameraLerpTime = 5f;


    [Header("カメラ参照")]
    [SerializeField]
    private KomiCameraAnchor komiCameraAnchor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // キー入力を取得
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }
        moveDirection.Normalize();

        // cameraModeに応じてプレイヤーの向きを調整
        if (komiCameraAnchor.cameraMode == KomiCameraMode.FirstPerson)
        {
            Vector3 lookDirection = new Vector3(komiCameraAnchor.cameraPos.transform.forward.x, 0, komiCameraAnchor.cameraPos.transform.forward.z).normalized;

            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
        else if (komiCameraAnchor.cameraMode == KomiCameraMode.ThirdPerson)
        {
            Vector3 lookDirection = new Vector3(komiCameraAnchor.cameraPos.transform.forward.x, 0, komiCameraAnchor.cameraPos.transform.forward.z).normalized;
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }





        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    void LookAtCameraFoward(float lerpTime)
    {
        Transform cameraTransform = komiCameraAnchor.cameraPos.transform;

        Vector3 lookDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * lerpTime);
    }
}
