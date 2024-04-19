const mongoose = require('mongoose');

const UserSchema = mongoose.Schema(
  {
    user_id: {
      type: String,
      required: true,
    },
    password: {
      type: String,
      required: true,
    },
    nickname: {
      type: String,
      required: true,
    },
    points: {
      joy: {
        type: Number,
        required: true,
        default: 0,
      },
      sadness: {
        type: Number,
        required: true,
        default: 0,
      },
      angry: {
        type: Number,
        required: true,
        default: 0,
      },
      fear: {
        type: Number,
        required: true,
        default: 0,
      },
      surprise: {
        type: Number,
        required: true,
        default: 0,
      },
      disgust: {
        type: Number,
        required: true,
        default: 0,
      },
    },
  },
  {
    timestamps: true,
  }
);

const User = mongoose.model('User', UserSchema);

module.exports = User;
