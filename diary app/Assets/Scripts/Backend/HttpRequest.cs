using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;

public class HttpRequest : MonoBehaviour
{
    public static HttpRequest i;
    
    private void Awake() {
        if(i==null) i=this;
    }

    public void Get<T>(string url, Action<T> onSuccess, Action<string> onFailed)
    {
        StartCoroutine(UnityWebRequestGET(url, onSuccess, onFailed)) ;
    }

    // Get
    private IEnumerator UnityWebRequestGET<T>(string url, Action<T> onSuccess, Action<string> onFailed)
    {
        // UnityWebRequest에 내장되있는 GET 메소드를 사용한다.
        using (UnityWebRequest request = UnityWebRequest.Get(url)){
            yield return request.SendWebRequest();  // 응답이 올때까지 대기한다.

            // Check for errors first
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
                onFailed?.Invoke(request.error);
            }
            else{
                // Debug.Log(url+"\n" +request.downloadHandler.text);
        
                T res = JsonUtility.FromJson<T>(request.downloadHandler.text);
                if (request.responseCode == 200){
                    onSuccess?.Invoke(res);
                }
                else{
                    onFailed?.Invoke(request.responseCode.ToString());
                }
            }
        }
    }


    public void Post<T>(string url, string jsonString, Action<T> onSuccess,  Action<string> onFailed)
    {
        StartCoroutine(UnityWebRequestPOST(url, jsonString, onSuccess, onFailed));
    }

    // POST
    private IEnumerator UnityWebRequestPOST<T>(string url, string jsonString, Action<T> onSuccess, Action<string> onFailed)
    {   
        using (var request = new UnityWebRequest(url, "POST")){
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                onFailed?.Invoke(request.error);
            }
            else
            {
                T res = JsonUtility.FromJson<T>(request.downloadHandler.text);
                if (request.responseCode == 200)
                {
                    onSuccess?.Invoke(res);
                }
                else
                {
                    onFailed?.Invoke(request.responseCode.ToString());
                }
            }
        }
    }

    public void Put<T>(string url, string jsonString, Action<T> onSuccess,  Action<string> onFailed)
    {
        StartCoroutine(UnityWebRequestPut(url, jsonString, onSuccess, onFailed));
    }

    // POST
    private IEnumerator UnityWebRequestPut<T>(string url, string jsonString, Action<T> onSuccess, Action<string> onFailed)
    {   
        using (var request = new UnityWebRequest(url, "PUT")){
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                onFailed?.Invoke(request.error);
            }
            else
            {
                T res = JsonUtility.FromJson<T>(request.downloadHandler.text);
                if (request.responseCode == 200)
                {
                    onSuccess?.Invoke(res);
                }
                else
                {
                    onFailed?.Invoke(request.responseCode.ToString());
                }
            }
        }
    }
}
