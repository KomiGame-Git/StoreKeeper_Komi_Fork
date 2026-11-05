using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomiMouseCursor : MonoBehaviour
{
    [SerializeField]
    bool isCursorLock = true;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = isCursorLock ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isCursorLock;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLock = !isCursorLock;
            Cursor.lockState = isCursorLock ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isCursorLock;
        }
    }
}
