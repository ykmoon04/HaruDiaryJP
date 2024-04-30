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


    // Read diary
    public void ReadDiary(string targetDate, Action<Diary> onSuccess){
        string url = BuildUrl(Resource.Diaries, $"{GameManager.i.GetUser().GetId()}/{targetDate}");
        HttpRequest.i.Get<Diary>(url,onSuccess, AlertOnFailed);
    }

    public void ReadDiary(string targetDate, Action<Diary> onSuccess, Action<string> onFailed){
        string url = BuildUrl(Resource.Diaries, $"{GameManager.i.GetUser().GetId()}/{targetDate}");
        HttpRequest.i.Get<Diary>(url,onSuccess, AlertOnFailed);
        /*

        string url = BuildUrl(Resource.Diaries, Action.Read);
        HttpRequest.i.Post<Diary>(url,DictToJson(data), onSuccess, AlertOnFailed);

        HttpRequest.i.Post<Diary>(url+SubUrl.diary_read.ToString(), DictToJson(data), onSuccess, OnFailed);

        */
    }

    

    public void CreateDiary(string text, Action<Diary> onSuccess){
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("user_id",GameManager.i.GetUser().GetId());
        data.Add("text", text);
        data.Add("date", DateTime.Now.ToString("yyyyMMdd"));

        string url = BuildUrl(Resource.Diaries,Action.Create);
        HttpRequest.i.Post<Diary>(url, DictToJson(data), onSuccess, AlertOnFailed);
    }


/*???
    public void ReadUser(string id, string password, Action<User> onSuccess, Action<string> onFailed){
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id",id);
        data.Add("password", password);

        HttpRequest.i.Post<User>(url+SubUrl.member_read.ToString(),DictToJson(data), onSuccess, onFailed);
    }

    */


/*

    // 유저 갱신 임시
    public void UpdateUserInfo(UserInfo field, string newData, Action<string> onSuccess){
        User user = GameManager.i.GetUser();

        if(user==null) {
            AlertOnFailed("유저가 존재하지 않음");
        }

        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id",user.GetId());
        data.Add("password", user.GetPassword());
        data.Add(field.ToString(), newData);

        HttpRequest.i.Post<string>(url+SubUrl.member_update.ToString(),DictToJson(data), onSuccess, AlertOnFailed);
    }

    // 회원 탈퇴
    public void DeleteUser(Action<string> onSuccess){
        User user = GameManager.i.GetUser();

        if(user==null) {
            AlertOnFailed("유저가 존재하지 않음");
        }

        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id",user.GetId());
        data.Add("password", user.GetPassword());
        
        HttpRequest.i.Post<string>(url+SubUrl.member_delete.ToString(), DictToJson(data), onSuccess, AlertOnFailed);
    }

    // 일기 등록
    // \n -> \n\n으로 변환하는 거는 호출부에서
    public void CreateDiary(string id, string text, Action<DiaryResult> onSuccess){
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id",GameManager.i.GetUser().GetId());
        data.Add("text", text);

        LoadingWindow.i.StartLoading();
        HttpRequest.i.Post<DiaryResult>(url+SubUrl.diary_create.ToString(), DictToJson(data), onSuccess, AlertOnFailed);
    }

    // 일기 읽기
    public void ReadDiary(string id, string targetDate, Action<Diary> onSuccess){
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id",GameManager.i.GetUser().GetId());
        data.Add("targetdate", targetDate);
        Debug.Log("확인- "+targetDate);
        HttpRequest.i.Post<Diary>(url+SubUrl.diary_read.ToString(), DictToJson(data), onSuccess, OnFailed);
    }

    public void ReadDiary(string id, string targetDate, Action<Diary> onSuccess, Action<string> onFailed){
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id",GameManager.i.GetUser().GetId());
        data.Add("targetdate", targetDate);
//        Debug.Log(targetDate);

        HttpRequest.i.Post<Diary>(url+SubUrl.diary_read.ToString(), DictToJson(data), onSuccess, onFailed);
    }

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

    
    // 나무 구매
    public void CreateObject(string emotion, double cost, Tree tree, Action<string> onSuccess){
        
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("id",GameManager.i.GetUser().GetId());
        data.Add("cost_sentiment",emotion);
        data.Add("cost_quantity",cost.ToString());
        data.Add("treename",tree.treename);
        data.Add("positionx",tree.positionx.ToString());
        data.Add("positiony",tree.positiony.ToString());
        data.Add("positionz",tree.positionz.ToString());

        HttpRequest.i.Post<string>(url+SubUrl.tree_create.ToString(), DictToJson(data), onSuccess, AlertOnFailed);
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
