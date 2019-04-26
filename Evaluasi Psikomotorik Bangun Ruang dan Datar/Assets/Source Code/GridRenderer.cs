using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    public GameObject palette;
    public GameObject prefab;

  
    // Start is called before the first frame update
    void Start()
    {
        NodeRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NodeRenderer()
    {
        Vector3 myPos = palette.transform.position - new Vector3(0.3f,0.18f,0.18f);
        for (int i = 0; i < 19; i++)
        {
            for(int y = 0; y<14; y++)
            {
                
                myPos = myPos + new Vector3(0.04f, 0f, 0f);
                GameObject myGO = Instantiate(prefab, myPos, palette.transform.rotation);
                myGO.AddComponent<Rigidbody>();
                myGO.AddComponent<InteractionBehaviour>();
                Rigidbody myRB = myGO.GetComponent<Rigidbody>();
                myRB.useGravity = false;
                myRB.constraints = RigidbodyConstraints.FreezeAll;
                
            }
            myPos = myPos + new Vector3(-0.56f, 0.02f, 0.02f);
              
        }
      
    }
}
