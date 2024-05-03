using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



public class CalendarController : MonoBehaviour
{
    public RectTransform background;
    public GameObject _calendarPanel;
    public Text _yearNumText;
    public Text _monthNumText;

    public GameObject _item;

    public List<GameObject> _dateItems = new List<GameObject>();
    const int _totalDateNum = 42;

    private DateTime _dateTime;


    private void OnEnable() {
        ReloadPanel();
    }

    public void Init(){
        Vector3 startPos = _item.transform.localPosition;
        _dateItems.Clear();
        _dateItems.Add(_item);

        for (int i = 1; i < _totalDateNum; i++)
        {
            GameObject item = GameObject.Instantiate(_item) as GameObject;
            item.name = "Item" + (i + 1).ToString();
            item.transform.SetParent(_item.transform.parent);
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3((i % 7) * 31 + startPos.x, startPos.y - (i / 7) * 25, startPos.z);

            _dateItems.Add(item);
        }

        _dateTime = DateTime.Now;
    }

    public void ReloadPanel(){
        _dateTime = DateTime.Now;
        CreateCalendar();
    }

    void CreateCalendar()
    {
        _yearNumText.text = _dateTime.Year.ToString()+"년";
        _monthNumText.text = _dateTime.Month.ToString()+"월";


        // Read all diaries of the month first
        Dictionary<string, Diary> diaryOfMonth = new Dictionary<string, Diary>();
        Backend.i.ReadDiaries(_dateTime.ToString("yyyyMM"),(diaries)=>{
            foreach(Diary diary in diaries.diaries){
                diaryOfMonth.Add(diary.date, diary);
            }
            CreateDateItem(diaryOfMonth);
        });
    }

    public void CreateDateItem(Dictionary<string, Diary> diaryOfMonth){
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);
        int lastDay = DateTime.DaysInMonth(_dateTime.Year, _dateTime.Month);

        if(index+lastDay>35) {
            background.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1290);}
        else background.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1125);

        int date = 0;
        for (int i = 0; i < _totalDateNum; i++)
        {
            Text label = _dateItems[i].GetComponentInChildren<Text>();
            Image checkImg = _dateItems[i].GetComponentsInChildren<Image>(true)[1];

            if (i >= index && i < index + lastDay)
            {
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    _dateItems[i].SetActive(true);

                    _dateItems[i].GetComponent<Image>().enabled = true;
                    _dateItems[i].GetComponent<Button>().enabled = true;

                    string targetDate = string.Format("{0}{1:D2}{2:D2}",thatDay.Year, thatDay.Month, date+1);
                    if(diaryOfMonth.Keys.Contains(targetDate)){
                        Debug.Log(targetDate);
                        _dateItems[i].GetComponent<DateItem>().SetDiary(diaryOfMonth[targetDate]);
                        Emotion em = diaryOfMonth[targetDate].GetMaxEmotionType();
                        Color color;

                        checkImg.enabled=true;
                        
                        switch(em){
                            case Emotion.Joy:
                                    ColorUtility.TryParseHtmlString(MyColor.joy, out color);
                                checkImg.color = color;
                            break;
                            case Emotion.Sadness:
                                ColorUtility.TryParseHtmlString(MyColor.sadness, out color);
                                checkImg.color = color;
                            break;
                            case Emotion.Disgust:
                                ColorUtility.TryParseHtmlString(MyColor.disgust, out color);
                                checkImg.color = color;                                
                            break;
                            case Emotion.Angry:
                                ColorUtility.TryParseHtmlString(MyColor.angry, out color);
                                checkImg.color = color;                                
                                break;
                            case Emotion.Surprise:
                                ColorUtility.TryParseHtmlString(MyColor.surprise, out color);
                                checkImg.color = color;                                
                                break;
                            case Emotion.Fear:
                                ColorUtility.TryParseHtmlString(MyColor.fear, out color);
                                checkImg.color = color;                               
                                    break;
                        }
                    }
                    else{
                        _dateItems[i].GetComponent<DateItem>().SetDiary(null);
                        checkImg.enabled = false;
                    }
                    
                    label.text = (date + 1).ToString();
                    date++;
                }
            }
            else
            {
                _dateItems[i].transform.GetChild(1).gameObject.SetActive(false);
                _dateItems[i].GetComponent<Image>().enabled = false;
                _dateItems[i].GetComponent<Button>().enabled = false;
                label.text = "";
            }
        }
    }

    int GetDays(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 1;
            case DayOfWeek.Tuesday: return 2;
            case DayOfWeek.Wednesday: return 3;
            case DayOfWeek.Thursday: return 4;
            case DayOfWeek.Friday: return 5;
            case DayOfWeek.Saturday: return 6;
            case DayOfWeek.Sunday: return 0;
        }

        return 0;
    }
    public void YearPrev()
    {
        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

    public void ShowCalendar(Text target)
    {
        _calendarPanel.SetActive(true);
        _target = target;
        // _calendarPanel.transform.position = new Vector3(965, 475, 0);//Input.mousePosition-new Vector3(0,120,0);
    }

    Text _target;
    public void OnDateItemClick(string day)
    {
        _target.text = _yearNumText.text + "Year" + _monthNumText.text + "Month" + day+"Day";
        _calendarPanel.SetActive(false);
    }
}
