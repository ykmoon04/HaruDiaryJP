using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
public class DiaryAnalysis : IEnumerable
{
    public double joy,sadness,angry,fear, surprise,disgust;

    Dictionary<Emotions, double> emotions;
    
    void InitEmotions(){
        if(emotions==null){
            emotions = new Dictionary<Emotions, double>
                    {
                        { Emotions.joy, joy },
                        { Emotions.sadness, sadness },
                        { Emotions.disgust, disgust },
                        { Emotions.angry, angry },
                        { Emotions.surprise, surprise },
                        { Emotions.fear, fear }
                    };
        }
    }

    public Emotions GetMaxEmotionType(){
        if(emotions==null){ InitEmotions();}
        return emotions.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
    }

    public IEnumerator GetEnumerator()
    {
        yield return joy;
        yield return sadness;
        yield return disgust;
        yield return angry;
        yield return surprise;
        yield return fear;
    }


}
