const Tree = require('../models/tree.model.js');

const getTrees = async (req, res) => {
  try {
    const { id } = req.params;
    const trees = await Tree.find({ user_id: id });
    res.status(200).json({ trees: trees });
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

const createTree = async (req, res) => {
  try {
    const tree = await Tree.create(req.body);
    res.status(200).json(tree);
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

module.exports = {
  getTrees,
  createTree,
};
