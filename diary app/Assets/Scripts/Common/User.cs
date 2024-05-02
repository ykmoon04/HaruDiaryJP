
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
        points.joy = (int) (analysis.joy * 100);
        points.sadness = (int) (analysis.sadness * 100);
        points.disgust = (int) (analysis.disgust * 100);
        points.angry = (int) (analysis.angry * 100);
        points.surprise = (int) (analysis.surprise * 100);
        points.fear = (int) (analysis.fear * 100);
    }

    public int getPoint(Emotions em){
        switch(em){
            case Emotions.joy:
                return points.joy;
            case Emotions.sadness:
                return points.sadness;
            case Emotions.angry:
                return points.angry;
            case Emotions.fear:
                return points.fear;
            case Emotions.surprise:
                return points.surprise;
            case Emotions.disgust:
                return points.disgust;
        }

        return -1;
    }

    public void SetPoints(DiaryAnalysis analysis){
        points.joy = (int) analysis.joy * 100;
        points.sadness = (int) analysis.sadness * 100;
        points.disgust = (int) analysis.disgust * 100;
        points.angry = (int) analysis.angry * 100;
        points.surprise = (int) analysis.surprise * 100;
        points.fear = (int) analysis.fear * 100;

    }

    public int getPoint(string em){
        /*
        switch(em){
            case "happiness":
                return happinessPt;
            case "sadness":
                return sadnessPt;
            case "angry":
                return angryPt;
            case "fear":
                return fearPt;
            case "surprise":
                return surprisePt;
            case "disgust":
                return disgustPt;
        }
        */

        return -1;
    }

    
    public int setPoint(string em, int money){

        return -1;
    }
    
    public int setPoint(Emotions em, int money){
        /*
        switch(em){
            case Emotions.happiness:
               this.happinessmoney = money;
               break;
            case Emotions.sadness:
                this.sadnessmoney = money;
                break;
            case Emotions.angry:
                this.angrymoney = money;
                break;
            case Emotions.fear:
                this.fearmoney = money;
                break;
            case Emotions.surprise:
                this.surprisemoney = money;
                break;
            case Emotions.disgust:
                this.disgustmoney = money;
                break;
        }
        */

        return -1;
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