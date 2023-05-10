using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;

public class Menu : MonoBehaviourPunCallbacks
{
    public int menustate;

    public GameObject loadingCanvas;
    public GameObject mainCanvas;
    public GameObject createCanvas;
    public GameObject lobbyCanvas;

    public Text loadingText;
    public int loadingTextState;
    public bool loadingFlick;

    public InputField createRoomIF;

    public Transform roomListHolder;
    public GameObject roomListItemPrefab;

    public Text quality;
    public GameObject optionsUI;

    public int Gamemode;
    public Image DMButton;
    public Image TDMButton;

    public GameObject GOCanvas;
    public Text GOKills;
    public Text GODeaths;
    public Text GOScore;
    public Text GOKDR;



    // Start is called before the first frame update
    void Start()
    {
        loadingFlick = true;
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(0, 999);
        Debug.Log("connected to master server");
        PhotonNetwork.JoinLobby();

    }
    public override void OnJoinedLobby()
    {
        if (PlayerPrefs.GetInt("GO") == 0)
        {
            menustate = 1;
        }
        else
        {
            menustate = 6;
        }
      
    }
    public void Continue()
    {
        PlayerPrefs.SetInt("GO", 0);
        menustate = 1;
    }
    public void DM()
    {
        Gamemode = 0;
    }

    public void TDM()
    {
        Gamemode = 1;
    }
    // Update is called once per frame
    void Update()
    {
        GOKills.text ="Kill : " + PlayerPrefs.GetInt("GOKills").ToString();
        GODeaths.text = "Deaths : " + PlayerPrefs.GetInt("GODeaths").ToString();
        GOScore.text = "Score : " + PlayerPrefs.GetInt("GOScore").ToString();

        

        if (Gamemode == 0)
        {
            DMButton.color = Color.green;
            TDMButton.color = Color.white;
        }

        if (Gamemode == 1)
        {
            DMButton.color = Color.white;
            TDMButton.color = Color.green;
        }
        Cursor.lockState = CursorLockMode.None;
        //code
        int qualityLevel = QualitySettings.GetQualityLevel();
        if (qualityLevel == 0)
        {
            quality.text = "Very Low";
        }

        if (qualityLevel == 1)
        {
            quality.text = "Low";
        }

        if (qualityLevel == 2)
        {
            quality.text = "Medium";
        }

        if (qualityLevel == 3)
        {
            quality.text = "High";
        }

        if (qualityLevel == 4)
        {
            quality.text = "Very High";
        }

        if (qualityLevel == 5)
        {
            quality.text = "Ultra";
        }

        if (loadingFlick)
        {
            loadingFlick = false;
            StartCoroutine(loadstatechange());
        }

        if (loadingTextState == 0)
        {
            loadingText.text = "Loading";
        }
        if (loadingTextState == 1)
        {
            loadingText.text = "Loading.";
        }
        if (loadingTextState == 2)
        {
            loadingText.text = "Loading..";
        }
        if (loadingTextState == 3)
        {
            loadingText.text = "Loading...";
        }

        if (menustate == 0)
        {
            loadingCanvas.SetActive(true);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(false);
            optionsUI.SetActive(false);
            GOCanvas.SetActive(false);
        }

        if (menustate == 1)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(true);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(false);
            optionsUI.SetActive(false);
            GOCanvas.SetActive(false);
        }

        if (menustate == 2)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(true);
            lobbyCanvas.SetActive(false);
            optionsUI.SetActive(false);
            GOCanvas.SetActive(false);
        }

        if (menustate == 3)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(true);
            optionsUI.SetActive(false);
            GOCanvas.SetActive(false);
        }

        if (menustate == 4)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(false);
            optionsUI.SetActive(true);
            GOCanvas.SetActive(false);
        }

        if (menustate == 6)
        {
            loadingCanvas.SetActive(false);
            mainCanvas.SetActive(false);
            createCanvas.SetActive(false);
            lobbyCanvas.SetActive(false);
            optionsUI.SetActive(false);
            GOCanvas.SetActive(true);
        }

    }

        IEnumerator loadstatechange()
        {
            yield return new WaitForSeconds(0.15f);
            loadingFlick = true;
            if (loadingTextState < 3)
            {
                loadingTextState++;

            }
            else
            {
                loadingTextState = 0;
            }
        }
    
    

    public void createroom()
    {
        menustate = 2;
    }

    public void lobby()
    {
        menustate = 3;
    }

    public void Back()
    {
        menustate = 1;
    }

    public void options()
    {
        menustate = 4;
    }
    public void QuickStart()
    {
        PhotonNetwork.JoinRandomRoom();

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        Hashtable options = new Hashtable();

        string[] lobbyProps = { "GT" };
        roomOptions.CustomRoomPropertiesForLobby = lobbyProps;
        options.Add("GT", 0);

        options.Add("Time",180);
        options.Add("redscore", 0);
        options.Add("bluescore", 0);
        options.Add("GameMode", 0);
        roomOptions.CustomRoomProperties = options;
        PhotonNetwork.CreateRoom("Room" + Random.Range(0, 1000),roomOptions);
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(createRoomIF.text))
        {
            return;
        }

        RoomOptions roomOptions = new RoomOptions();
        Hashtable options = new Hashtable();


        string[] lobbyProps = { "GT" };
        roomOptions.CustomRoomPropertiesForLobby = lobbyProps;
        options.Add("GT", Gamemode);

        options.Add("Time", 180);
        options.Add("redscore", 0);
        options.Add("bluescore", 0);
        options.Add("GameMode", Gamemode);
        roomOptions.CustomRoomProperties = options;

        PhotonNetwork.CreateRoom(createRoomIF.text,roomOptions);
        menustate = 0;
        loadingTextState = 0;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Development");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListHolder)
        {
            Destroy(trans.gameObject);
        }

        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListItemPrefab, roomListHolder).GetComponent<RoomListItem>().configure(roomList[i]);
        }
    }

    public void IncreaseGraphics()
    {
        QualitySettings.IncreaseLevel(true);
    }

    public void DecreaseGraphics()
    {
        QualitySettings.DecreaseLevel(true);
    }
}

