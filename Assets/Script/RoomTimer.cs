using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTimer : MonoBehaviour
{
    public Text time;
    public bool count;
    public int Time;
    ExitGames.Client.Photon.Hashtable setTime = new ExitGames.Client.Photon.Hashtable();
    public Manager manager;
    public bool flick;
    // Start is called before the first frame update
    void Start()
    {
        count = true;
    }

    // Update is called once per frame
    void Update()
    {
       

        Time = (int)PhotonNetwork.CurrentRoom.CustomProperties["Time"];
        float minutes = Mathf.FloorToInt((int)PhotonNetwork.CurrentRoom.CustomProperties["Time"] / 60);
        float seconds = Mathf.FloorToInt((int)PhotonNetwork.CurrentRoom.CustomProperties["Time"] % 60);

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (PhotonNetwork.IsMasterClient)
        {
            if (count)
            {
                count = false;
                StartCoroutine(timer());
            }
        }

        if (Time <= 0 && !flick)
        {

            flick = true;
            StartCoroutine(endGame());
        }

        if (flick)
        {
            manager.scoreboardCanvas = true;
            manager.scoreboardUI.SetActive(true);
            time.gameObject.SetActive(false);
        }
    }
    IEnumerator timer()
    {
        
        yield return new WaitForSeconds(1);
        int nextTime = Time -= 1;
        setTime["Time"] = nextTime;
        PhotonNetwork.CurrentRoom.SetCustomProperties(setTime);
        count = true;
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(5);
        PlayerPrefs.SetInt("GO", 1);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Menu");
    }
}
