using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Diary
{
    public DiaryAnalysis analysis;

    public string user_id;    
    public string text;
    public string date;
    
    public bool hasText(){
        if(text == null || text == ""){
            return false;
        }
        return true;
    }

    public Emotions GetMaxEmotionType(){
        return analysis.GetMaxEmotionType();
    }
}

[System.Serializable]
public class DiaryAnalysis{
    public double joy,sadness,angry,fear, surprise,disgust;
    public DiaryAnalysis(){}
    public DiaryAnalysis(double joy, double sadness, double disgust, double angry, double surprise, double fear){
        this.joy = joy;
        this.sadness = sadness;
        this.disgust = disgust;
        this.angry = angry;
        this.surprise = surprise;
        this.fear = fear;
    }

    public Emotions GetMaxEmotionType(){
        Emotions res = Emotions.joy;
        double maxVal = joy;

        if(sadness > maxVal){
            maxVal=sadness;
            res = Emotions.sadness;
        }
        if(disgust > maxVal){
            maxVal=disgust;
            res = Emotions.disgust;
        }


        if(angry > maxVal){
            maxVal=angry;
            res = Emotions.angry;
        }


        if(surprise > maxVal){
            maxVal=surprise;
            res = Emotions.surprise;
        }


        if(fear > maxVal){
            maxVal=fear;
            res = Emotions.fear;
        }

        return res;
    }


}
