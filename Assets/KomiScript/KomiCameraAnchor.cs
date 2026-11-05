using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomiCameraAnchor : MonoBehaviour
{
    [Header("オブジェクト設定")]
    [SerializeField]
    GameObject cameraAnchor;
    [SerializeField]
    GameObject cameraPos;
    [SerializeField]
    GameObject target;

    [Header("カメラ設定")]
    [SerializeField]
    KomiCameraMode cameraMode;
    [SerializeField, Tooltip("一人称視点のカメラ位置")]
    Vector3 FirstPersonOffset;
    [SerializeField, Tooltip("三人称視点のカメラ位置")]
    Vector3 ThirdPersonOffset;
    [SerializeField, Tooltip("自由視点のカメラ位置")]
    Vector3 FreePersonOffset;

    Vector3 anchorOffset;

    Vector3 axisUp;
    Vector3 axisRight;
    void Start()
    {
        SetCameraOffset();
        axisUp = GetYawWorldUp();
        axisRight = GetPitchWorldRight();
        anchorOffset = cameraAnchor.transform.position - target.transform.position;
    }


    void Update()
    {
        // F5キーが押されたらカメラモードを切り替える
        if (Input.GetKeyDown(KeyCode.F5))
        {
            switch (cameraMode)
            {
                case KomiCameraMode.FirstPerson:
                    SetCameraMode(KomiCameraMode.ThirdPerson);
                    SetCameraOffset();
                    SetCameraRotation();
                    break;
                case KomiCameraMode.ThirdPerson:
                    SetCameraMode(KomiCameraMode.FreePerson);
                    SetCameraOffset();
                    SetCameraRotation();
                    break;
                case KomiCameraMode.FreePerson:
                    SetCameraMode(KomiCameraMode.FirstPerson);
                    SetCameraOffset();
                    SetCameraRotation();
                    break;
            }
        }

        // マウスの移動入力を取得
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        axisUp = GetYawWorldUp();

        // Debug.DrawLine(cameraPos.transform.position, cameraPos.transform.position + cameraAnchor.transform.TransformDirection(axisUp), Color.red, 1f);
        // Y軸回転を計算
        Quaternion yawRotation = Quaternion.AngleAxis(mouseX, axisUp);
        // Y軸回転をカメラ位置に適用
        cameraPos.transform.localPosition = yawRotation * cameraPos.transform.localPosition;
        cameraPos.transform.localRotation = yawRotation * cameraPos.transform.localRotation;

        axisRight = GetPitchWorldRight();

        // Debug.DrawLine(cameraPos.transform.position, cameraPos.transform.position + cameraAnchor.transform.TransformDirection(axisRight), Color.blue, 1f);

        // X軸回転を計算
        Quaternion pitchRotation = Quaternion.AngleAxis(-mouseY, axisRight);
        // X軸回転をカメラ位置に適用
        cameraPos.transform.localPosition = pitchRotation * cameraPos.transform.localPosition;
        cameraPos.transform.localRotation = pitchRotation * cameraPos.transform.localRotation;

        // カメラアンカーをターゲットの位置に追従させる
        cameraAnchor.transform.position = target.transform.position + anchorOffset;

    }

    /// <summary>
    /// カメラ位置を基準としたY軸回転の回転軸を取得する
    /// </summary>
    /// <returns>Y軸回転の回転軸</returns>
    Vector3 GetYawWorldUp()
    {
        Vector3 yawWorldUp = cameraAnchor.transform.up;
        return yawWorldUp;
    }

    /// <summary>
    /// カメラ位置を基準としたX軸回転の回転軸を取得する
    /// </summary>
    /// <returns>X軸回転の回転軸</returns>
    Vector3 GetPitchWorldRight()
    {
        Vector3 pitchWorldRight = cameraPos.transform.right;
        return pitchWorldRight;
    }

    /// <summary>
    /// メインカメラの相対位置を設定する
    /// </summary>
    void SetCameraOffset()
    {
        switch (cameraMode)
        {
            // 一人称視点
            case KomiCameraMode.FirstPerson:
                cameraPos.transform.localPosition = FirstPersonOffset;
                break;
            // 三人称視点
            case KomiCameraMode.ThirdPerson:
                cameraPos.transform.localPosition = ThirdPersonOffset;
                break;
            // 自由視点
            case KomiCameraMode.FreePerson:
                cameraPos.transform.localPosition = FreePersonOffset;
                break;
        }
    }

    /// <summary>
    /// カメラの回転を設定する
    /// </summary>
    void SetCameraRotation()
    {
        switch (cameraMode)
        {
            case KomiCameraMode.FirstPerson:
                cameraPos.transform.localRotation = Quaternion.LookRotation(target.transform.forward);
                break;
            case KomiCameraMode.ThirdPerson:
                cameraPos.transform.localRotation = Quaternion.LookRotation(cameraAnchor.transform.position - cameraPos.transform.position);
                break;
            case KomiCameraMode.FreePerson:
            cameraPos.transform.localRotation = Quaternion.LookRotation(cameraAnchor.transform.position - cameraPos.transform.position);
                break;
        }
    }

    /// <summary>
    /// カメラモードを設定する
    /// </summary>
    /// <param name="newMode">設定するカメラモード</param>
    void SetCameraMode(KomiCameraMode newMode)
    {
        cameraMode = newMode;
    }


}

/// <summary>
/// カメラモードの列挙
/// </summary>
public enum KomiCameraMode
{
    /// <summary>
    /// 一人称視点
    /// </summary>
    FirstPerson,
    /// <summary>
    /// 三人称視点
    /// </summary>
    ThirdPerson,
    /// <summary>
    /// 自由視点
    /// </summary>
    FreePerson,
}