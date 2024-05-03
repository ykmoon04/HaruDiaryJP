
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;

public class DateItem : MonoBehaviour
{
    [SerializeField]
    GameObject diaryHist;
    [SerializeField]
    TextMeshProUGUI dateLabel;

    [SerializeField]
    TextMeshProUGUI content;

    public Text joy, sadness, fear, disgust, surprise, angry;
    Diary diary;

    public void OnClick(){
        if(diary == null){
            Debug.Log("hello");
            return;
        }
        
        Debug.Log("hello2");
        DateTime date = DateTime.ParseExact(diary.date, "yyyyMMdd", CultureInfo.InvariantCulture);
        string formattedDate = date.ToString("yyyy.MM.dd");
        dateLabel.text = formattedDate;

        content.text = diary.text;

        joy.text = $"幸せ {Math.Round(diary.analysis.joy,2).ToString()}";
        sadness.text = $"悲しみ {Math.Round(diary.analysis.sadness,2).ToString()}";
        fear.text = $"恐れ {Math.Round(diary.analysis.fear,2).ToString()}";
        disgust.text = $"嫌悪 {Math.Round(diary.analysis.disgust,2).ToString()}";
        surprise.text = $"驚き {Math.Round(diary.analysis.surprise,2).ToString()}";
        angry.text = $"怒り {Math.Round(diary.analysis.angry,2).ToString()}";
        
        diaryHist.SetActive(true);
    }

    public void SetDiary(Diary diary){
        this.diary = diary;
    }

}
