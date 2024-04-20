from transformers import AutoTokenizer, AutoModelForSequenceClassification
import numpy as np

checkpoint = 'cl-tohoku/bert-base-japanese-whole-word-masking'
tokenizer = AutoTokenizer.from_pretrained(checkpoint)

model = AutoModelForSequenceClassification.from_pretrained("stopovermoon/japanese-sentiment-analysis", token = "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")

emotion_names_jp = ['喜び', '悲しみ', '驚き', '怒り', '恐れ', '嫌悪']

def np_softmax(x):
    f_x = np.exp(x) / np.sum(np.exp(x))
    return f_x

def analyze_emotion(text, show_fig=False):
    model.eval()

    tokens = tokenizer(text, truncation=True, return_tensors="pt")
    tokens.to(model.device)
    preds = model(**tokens)
    prob = np_softmax(preds.logits.cpu().detach().numpy()[0])
    out_dict = {n: p for n, p in zip(emotion_names_jp, prob)}

    print(out_dict)

analyze_emotion('傘持たなかったのに急に雨が降り始めて濡れちゃった。アンラッキーすぎ')