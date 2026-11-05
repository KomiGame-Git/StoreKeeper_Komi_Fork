using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Jumppower = 400;
    public float Movespeed = 10;
    public float ShiftMovespeed;
    [SerializeField]
    private bool Grounded;
    private Rigidbody rb;
    public float MousemovepowerX = 15f;
    public float MousemovepowerY = 15f;
    public float MinimumX = -360f;
    public float MaximumX = 360f;
    public float MinimumY = -60f;
    public float MaximumY = 60f;
    float RotationX = 0f;
    float RotationY = 0;
    public GameObject VerRot;
    public GameObject HotRot;
    public float gravity;
    private Animator animator;
    public bool cameramodeFP = true;
    public GameObject TPcamera;
    public GameObject FPcamera;
    public GameObject playerRender;
    public GameMenu gameMenu;
    public GameObject nowcamera;
    public PlayerEnventory playerEnventory;
    public Vector3 respownpoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        FPcamera.SetActive(true);
        TPcamera.SetActive(false);
        nowcamera = FPcamera;
        playerRender.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward * Movespeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += -transform.forward * Movespeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += -transform.right * Movespeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right * Movespeed;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            playerEnventory.Throwitem();
        }
        if (Grounded == true && Input.GetKey(KeyCode.Space))
        {
            Grounded = false;
            //Debug.Log("grounded=false,jump");
            rb.velocity = new Vector3(rb.velocity.y, Jumppower, rb.velocity.z);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            CameramodeChange();
        }

        RotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MousemovepowerX;

        RotationY += Input.GetAxis("Mouse Y") * MousemovepowerY;
        RotationY = Mathf.Clamp(RotationY, MinimumY, MaximumY);

        if (!gameMenu.isGamemenunow)
        {
            if (cameramodeFP == true)
            {
                FPcamera.transform.localEulerAngles = new Vector3(-RotationY, 0, 0);
                HotRot.transform.localEulerAngles = new Vector3(0, RotationX, 0);
                Vector3 normalizedDirection = moveDirection.normalized;
                rb.velocity = new Vector3(normalizedDirection.x * Movespeed, rb.velocity.y, normalizedDirection.z * Movespeed);
            }
            else
            {
                VerRot.transform.localEulerAngles = new Vector3(-RotationY, 0, 0);
                HotRot.transform.localEulerAngles = new Vector3(0, RotationX, 0);
                Vector3 normalizedDirection = moveDirection.normalized;
                rb.velocity = new Vector3(normalizedDirection.x * Movespeed, rb.velocity.y, normalizedDirection.z * Movespeed);
            }
        }
        if (transform.position.y <= -15)
        {
            transform.position = respownpoint;
        }
    }
    void FixedUpdate()
    {
        if (rb.velocity.y < 30)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * gravity * Time.deltaTime;
        }
    }
    void CameramodeChange()
    {
        cameramodeFP = !cameramodeFP;
        if (cameramodeFP == true)
        {
            FPcamera.SetActive(true);
            TPcamera.SetActive(false);
            playerRender.GetComponent<Renderer>().enabled = false;
            nowcamera = FPcamera;
        }
        else
        {
            FPcamera.SetActive(false);
            TPcamera.SetActive(true);
            playerRender.GetComponent<Renderer>().enabled = true;
            nowcamera = TPcamera;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        float hity = contact.point.y;
        if ((collision.gameObject.tag == ("block") ||collision.gameObject.tag==("Machine"))&& hity > transform.position.y)
        {
            Grounded = true;
            //Debug.Log("grounded=true,block");
        }
        /*if (collision.gameObject.tag == "Ground")
        {
            Grounded = true;
        }*/
    }
    void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "Ground")
        {
            Grounded = true;
            Debug.Log("grounded=true,terrain");
        }*/
    }
    void OnCollisionExit(Collision collision)
    {
        //Debug.Log("collisionexit");
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "block"||collision.gameObject.tag==("Machine"))
        {
            Grounded = false;
            //Debug.Log("grounded=false");
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground"||collision.gameObject.tag==("Machine")||collision.gameObject.tag==("block"))
        {
            Grounded = true;
        }
    }
}
