const express = require('express');
const Diary = require('../models/diary.model.js');
const router = express.Router();

const {
  getDiaries,
  createDiary,
} = require('../controllers/diary.controller.js');

router.get('/:id', getDiaries);
router.post('/', createDiary);

module.exports = router;
