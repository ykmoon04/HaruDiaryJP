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

const createDiary = async (req, res) => {
  try {
    const diary = await Diary.create(req.body);
    res.status(200).json(diary);
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

module.exports = {
  getDiaries,
  createDiary,
};
