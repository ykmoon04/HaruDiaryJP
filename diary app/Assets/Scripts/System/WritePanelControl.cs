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
            Backend.i.ReadDiary(today.ToString("yyyyMMdd"), OnReadDiarySuccess, (noDiary)=>{
                Debug.Log("dsfdsfsd");
            });
        }
    }

    public void OnReadDiarySuccess(Diary diary){
        if(diary != null){
            DisplayResult(diary);
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
        // user info update
        GameManager.i.UpdateUser(diary.analysis);
        
        // gui
        initialPanel.SetActive(false);
        DisplayResult(diary);    
    
        LoadingWindow.i.EndLoading(0f,()=>{});
    }

    public void DisplayResult(Diary diary){
        diaryOfTheDay.text = diary.text;
        SetAnalysis(diary.analysis);

        resultPanel.SetActive(true);
        initialPanel.SetActive(false);
    }

    public void SetAnalysis(DiaryAnalysis analysis){
        if (analysis == null) return;

        Dictionary<string, (double value, string color, string emotionName)> emotions = new Dictionary<string, (double, string, string)>
        {
            { "joy", (analysis.joy, MyColor.joy, "행복") },
            { "sadness", (analysis.sadness, MyColor.sadness, "슬픔") },
            { "angry", (analysis.angry, MyColor.angry, "분노") },
            { "disgust", (analysis.disgust, MyColor.disgust, "혐오") },
            { "surprise", (analysis.surprise, MyColor.surprise, "놀람") },
            { "fear", (analysis.fear, MyColor.fear, "공포") }
        };

        string resultText = "오늘 하루는 \n";

        bool anyEmotionSignificant = false;
        foreach (var emotion in emotions) {
            if (emotion.Value.value > 0.1) {
                anyEmotionSignificant = true;
                resultText += string.Format("<color={0}>{1}</color>이 <color={2}>{3} 포인트</color>\n",
                    emotion.Value.color, emotion.Value.emotionName, emotion.Value.color, Math.Round(emotion.Value.value * 100, 0));
            }
        }

        resultText += anyEmotionSignificant ? "만큼 있었네요!" : "무난했어요!";
        StatOfTheDay.text = resultText;
    }
}

