import analyze
from flask import Flask,request
app = Flask(__name__)

@app.route('/')
def home():
    return 'Japanese Sentiment Analysis'

@app.route('/eval', methods=['POST'])
def eval():
    params = request.get_json()
    return analyze.analyze_emotion(params["text"])


if __name__ == '__main__':
    app.run(debug=True)