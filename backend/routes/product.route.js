const express = require('express');
const Product = require('../models/product.model.js');
const router = express.Router();
const {
  getProducts,
  getProductById,
  createProduct,
  updateProduct,
  deleteProduct,
} = require('../controllers/product.controller.js');

router.get('/', getProducts);
router.get('/:id', getProductById);
router.post('/', createProduct);
router.put('/', updateProduct);
router.delete('/:id', deleteProduct);

module.exports = router;
