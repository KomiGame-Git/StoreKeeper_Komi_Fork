using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アタッチされているGameObjectにRigidbodyを必須とする
[RequireComponent(typeof(Rigidbody))]
public class KomiPlayer : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
