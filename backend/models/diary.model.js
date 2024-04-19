const Diary = mongoose.Schema(
  {
    user_id: {
      type: String,
      required: true,
    },
    text: {
      type: String,
      required: true,
    },
    date: {
      type: Date,
      required: true,
    },
    diary_result: [
      {
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
    ],
  },
  {
    timestamps: true,
  }
);

const Diary = mongoose.model('Diary', DiarySchema);

module.exports = Diary;