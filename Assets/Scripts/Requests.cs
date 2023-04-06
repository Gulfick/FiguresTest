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
            www.Dispose();
            return null;
        }
        else
        {
            var response = www.downloadHandler.text;
            Debug.Log($"POST response: {response}");
            www.Dispose();
            return response;
        }
    }

    public static async UniTask<string> Get(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        await www.SendWebRequest();
        
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
            www.Dispose();
            return null;
        }
        else
        {
            var response = www.downloadHandler.text;
            Debug.Log($"GET response: {response}");
            www.Dispose();
            return response;
        }
    }
}
