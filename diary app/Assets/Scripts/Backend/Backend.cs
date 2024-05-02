using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.CompilerServices;

public class Backend : MonoBehaviour
{
    public static Backend i;

    enum Resource {
        Users,
        Diaries,
        Trees,
    }

    enum Action {
        Create,
        Read,
        Update,
        Delete
    }



    private string url = "http://localhost:3000/api/";
    private void Awake() {
        if(i==null) i=this;
        DontDestroyOnLoad(gameObject);
    }

    string BuildUrl(Resource resource, Action action) {
        return $"{url}{resource.ToString().ToLower()}/{action.ToString().ToLower()}";
    }

    string BuildUrl(Resource resource, Action action, string param) {
        return $"{url}{resource.ToString().ToLower()}/{action.ToString().ToLower()}/{param}";
    }

    string BuildUrl(Resource resource, string param) {
        return $"{url}{resource.ToString().ToLower()}/{param}";
    }


    // Create User
    public void CreateUser(string nickname, Action<User> onSuccess) {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("nickname", nickname);

        string url = BuildUrl(Resource.Users, Action.Create);
        HttpRequest.i.Post<User>(url,DictToJson(data), onSuccess, AlertOnFailed);
    }
    

    
    // Read User
    public void ReadUser(string id, Action<User> onSuccess){
        string url = BuildUrl(Resource.Users,id);
        HttpRequest.i.Get<User>(url, onSuccess, AlertOnFailed);
    }

    // Update User
    public void UpdateUser(Action<User> onSuccess){
        User user = GameManager.i.GetUser();
        Debug.Log(JsonUtility.ToJson(user));
        string url = BuildUrl(Resource.Users, Action.Update, user.GetId());
        HttpRequest.i.Put<User>(url, JsonUtility.ToJson(user), onSuccess, AlertOnFailed);
    }


    // Read diary
    public void ReadDiary(string targetDate, Action<Diary> onSuccess){
        string url = BuildUrl(Resource.Diaries, $"{GameManager.i.GetUser().GetId()}/{targetDate}");
        HttpRequest.i.Get<Diary>(url,onSuccess, AlertOnFailed);
    }

    public void ReadDiary(string targetDate, Action<Diary> onSuccess, Action<string> onFailed){
        string url = BuildUrl(Resource.Diaries, $"{GameManager.i.GetUser().GetId()}/{targetDate}");
        HttpRequest.i.Get<Diary>(url,onSuccess, onFailed);
    }

    

    public void CreateDiary(string text, Action<Diary> onSuccess){
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("user_id",GameManager.i.GetUser().GetId());
        data.Add("text", text);
        data.Add("date", DateTime.Now.ToString("yyyyMMdd"));

        string url = BuildUrl(Resource.Diaries,Action.Create);
        HttpRequest.i.Post<Diary>(url, DictToJson(data), onSuccess, AlertOnFailed);
    }

    
    public void CreateTree(Tree tree, Action<string> onSuccess){
        string url = BuildUrl(Resource.Trees,Action.Create);
        HttpRequest.i.Post<string>(url, JsonUtility.ToJson(tree), onSuccess, AlertOnFailed);
    }

    public void ReadTrees(Action<TreeList> onSuccess){

        string url = BuildUrl(Resource.Trees, GameManager.i.GetUser().GetId());
        HttpRequest.i.Get<TreeList>(url, onSuccess, AlertOnFailed);
    }


/*???
    

    // 일기 갱신
    public void UpdateDiary(string id, string text, string targetDate, Action<DiaryResult> onSuccess){
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id", GameManager.i.GetUser().GetId());
        data.Add("text", text);
        data.Add("targetdate", targetDate);

        HttpRequest.i.Post<DiaryResult>(url+SubUrl.diary_update.ToString(), DictToJson(data), onSuccess, AlertOnFailed);
    }

    // 일기 삭제
    public void DeleteDiary(string id, string targetDate, Action<string> onSuccess){
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id",GameManager.i.GetUser().GetId());
        data.Add("targetdate", targetDate);

        HttpRequest.i.Post<string>(url+SubUrl.diary_delete.ToString(), DictToJson(data), onSuccess, AlertOnFailed);
    }

    
    



    // 회원 나무 전부 조회
    public void ReadAllObjects(String id, Action<TreeList> onSuccess){
       Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id",GameManager.i.GetUser().GetId());

        HttpRequest.i.Post<TreeList>(url+SubUrl.tree_list_read.ToString(), DictToJson(data), onSuccess, AlertOnFailed);
    }
    

    // 통계 읽기
    public void ReadStatistics(string id, string startDate, string endDate, Action<EmotionStats> onSuccess){
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id",GameManager.i.GetUser().GetId());
        data.Add("date_start", startDate);
        data.Add("date_end", endDate);
    
        HttpRequest.i.Post<EmotionStats>(url+SubUrl.statistic_read.ToString(), DictToJson(data), onSuccess, OnFailed);
    }
    */


    public void OnFailed(string message){
        Debug.Log(message);
    }

    public void AlertOnFailed(string message){
        UIPopUp.i.SetText("ERROR", message);
        UIPopUp.i.Show();
    }

    public string DictToJson<T>(Dictionary<string,T> dict){
        var x = dict.Select(d =>
                string.Format("\"{0}\": \"{1}\"", d.Key, string.Join(",", d.Value)));
        return "{" + string.Join(",", x) + "}";
    }

    
}
