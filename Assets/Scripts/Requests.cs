using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class Requests
{
    public static async UniTask<string> SendJson(string url, string json)
    {
        byte[] postData = Encoding.UTF8.GetBytes(json);

        UnityWebRequest www = UnityWebRequest.Post(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(postData);
        www.uploadHandler.contentType = "application/json";
        www.downloadHandler = new DownloadHandlerBuffer();

        await www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
            return null;
        }
        else
        {
            Debug.Log("POST response: " + www.downloadHandler.text);
            return www.downloadHandler.text;
        }
    }

    public static async UniTask<string> Get(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        await www.SendWebRequest();
        
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
            return null;
        }
        else
        {
            Debug.Log("GET response: " + www.downloadHandler.text);
            return www.downloadHandler.text;
        }
    }
}
