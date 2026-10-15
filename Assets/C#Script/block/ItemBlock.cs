using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBlock : MonoBehaviour
{
    public GameObject holdingobject = null;
    public bool isholding = false;
    public Vector3 startscale;
    public ItemType type;
    public int stockitem = 0;
    public TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        startscale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (this.gameObject.transform.position.y <= -15)
        {
            Destroy(this.gameObject);
        }
        if (type == ItemType.food)
        {

            renderer.material.color = Color.blue;
        }
        else
            if (type == ItemType.nofood)
            {
                renderer.material.color = Color.red;
            }
            else
                if (type == ItemType.none)
                {
                    renderer.material.color = Color.white;
                }
        if (stockitem == 0)
        {
            text.text = "空";
            type = ItemType.none;
        }
        else
        {
            text.text = stockitem.ToString();
        }

    }
    void Playercarry()
    {

    }
}
