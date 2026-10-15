using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockholder : MonoBehaviour
{
    public GameObject holdpoint = null;
    public GameObject holditem = null;
    public float scale;
    public ItemBlock itemBlock;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (holditem != null)
        {
            itemBlock = holditem.GetComponent<ItemBlock>();
            holditem.transform.localScale = itemBlock.startscale * scale;
            holditem.transform.position = holdpoint.transform.position;
            holditem.transform.rotation = holdpoint.transform.rotation;
        }
        if (holditem == null)
        {
            itemBlock = null;
        }
    }
}
