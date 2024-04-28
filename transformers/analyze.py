from transformers import AutoTokenizer, AutoModelForSequenceClassification
import numpy as np
import json


checkpoint = 'cl-tohoku/bert-base-japanese-whole-word-masking'
tokenizer = AutoTokenizer.from_pretrained(checkpoint)

model = AutoModelForSequenceClassification.from_pretrained("stopovermoon/japanese-sentiment-analysis", token = "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")

emotion_names = ['joy', 'sadness', 'surprise', 'angry', 'fear', 'disgust']

def np_softmax(x):
    f_x = np.exp(x) / np.sum(np.exp(x))
    return f_x

def analyze_emotion(text):
    model.eval()

    tokens = tokenizer(text, truncation=True, return_tensors="pt")
    tokens.to(model.device)
    preds = model(**tokens)
    prob = np_softmax(preds.logits.cpu().detach().numpy()[0])
    out_dict = {n: p for n, p in zip(emotion_names, prob)}

    return json.dumps(out_dict, default=convert_to_serializable)

# Convert float32 values to float
def convert_to_serializable(obj):
    if isinstance(obj, np.float32):
        return float(obj)
    return obj

# analyze_emotion('傘持たなかったのに急に雨が降り始めて濡れちゃった。アンラッキーすぎ')