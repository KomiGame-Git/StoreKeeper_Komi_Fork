using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public GameObject input;
    public GameObject output;
    public GameObject box;
    public Stocker stocker;
    public Blockholder blockholder;
    public bool iscoroutine = false;
    public ItemType type;
    public bool nowmoveing = false;
    public GameObject lookpoint;
    public Gridmanager gridmanager;
    public List<Collider> colliderlist;

    // Start is called before the first frame update
    void Start()
    {
        colliderlist.Clear();
        stocker = input.GetComponent<Stocker>();
        blockholder = output.GetComponent<Blockholder>();
        colliderlist.Add(this.gameObject.transform.GetComponent<Collider>());
        colliderlist.Add(input.transform.GetComponent<Collider>());
        colliderlist.Add(output.transform.GetComponent<Collider>());
        Debug.Log(colliderlist.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (nowmoveing)
        {
            transform.position = gridmanager.gridposition(lookpoint.transform.position);
        }
        else if (stocker.stockitem > 0 && !iscoroutine)
        {
            iscoroutine = true;
            StartCoroutine(makeitem());
        }
    }
    IEnumerator makeitem()
    {

        for (; stocker.stockitem > 0; stocker.stockitem--)
        {
            yield return new WaitForSeconds(1f);
            if (blockholder.holditem == null)
            {
                blockholder.holditem = Instantiate(box);
                blockholder.itemBlock = blockholder.holditem.transform.GetComponent<ItemBlock>();
                blockholder.itemBlock.type = type;
                blockholder.itemBlock.stockitem = 0;
                blockholder.itemBlock.isholding = true;
                blockholder.itemBlock.holdingobject = output;
                //blockholder.holdpoint=output.
            }
            blockholder.itemBlock.stockitem++;
            //Debug.Log("makeitem");
        }
        iscoroutine = false;
    }
    /*public void setting()
    {
        nowmoveing=false;
    }*/
    public void colliderchenge(bool chenge)
    {
        Debug.Log(colliderlist.Count);
        for (int i = 0; i < colliderlist.Count; i++)
        {
            Debug.Log(i);
            colliderlist[i].enabled = chenge;
        }
    }
}
