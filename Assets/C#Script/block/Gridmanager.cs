using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridmanager : MonoBehaviour
{
    public float gridsize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 gridposition(Vector3 nowposition)
    {
        return new Vector3(Mathf.FloorToInt(nowposition.x/gridsize),0,Mathf.FloorToInt(nowposition.z/gridsize));
    }
}
