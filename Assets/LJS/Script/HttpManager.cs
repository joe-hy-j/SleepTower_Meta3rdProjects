using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;   // http 통신을 위한 네임 스페이스
using System.Text;      // json, csv같은 문서 형태의 인코딩(UTF-8)을 위한 네임 스페이스
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;

public class HttpManager : MonoBehaviour
{
    public string url;
    public Text text_response;    
    public List<InputField> userInputs = new List<InputField>();
    public Toggle freeUser;

    public void Get()
    {
        StartCoroutine(GetRequest(url));
    }

    // Get 통신 코루틴 함수
    IEnumerator GetRequest(string url)
    {
        // http Get 통신 준비를 한다.
        UnityWebRequest request = UnityWebRequest.Get(url);

        // 서버에 Get 요청을 하고, 서버로부터 응답이 올 때까지 대기한다.
        yield return request.SendWebRequest();

        // 만일, 서버로부터 온 응답이 성공(200)이라면...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 응답받은 데이터를 출력한다.
            string response = request.downloadHandler.text;

            print(response);
            text_response.text = response;
        }
        // 그렇지 않다면(400, 404 etc)...
        else
        {
            // 에러 내용을 출력한다.
            print(request.error);
            text_response.text = request.error;
        }

    }

    public void GetJson()
    {   
        StartCoroutine(GetJsonImageRequest(url));
    }

    IEnumerator GetJsonImageRequest(string url)
    {
        // url로부터 Get으로 요청을 준비한다.
        UnityWebRequest request = UnityWebRequest.Get(url);

        // 준비된 요청을 서버에 전달하고 응답이 올때까지 기다린다.
        yield return request.SendWebRequest();

        // 만일, 응답이 성공이라면...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 텍스트를 받는다.
            string result = request.downloadHandler.text;
            // 응답 받은 json 데이터를 RequestImage 구조체 형태로 파싱한다.
            RequestImage reqImageData = JsonUtility.FromJson<RequestImage>(result);

            //byte[] binaries = Encoding.UTF8.GetBytes(reqImageData.img);
            byte[] binaries = Convert.FromBase64String(reqImageData.img);
            print(binaries.Length);
            if (binaries.Length > 0)
            {
                Texture2D texture = new Texture2D(2, 2);

                // byte 배열로 된 raw 데이터를 텍스쳐 형태로 변환해서 texture2D 인스턴스로 변환한다.
                texture.LoadImage(binaries);
                
            }

        }
        // 그렇지 않다면...
        else
        {
            // 에러 내용을 text_response에 전달한다.
            text_response.text = request.responseCode + ": " + request.error;
            Debug.LogError(request.responseCode + ": " + request.error);
        }

    }

    // 서버에 Json 데이터를 Post하는 함수
    public void PostJson()
    {
        StartCoroutine(PostJsonRequest(url));
    }

    IEnumerator PostJsonRequest(string url)
    {
        // 사용자의 입력 정보를 Json 데이터로 변환하기
        JoinUserData userData = new JoinUserData();
        userData.id = Convert.ToInt32(userInputs[0].text);
        userData.password = userInputs[1].text;
        userData.nickName = userInputs[2].text;
        userData.freeAccount = freeUser.isOn;
        string userJsonData = JsonUtility.ToJson(userData, true);
        byte[] jsonBins = Encoding.UTF8.GetBytes(userJsonData);

        // Post를 하기 위한 준비를 한다.
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(jsonBins);
        request.downloadHandler = new DownloadHandlerBuffer();

        // 서버에 Post를 전송하고 응답이 올 때까지 기다린다.
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // 다운로드 핸들러에서 텍스트 값을 받아서 UI에 출력한다.
            string response = request.downloadHandler.text;
            text_response.text = response;
            Debug.LogWarning(response);
        }
        else
        {
            text_response.text = request.error;
            Debug.LogError(request.error);
        }
    }

    [System.Serializable]
    public struct RequestImage
    {
        public string img;
    }

    [System.Serializable]
    public struct JoinUserData
    {
        public int id;
        public string password;
        public string nickName;
        public bool freeAccount;
    }
}