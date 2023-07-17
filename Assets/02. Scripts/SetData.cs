using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetData : MonoBehaviour
{

    public string _Account_Reg = "http://localhost/ndInsertUser.php";
    public string _Account_Tbl = "http://localhost/ndInsertTables.php";
    public string _Account_Get = "http://localhost/ndGetUser.php";
    public string _Ranking_Set = "http://localhost/ndGetRanking.php";
    public string _Data_Get = "http://localhost/ndGetData.php";
    public string _Get_Ranking = "http://localhost/ndSendRanking.php";

    public Text _CheckStatus;
    public Text _ID_Value;
    public Text _PW_Value;

    public string _ID_Value_string;
    public string _NUM_Value;
    public int _Gold_Value;
    public int _DISTANCE_Value;
    public int _ZOMBIE_Value;

    //public List<string> rankDistanceList = new List<string>();
   // public List<string> rankZombieList = new List<string>();
    //public List<string> rankUserNumList = new List<string>();

    private void Update()
    {
        /*_ID_Value_string = _ID_Value.text;
        _NUM_Value = GameManager.instance.playerNum;
        _Gold_Value = GameManager.instance.gold_;
        _DISTANCE_Value = GameManager.instance.distance_;
        _ZOMBIE_Value = GameManager.instance.zombie_;*/
    }

    public void SendAccount()
    {
        StartCoroutine(SetAccout(_ID_Value.text, _PW_Value.text));
    }

    IEnumerator SetAccout(string _id, string _pw)
    {
        WWWForm _PostData = new WWWForm();
        _PostData.AddField("ID_Value", _id.Trim());
        _PostData.AddField("PW_Value", _pw.Trim());

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Account_Reg, _PostData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("Success"))
                {
                    _CheckStatus.text = "Complete";
                    InsertTables();
                }
                else if (_sendData.downloadHandler.text.Equals("1"))
                {
                    _CheckStatus.text = "1Error";
                }
                else if (_sendData.downloadHandler.text.Equals("2"))
                {
                    _CheckStatus.text = "2Error";
                }
                else
                    _CheckStatus.text = _sendData.downloadHandler.text;
            }

            _sendData.Dispose();
        }

        _ID_Value.gameObject.transform.parent.GetComponent<InputField>().text = "";
        _PW_Value.gameObject.transform.parent.GetComponent<InputField>().text = "";
    }

    public void GetAccount()
    {
        StartCoroutine(GetAccout(_ID_Value.text, _PW_Value.text));
    }

    IEnumerator GetAccout(string _id, string _pw)
    {
        WWWForm _PostData = new WWWForm();
        _PostData.AddField("ID_Value", _id.Trim());
        _PostData.AddField("PW_Value", _pw.Trim());

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Account_Get, _PostData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("99"))
                {
                    _CheckStatus.text = _sendData.downloadHandler.text;
                   
                }
                else
                {
                    _CheckStatus.text = "Complete";
                    //GameManager.instance.playerID = _ID_Value_string;
                    //GameManager.instance.playerNum = _sendData.downloadHandler.text;
                }
            }

            _sendData.Dispose();
        }

        _ID_Value.gameObject.transform.parent.GetComponent<InputField>().text = "";
        _PW_Value.gameObject.transform.parent.GetComponent<InputField>().text = "";
    }

    public void InsertTables()
    {
        StartCoroutine(InsertTables(_ID_Value.text));
    }

    IEnumerator InsertTables(string _id)
    {
        WWWForm _PostData = new WWWForm();
        _PostData.AddField("ID_Value", _id.Trim());

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Account_Tbl, _PostData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("99"))
                {
                    _CheckStatus.text = _sendData.downloadHandler.text;

                }
                else
                {
                    _CheckStatus.text = "Complete";
                   // GameManager.instance.playerID = _ID_Value_string;
                    //GameManager.instance.playerNum = _sendData.downloadHandler.text;
                }
            }

            _sendData.Dispose();
        }
    }

    public void GetGold()
    {
        StartCoroutine(GetGold(_NUM_Value, "1", _Gold_Value));
    }

    IEnumerator GetGold(string _num, string _data, int _gold)
    {
        WWWForm _PostData = new WWWForm();
        _PostData.AddField("NUM_Value", _num.Trim());
        _PostData.AddField("DATA_Value", _data.Trim());
        _PostData.AddField("GOLD_Value", _gold);

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Data_Get, _PostData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("99"))
                {
                    _CheckStatus.text = _sendData.downloadHandler.text;

                }
                else
                {
                    _CheckStatus.text = "Complete";
                    //GameManager.instance.gold = _sendData.downloadHandler.text;
                }
            }
            _sendData.Dispose();
        }
    }
    public void GetZombie()
    {
        StartCoroutine(GetZombie(_NUM_Value, "2"));
    }

    IEnumerator GetZombie(string _num, string _data)
    {
        WWWForm _PostData = new WWWForm();
        _PostData.AddField("NUM_Value", _num.Trim());
        _PostData.AddField("DATA_Value", _data.Trim());

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Data_Get, _PostData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("99"))
                {
                    _CheckStatus.text = _sendData.downloadHandler.text;

                }
                else
                {
                    _CheckStatus.text = "Complete";
                   // GameManager.instance.zombie = _sendData.downloadHandler.text;
                }
            }
            _sendData.Dispose();
        }
    }
    public void GetDistance()
    {
        StartCoroutine(GetDistance(_NUM_Value, "3", _DISTANCE_Value));
    }

    IEnumerator GetDistance(string _num, string _data, int _dist)
    {
        WWWForm _PostData = new WWWForm();
        _PostData.AddField("NUM_Value", _num.Trim());
        _PostData.AddField("DATA_Value", _data.Trim());
        _PostData.AddField("DIST_Value", _dist);

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Data_Get, _PostData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("99"))
                {
                    _CheckStatus.text = _sendData.downloadHandler.text;

                }
                else
                {
                    _CheckStatus.text = "Complete";
                   // GameManager.instance.distance = _sendData.downloadHandler.text;
                }
            }
            _sendData.Dispose();
        }
    }

    public void SetRank()
    {
        StartCoroutine(SetRank(_NUM_Value, _ZOMBIE_Value, _DISTANCE_Value));
    }
    
    IEnumerator SetRank(string _num, int _zom, int _dist)
    {
        WWWForm _PostData = new WWWForm();
        _PostData.AddField("NUM_Value", _num.Trim());
        _PostData.AddField("ZOM_Value", _zom);
        _PostData.AddField("DIST_Value", _dist);

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Ranking_Set, _PostData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("99"))
                {
                    _CheckStatus.text = _sendData.downloadHandler.text;

                }
                else
                {
                    _CheckStatus.text = "Complete";
                }
            }
            _sendData.Dispose();
        }
    }

    public void GetRankZombie()
    {
        StartCoroutine(GetRankZombie("1", _DISTANCE_Value, _ZOMBIE_Value));
    }

    IEnumerator GetRankZombie(string _rank, int _dist, int _zom)
    {
        WWWForm _PostData = new WWWForm();
        _PostData.AddField("RANK_Value", _rank.Trim());
        _PostData.AddField("DIST_Value", _dist);
        _PostData.AddField("ZOM_Value", _zom);

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Get_Ranking, _PostData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("99"))
                {
                    _CheckStatus.text = _sendData.downloadHandler.text;

                }
                else
                {
                    _CheckStatus.text = "Complete";
                    
                    string rankdata = _sendData.downloadHandler.text;
                    RankZombie[] rank = JsonUtility.FromJson<RankZombie[]>(rankdata);

                    // 데이터 값 출력 및 리스트에 저장
                    List<string> rankZombieList = new List<string>();
                    List<string> rankUserNumList = new List<string>();

                    foreach (RankZombie obj in rank)
                    {
                        string zombie = JsonUtility.ToJson(obj.zombie);

                        rankZombieList.Add(zombie);

                        string userNum = JsonUtility.ToJson(obj.userNum);

                        rankUserNumList.Add(userNum);
                    }
                    print(rankZombieList.ToArray());
                    print(rankUserNumList.ToArray());
                }
            }
            _sendData.Dispose();
        }
    }
    public void GetRankDistance()
    {
        StartCoroutine(GetRankDistance("2", _DISTANCE_Value, _ZOMBIE_Value));
    }

    IEnumerator GetRankDistance(string _rank, int _dist, int _zom)
    {
        WWWForm _PostData = new WWWForm();
        _PostData.AddField("RANK_Value", _rank.Trim());
        _PostData.AddField("DIST_Value", _dist);
        _PostData.AddField("ZOM_Value", _zom);

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Get_Ranking, _PostData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("99"))
                {
                    _CheckStatus.text = _sendData.downloadHandler.text;

                }
                else
                {
                    _CheckStatus.text = "Complete";

                    string rankData = _sendData.downloadHandler.text;
                    print(_sendData.downloadHandler.text);
                    // JSON 데이터 역직렬화
                    RankDistance rank = JsonUtility.FromJson<RankDistance>(rankData);

                    // 데이터 값 출력 및 리스트에 저장
                    List<string> rankDistanceList = new List<string>();
                    List<string> rankUserNumList = new List<string>();

                  /*  foreach (RankData obj in rank.data)
                    {
                        rankDistanceList.Add(obj.distance);
                        rankUserNumList.Add(obj.userNum);
                    }
                    print(rankDistanceList.ToArray());
                    print(rankUserNumList.ToArray());*/
                }
            }
            _sendData.Dispose();
        }
    }
    public void GetUserZombie()
    {
        StartCoroutine(GetUserZombie("3", _DISTANCE_Value, _ZOMBIE_Value));
    }

    IEnumerator GetUserZombie(string _rank, int _dist, int _zom)
    {
        WWWForm _PostData = new WWWForm();
        _PostData.AddField("RANK_Value", _rank.Trim());
        _PostData.AddField("DIST_Value", _dist);
        _PostData.AddField("ZOM_Value", _zom);

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Get_Ranking, _PostData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("99"))
                {
                    _CheckStatus.text = _sendData.downloadHandler.text;

                }
                else
                {
                    _CheckStatus.text = "Complete";

                    string rankdata = _sendData.downloadHandler.text;
                    RankZombie[] rank = JsonUtility.FromJson<RankZombie[]>(rankdata);

                    // 데이터 값 출력 및 리스트에 저장
                    List<string> rankZombieList = new List<string>();
                    List<string> rankUserNumList = new List<string>();

                    foreach (RankZombie obj in rank)
                    {
                        string zombie = JsonUtility.ToJson(obj.zombie);

                        rankZombieList.Add(zombie);

                        string userNum = JsonUtility.ToJson(obj.userNum);

                        rankUserNumList.Add(userNum);
                    }
                    print(rankZombieList.ToArray());
                    print(rankUserNumList.ToArray());
                }
            }
            _sendData.Dispose();
        }
    }
    public void GetUserDistance()
    {
        StartCoroutine(GetUserDistance("4", _DISTANCE_Value, _ZOMBIE_Value));
    }

    IEnumerator GetUserDistance(string _rank, int _dist, int _zom)
    {
        WWWForm _postData = new WWWForm();
        _postData.AddField("RANK_Value", _rank.Trim());
        _postData.AddField("DIST_Value", _dist);
        _postData.AddField("ZOM_Value", _zom);

        using (UnityWebRequest _sendData = UnityWebRequest.Post(_Get_Ranking, _postData))
        {
            yield return _sendData.SendWebRequest();

            if (_sendData.isNetworkError || _sendData.isHttpError)
            {
                _CheckStatus.text = _sendData.error;
            }
            else
            {
                if (_sendData.downloadHandler.text.Equals("99"))
                {
                    _CheckStatus.text = _sendData.downloadHandler.text;
                }
                else
                {
                    _CheckStatus.text = "Complete";

                    string rankData = _sendData.downloadHandler.text;
                    Debug.Log(rankData);

                    var rank = JsonUtility.FromJson<RankData>(rankData);

                    // 데이터 값 출력 및 리스트에 저장
                    List<string> rankDistanceList = new List<string>();
                    List<string> rankUserNumList = new List<string>();

                    foreach (RankDistance obj in rank.results)
                    {
                        rankDistanceList.Add(obj.distance);
                        rankUserNumList.Add(obj.userNum);
                    }

                    // 리스트 출력
                    foreach (string distance in rankDistanceList)
                    {
                        Debug.Log("Distance: " + distance);
                    }

                    foreach (string userNum in rankUserNumList)
                    {
                        Debug.Log("UserNum: " + userNum);
                    }

                }
            }
        }
    }
    [System.Serializable]
    public class RankData
    {
        public RankDistance[] results;
    }
    [System.Serializable]
    public class RankDistance
    {
        public string userNum;
        public string distance;
    }
    public class RankZombie
    {
        public string userNum;
        public string zombie;
    }
}

