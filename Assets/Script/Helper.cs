using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public PhotonView PV;

    public GameObject FPSView;
    public GameObject UI;

    public string NICKNAME;

    public GameObject networkManager;
    public int Team;

    public TextMesh TDMname;
    public Manager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("Scripts").GetComponent<Manager>();
       
        if (PV.IsMine)
        {
            GetComponent<PhotonView>().RPC("UpdateNickName", RpcTarget.AllBuffered,PhotonNetwork.NickName);
            networkManager.SetActive(false);
            gameObject.tag = "LocalPlayer";
          
            FPSView.SetActive(true);
            UI.SetActive(true);
            GetComponent<PhotonView>().RPC("UpdateTeam", RpcTarget.AllBuffered, (int)PhotonNetwork.LocalPlayer.CustomProperties["TEAM"]);
            TDMname.gameObject.SetActive(false);
        }
        else
        {
            TDMname.gameObject.SetActive(true);
        }

    }

    [PunRPC]
    public void UpdateTeam(int team)
    {
        Team = team;
    }

    [PunRPC]
    public void UpdateNickName(string nickname)
    {
        NICKNAME = nickname;
        TDMname.text = nickname;
    }
    // Update is called once per frame
    void Update()
    {
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["GameMode"] == 1)
        {
            if (Team == (int)PhotonNetwork.LocalPlayer.CustomProperties["TEAM"] && !PV.IsMine)
            {
                TDMname.gameObject.SetActive(true);
            }
        }
       
        if (manager.Alive)
        {
            TDMname.transform.LookAt(manager.Player.transform);
        }

    }
}
