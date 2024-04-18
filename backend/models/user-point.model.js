const UserPointSchema = mongoose.Schema(
  {
    user_id: {
      type: String,
      required: true,
    },
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
  {
    timestamps: true,
  }
);

const UserPoint = mongoose.model('UserPoint', UserPointSchema);

module.exports = UserPoint;
