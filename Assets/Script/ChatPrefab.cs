using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChatPrefab : MonoBehaviour
{
    public Text Username;
    public Text Message;
    public bool local;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            StartCoroutine(deleteDelay());
        }
    }

    IEnumerator deleteDelay()
    {
        yield return new WaitForSeconds(10);
       
        if (GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (local)
        {
            Username.color = Color.yellow;
            Message.color = Color.yellow;
        }
    }
    [PunRPC]
    public void MessageUser(string username)
    {
        Username.text = username;
        if (GetComponent<PhotonView>().IsMine)
        {
            local = true;
           
        }
    }

    [PunRPC]
    public void MessageContent(string message)
    {
        Message.text = message;
       
    }
}
