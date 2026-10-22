using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public PlayerMove playerMove;
    public float Raycastlength;
    public PlayerEnventory playerEnventory;
    public bool iscoroutine = false;
    public GameObject machineobject;
    public GameObject lookpoint;
    public Gridmanager gridmanager;
    public bool nowmoveing;
    private bool leftcheckcoroutine=false;
    public GameObject movemachine;
    void Start()
    {

    }

    void Update()
    {
        Debug.DrawRay(playerMove.nowcamera.transform.position, playerMove.nowcamera.transform.forward * Raycastlength, Color.red);
        if (nowmoveing)
        {
            if (!leftcheckcoroutine)
            {
                StartCoroutine(leftclickchecker(movemachine.transform.GetComponent<Machine>()));
            }
        }
        else if (Physics.Raycast(playerMove.nowcamera.transform.position, playerMove.nowcamera.transform.forward, out RaycastHit hit, Raycastlength))
        {
            if (hit.transform.tag == "block" && Input.GetMouseButtonUp(1) && playerEnventory.item == null)//ブロックをとる
            {
                ItemBlock itemBlock = hit.collider.GetComponent<ItemBlock>();

                if (itemBlock.isholding && itemBlock.holdingobject != null)
                {
                    Blockholder blockholder = itemBlock.holdingobject.GetComponent<Blockholder>();
                    playerEnventory.Getitem(blockholder.holditem);
                    blockholder.holditem = null;
                    Debug.Log(blockholder.holditem);
                    itemBlock.holdingobject = null;
                    itemBlock.isholding = false;
                    //Debug.Log(playerEnventory.item.layer);
                    playerEnventory.item.layer = 2;
                }
                else
                {
                    playerEnventory.Getitem(hit.transform.gameObject);
                    itemBlock.holdingobject = null;
                    itemBlock.isholding = false;
                    //Debug.Log(playerEnventory.item.layer);
                    playerEnventory.item.layer = 2;
                }

            }
            else if (hit.transform.tag == "blockholder" && Input.GetMouseButtonUp(0) && playerEnventory.item != null)//ブロックを置く
            {
                Blockholder blockholder = hit.transform.GetComponent<Blockholder>();
                if (blockholder.holditem == null)
                {
                    blockholder.holditem = playerEnventory.item;
                    ItemBlock itemBlock = playerEnventory.item.GetComponent<ItemBlock>();
                    itemBlock.holdingobject = hit.transform.gameObject;
                    itemBlock.isholding = true;
                    playerEnventory.item.layer = 0;
                    playerEnventory.item = null;
                }
            }
            else if (hit.transform.tag == "stocker" && Input.GetMouseButton(0) && !iscoroutine)//ストッカーに入れる
            {
                iscoroutine = true;
                StartCoroutine(itemsupply(hit.transform.gameObject));
            }
            else if (hit.transform.tag == "Machine" && Input.GetMouseButton(0) && Input.GetMouseButton(1) && !nowmoveing)
            {
                nowmoveing = true;
                machineobject = hit.transform.parent.gameObject;
                movemachine = Instantiate(machineobject);
                Machine machinescript = movemachine.transform.GetComponent<Machine>();
                machinescript.nowmoveing = true;
                machinescript.lookpoint = lookpoint;
                machinescript.gridmanager = gridmanager;
            }

        }
    }
    IEnumerator itemsupply(GameObject hit)
    {
        Stocker stocker = hit.transform.GetComponent<Stocker>();
        ItemBlock itemBlock = playerEnventory.item.GetComponent<ItemBlock>();
        //Debug.Log("coroutine");
        //Debug.Log(!Input.GetMouseButton(0));
        //Debug.Log(itemBlock.stockitem <= 0);
        for (; itemBlock.stockitem >= 1 && Input.GetMouseButton(0); itemBlock.stockitem--)
        {
            //Debug.Log("for");
            yield return null;
            stocker.stockitem++;
        }
        yield return null;
        iscoroutine = false;
    }
    IEnumerator leftclickchecker(Machine machine)
    {
        leftcheckcoroutine=true;
        yield return new WaitWhile(()=>Input.GetMouseButton(0));
        //machine.setting();
        yield return new WaitUntil(()=>Input.GetMouseButtonDown(0));
        Debug.Log(Input.GetMouseButton(0));
        nowmoveing=false;
        machine.nowmoveing=false;
        machine=null;
        movemachine=null;
        machineobject=null;
        leftcheckcoroutine=false;
    }
}
