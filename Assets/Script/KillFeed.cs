using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class KillFeed : MonoBehaviour
{
    public Transform KillFeedArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void PlayerKilled(string killer, string killed)
    {
        GameObject prefab = PhotonNetwork.Instantiate("KillFeedPrefab", KillFeedArea.position, KillFeedArea.rotation);
        prefab.transform.SetParent(KillFeedArea);
        prefab.transform.SetAsFirstSibling();
        prefab.GetComponent<PhotonView>().RPC("UpdateNames", RpcTarget.All, killer, killed);
    }
}
