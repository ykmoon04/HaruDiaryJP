const mongoose = require('mongoose');

// object
const ProductSchema = mongoose.Schema(
  {
    name: {
      type: String,
      required: [true, 'Please enter product name'],
    },
    quantity: {
      type: Number,
      required: true,
      default: 0,
    },
    price: {
      type: Number,
      required: true,
      default: 0,
    },
    image: {
      type: String,
      required: true,
      default: 0,
    },
  },
  {
    timestamps: true,
  }
);

const Product = mongoose.model('Product', ProductSchema); // 이름 단수로 할 것

module.exports = Product;
