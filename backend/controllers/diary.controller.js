const axios = require('axios');
const Diary = require('../models/diary.model.js');

const getDiaries = async (req, res) => {
  try {
    const { id } = req.params;
    const diaries = await Diary.find({ user_id: id });
    res.status(200).json(diaries);
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

const getDiary = async (req, res) => {
  try {
    const { id, date } = req.params;
    const diary = await Diary.findOne({ user_id: id, date: date });
    if (diary) {
      res.status(200).json(diary);
    } else {
      res.status(404).json({ message: 'Diary not found' });
    }
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

const createDiary = async (req, res) => {
  try {
    const apiUrl = 'http://127.0.0.1:5000/eval';

    console.log(req.body.text);
    const analysisResult = await axios.post(
      apiUrl,
      { text: req.body.text },
      {
        headers: {
          'Content-Type': 'application/json',
        },
      }
    );

    console.log(analysisResult.data);

    const diary = await Diary.create({
      ...req.body,
      analysis: analysisResult.data,
    });

    console.log(diary);

    res.status(200).json(diary);
  } catch (error) {
    console.log(error.message);
    res.status(500).json({ message: error.message });
  }
};

module.exports = {
  getDiaries,
  createDiary,
  getDiary,
};
