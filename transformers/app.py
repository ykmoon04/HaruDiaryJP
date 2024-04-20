import analyze
import json
from flask import Flask
import requests

app = Flask(__name__)

@app.route('/')
def home():
    return 'Hello, World!'

@app.route('/eval', methods=['POST'])
def eval():
    ''' get parameter '''
    return analyze.analyze_emotion("")



if __name__ == '__main__':
    app.run(debug=True)