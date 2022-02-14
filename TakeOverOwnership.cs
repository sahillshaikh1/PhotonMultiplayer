using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TakeOverOwnership : MonoBehaviour
{
    [HideInInspector]
    public GameObject parentObj;

    //// PRIVATE VARS
    //private bool isOwnershipMine;

    public void RequestOwnership()
    {
        if (parentObj != null)
        {
            //Debug.Log("Parent Ref Holded Properly >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            parentObj.GetComponent<PhotonView>().RequestOwnership();
        }
        else
        {
            //Debug.Log("New parent Ref Not Assigned Properlyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
            transform.parent.GetComponent<PhotonView>().RequestOwnership();
        }
    }

}


// BACKUP CODE
//GameObject parent;

//private void Start()
//{
//    parent = transform.parent.gameObject;
//}

//public void RequestOwnership()
//{
    //transform.parent.GetComponent<PhotonView>().RequestOwnership();

//    parent.GetComponent<PhotonView>().RequestOwnership();
//}

