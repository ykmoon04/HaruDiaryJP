using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


// 다이어리 쓰기 관련

public class WritePanelControl : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI date;
    [SerializeField]
    GameObject initialPanel;

    [SerializeField]
    TextMeshProUGUI diaryInput;

    [SerializeField]
    GameObject resultPanel;

    [SerializeField]
    TextMeshProUGUI diaryOfTheDay;

    [SerializeField]
    TextMeshProUGUI StatOfTheDay;

    DateTime today;


    
    public void Init(){
        today = DateTime.Now;
        date.text= today.ToString("yyyy.MM.dd");
        ReadDiary();
    }
    private void ReadDiary(){
        if(GameManager.i.GetUser() != null){
            Backend.i.ReadDiary(today.ToString("yyyyMMdd"), OnReadDiarySuccess);
        }
    }

    public void OnReadDiarySuccess(Diary diary){
        Debug.Log("Success to read diary");
        if(diary != null){
            SetResultPanel(diary);
            // GameManager.i.UpdateUser();
        }
    }

    public void OnSubmitBtnClick(){
        if (GameManager.i.GetUser() != null && diaryInput.text != "") {
            string content = diaryInput.text;
            content = content.Replace('\n',' ');

            LoadingWindow.i.StartLoading();
            Backend.i.CreateDiary(content, OnSubmitSuccess);
        }
    }

    public void OnSubmitSuccess(Diary diary){
        initialPanel.SetActive(false);
        SetResultPanel(diary);
        GameManager.i.UpdateUser();
        
        LoadingWindow.i.EndLoading(0f,()=>{});
    }

    public void SetResultPanel(Diary diary){
        Debug.Log("Set diary result panel");
        diaryOfTheDay.text = diary.text;
        SetAnalysis(diary.analysis);

        resultPanel.SetActive(true);
        initialPanel.SetActive(false);
    }

    public void SetAnalysis(DiaryAnalysis analysis){
        if(analysis != null){
            StatOfTheDay.text = "오늘 하루는 \n";
            if(analysis.joy > 0.01){
                StatOfTheDay.text += string.Format("<color={0}>행복</color>이 <color={1}>{2} 포인트</color>\n", MyColor.joy, MyColor.joy, Math.Round(analysis.joy*100,0));
            }
            if(analysis.sadness > 0.01){
                StatOfTheDay.text +=  string.Format("<color={0}>슬픔</color>이 <color={1}>{2} 포인트</color>\n", MyColor.sadness,MyColor.sadness, Math.Round(analysis.sadness*100,0));

            }
            if(analysis.angry > 0.01){
                StatOfTheDay.text +=  string.Format("<color={0}>분노</color>가 <color={1}>{2} 포인트</color>\n", MyColor.angry,MyColor.angry, Math.Round(analysis.angry*100,0));

            }
            if(analysis.disgust > 0.01){
                StatOfTheDay.text +=  string.Format("<color={0}>혐오</color>가 <color={1}>{2} 포인트</color>\n", MyColor.disgust,MyColor.disgust, Math.Round(analysis.disgust*100,0));

            }
            if(analysis.surprise > 0.01){
                StatOfTheDay.text +=  string.Format("<color={0}>놀람</color>이 <color={1}>{2} 포인트</color>\n",MyColor.surprise, MyColor.surprise, Math.Round(analysis.surprise*100,0));
            }
            if(analysis.fear > 0.01){
                StatOfTheDay.text +=  string.Format("<color={0}>공포</color>가 <color={1}>{2} 포인트</color>\n", MyColor.fear, MyColor.fear, Math.Round(analysis.fear*100,0));
            }

            if(StatOfTheDay.text == "오늘 하루는 \n"){
                StatOfTheDay.text += "무난했어요!";
            }
            else{
                StatOfTheDay.text += "만큼 있었네요!";
            } 
        }
    }
}

