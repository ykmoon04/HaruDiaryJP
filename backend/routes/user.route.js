const express = require('express');
const Product = require('../models/user.model.js');
const router = express.Router();

const {
  getUserById,
  createUser,
} = require('../controllers/user.controller.js');

router.get('/:id', getUserById);
router.post('/', createUser);

module.exports = router;
