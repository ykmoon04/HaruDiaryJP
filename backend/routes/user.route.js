const express = require('express');
const Product = require('../models/user.model.js');
const router = express.Router();

const {
  getUserById,
  createUser,
  updateUser,
} = require('../controllers/user.controller.js');

router.get('/:id', getUserById);
router.post('/create', createUser);
router.put('/update/:id', updateUser);

module.exports = router;
