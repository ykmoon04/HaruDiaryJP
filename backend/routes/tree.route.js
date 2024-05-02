const express = require('express');
const Tree = require('../models/tree.model.js');
const router = express.Router();

const { getTrees, createTree } = require('../controllers/tree.controller.js');

router.get('/:id', getTrees);
router.post('/create', createTree);

module.exports = router;
