const mongoose = require('mongoose');

// object
const TreeSchema = mongoose.Schema(
  {
    user_id: {
      type: String,
      required: true,
    },
    tree_name: {
      type: String,
      required: true,
    },
    position_x: {
      type: Number,
      required: true,
      default: 0,
    },
    position_y: {
      type: Number,
      required: true,
      default: 0,
    },
    position_z: {
      type: Number,
      required: true,
      default: 0,
    },
  },
  {
    timestamps: true,
  }
);

const Tree = mongoose.model('Tree', TreeSchema);

module.exports = Tree;
