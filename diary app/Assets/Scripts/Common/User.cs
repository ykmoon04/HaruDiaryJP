
using System;
using System.Collections.Generic;
[System.Serializable]
public class User {
    public string _id;
    public string nickname;
    
    public Points points;
    
    public string GetId() => _id;
    public string GetName() => nickname;
    

    public void UpdatePoints(DiaryAnalysis analysis) {
        points.joy += analysis.joy > 0.1 ? (int) Math.Round(analysis.joy * 100, 0) : 0;
        points.sadness += analysis.sadness > 0.1 ? (int) Math.Round(analysis.sadness * 100, 0) : 0;
        points.disgust += (int) analysis.disgust > 0.1 ? (int) Math.Round(analysis.disgust * 100, 0) : 0;
        points.angry += analysis.angry > 0.1 ? (int) Math.Round(analysis.angry * 100, 0) : 0;
        points.surprise += analysis.surprise > 0.1 ? (int) Math.Round(analysis.surprise * 100, 0) : 0;
        points.fear += (int) analysis.fear > 0.1 ? (int) Math.Round(analysis.fear * 100, 0) : 0;
    }

    public void UpdatePoints(Emotion emotion, int change) {
        switch (emotion)
        {
            case Emotion.Joy:
                points.joy += change;
                break;
            case Emotion.Sadness:
                points.sadness += change;
                break;
            case Emotion.Angry:
                points.angry += change;
                break;
            case Emotion.Fear:
                points.fear += change;
                break;
            case Emotion.Surprise:
                points.surprise += change;
                break;
            case Emotion.Disgust:
                points.disgust += change;
                break;
        }
    }

    

    public int getPoint(Emotion emotion){
        switch(emotion){
            case Emotion.Joy:
                return points.joy;
            case Emotion.Sadness:
                return points.sadness;
            case Emotion.Angry:
                return points.angry;
            case Emotion.Fear:
                return points.fear;
            case Emotion.Surprise:
                return points.surprise;
            case Emotion.Disgust:
                return points.disgust;
        }

        return -1;
    }

  


    public void setPoint(Emotion emotion, int value){
        
        switch(emotion){
            case Emotion.Joy:
                points.joy = value;
                break;
            case Emotion.Sadness:
                points.sadness = value;
                break;
            case Emotion.Angry:
                points.angry = value;
                break;
            case Emotion.Fear:
                points.fear = value;
                break;
            case Emotion.Surprise:
                points.surprise = value;
                break;
            case Emotion.Disgust:
                points.disgust = value;
                break;
        }

    }


/*
    public override string ToString(){
        return "\"id\": " + _id + "\n\"passsword\": " + password + "\n\"name\": " + name + "\n\"happinessPt\": " + happinessPt + " \"disgustPt\": " + disgustPt +" \"sadnessPt\": " + sadnessPt +" \"angryPt\": " + angryPt +" \"surprisePt\": " + surprisePt +" \"fearPt\": " + fearPt;
    }
    */
}

[System.Serializable]
public class Points {
    public int joy,sadness,angry,fear, surprise,disgust; 
}