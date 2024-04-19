from transformers import AutoTokenizer, AutoModelForSequenceClassification
import numpy as np

checkpoint = 'cl-tohoku/bert-base-japanese-whole-word-masking'
tokenizer = AutoTokenizer.from_pretrained(checkpoint)
# model = AutoModelForSequenceClassification.from_pretrained('./model/')

# tokenizer = AutoTokenizer.from_pretrained("stopovermoon/japanese-sentiment-analysis")
model = AutoModelForSequenceClassification.from_pretrained("stopovermoon/japanese-sentiment-analysis", token = "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")

emotion_names_jp = ['喜び', '悲しみ', '期待', '驚き', '怒り', '恐れ', '嫌悪', '信頼']

def np_softmax(x):
    f_x = np.exp(x) / np.sum(np.exp(x))
    return f_x

def analyze_emotion(text, show_fig=False):
    # 推論モードを有効化
    model.eval()

    # 入力データ変換 + 推論
    tokens = tokenizer(text, truncation=True, return_tensors="pt")
    tokens.to(model.device)
    preds = model(**tokens)
    prob = np_softmax(preds.logits.cpu().detach().numpy()[0])
    out_dict = {n: p for n, p in zip(emotion_names_jp, prob)}

    # 棒グラフを描画
    if show_fig:
        plt.figure(figsize=(8, 3))
        df = pd.DataFrame(out_dict.items(), columns=['name', 'prob'])
        sns.barplot(x='name', y='prob', data=df)
        plt.title('入力文 : ' + text, fontsize=15)
    else:
        print(out_dict)

analyze_emotion('バイトで上司に𠮟られた。。やめたいな')