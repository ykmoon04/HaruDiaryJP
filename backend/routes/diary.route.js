const express = require('express');
const Diary = require('../models/diary.model.js');
const router = express.Router();

const {
  getDiaries,
  createDiary,
  getDiary,
} = require('../controllers/diary.controller.js');

router.get('/:id', getDiaries);
router.get('/:id/:date', getDiary);
router.post('/create', createDiary);

module.exports = router;
